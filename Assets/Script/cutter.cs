using UnityEngine;

public class cutter : MonoBehaviour
{
    [SerializeField] float lengthToCut;
    [SerializeField] float widthToCut;

    [SerializeField] float MaxDistanceClampTable = 5f;
    [SerializeField] GameObject PlayerCamera;
    GameObject clamptable = null;

    [SerializeField] KeyCode key;


    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Plank plank = GetComponent<Plank>();

            plank.Cut("length", lengthToCut);
            plank.Cut("width", widthToCut);
        }

        ClampTable();
    }

    void ClampTable()
    {
        RaycastHit hit;

        Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, MaxDistanceClampTable);

        

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Clamp Table"))
            {
                clamptable = hit.collider.gameObject;

                clamptable.GetComponent<ClampTable>().LookingAtTable = true;
            }else
            {
                if (clamptable != null)
                {
                    clamptable.GetComponent<ClampTable>().LookingAtTable = false;
                    clamptable = null;
                }
            }
        }
        
    }
}
