using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Editor Settings")]
    [SerializeField] bool ShowGroundCheckRay = true;


    [Header("Movement")]
    [SerializeField] float Speed = 5f;
    float SpeedMultiplier = 1f;
    [SerializeField] float SprintMultiplier = 1.5f;


    [Header("Jump")]
    [SerializeField] float JumpForce = 1f;
    
    [Header("Ground Check")]
    public bool isGrounded = false;
    [SerializeField] Vector3 RayOffset;
    [SerializeField] float GroundDistance = 1f;
    [SerializeField] LayerMask GroundLayer;

    [Header("Look")]
    public float MouseSensetivity = 100f;
    public Transform CameraTransform;

    Rigidbody rb;
    float multiTemp = 0f;
    float mouseXRotation = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        multiTemp = SpeedMultiplier;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Sprint();
        GroundCheck();
        Jump();
        Look();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        Vector3 dir = transform.TransformDirection(input);

        rb.MovePosition(rb.position + (dir * (Speed * SpeedMultiplier) * Time.deltaTime));
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftControl) && GetComponent<Grab>().GrabbedPlank == null)
        {
            SpeedMultiplier = SprintMultiplier;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            SpeedMultiplier = multiTemp;
        }
    }

    void GroundCheck()
    {
        bool didHit = Physics.Raycast(transform.position + RayOffset, Vector3.down, out RaycastHit hit, GroundDistance, GroundLayer);
        
        if (didHit)
        {
            isGrounded = true;
        }else
        {
            isGrounded = false;
        }
        
        if (ShowGroundCheckRay)
        {
            Debug.DrawRay(transform.position + RayOffset, Vector3.down * GroundDistance, Color.brown);
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, JumpForce, 0));
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensetivity* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensetivity * Time.deltaTime;

        mouseXRotation -= mouseY;
        mouseXRotation = Mathf.Clamp(mouseXRotation, -90f, 90f);

        if (GetComponent<Grab>().GrabbedPlank == null)
        {
            CameraTransform.localRotation = Quaternion.Euler(mouseXRotation, 0, 0);
        }else
        {
            CameraTransform.localRotation = Quaternion.Euler(15, 0, 0);
        }

        transform.Rotate(Vector3.up * mouseX);
    }
}
