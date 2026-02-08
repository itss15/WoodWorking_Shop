using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class NormalSaw : MonoBehaviour
{
    [Header("Animation")]
    Animator animator;
    [SerializeField] bool IsMoving = false;
    public bool IsGrabbed = false;
    GameObject Player;

    [Header("Cutting")]
    [SerializeField] Vector2 CuttPlankAmount;
    [SerializeField] float CuttTime = 2f;
    public bool IsCutting = false;

    //[Header("Cutting")]
    private void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        UpdateAnimation();
        if (IsGrabbed)
        {
            transform.position = Player.transform.position;
            transform.rotation = Player.GetComponentInChildren<Camera>().gameObject.transform.rotation;
        }
    }

    void UpdateAnimation()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !IsCutting)
        {
            IsMoving = true;
        }else
        {
            IsMoving = false;
        }

        animator.SetBool("IsPlayerWalking", IsMoving);
        animator.SetBool("IsGrabbed", IsGrabbed);
    }


    public void Cutt(GameObject Plank)
    {
        StartCoroutine(ICutt(Plank));
    }

    IEnumerator ICutt(GameObject Plank)
    {
        IsCutting = true;
        
        Player.GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(CuttTime);

        Plank.GetComponentInParent<Plank>().Cut("length", CuttPlankAmount.x);
        Plank.GetComponentInParent<Plank>().Cut("width", CuttPlankAmount.y);

        Player.GetComponent<PlayerMovement>().enabled = true;

        IsCutting = false;
    }
}
