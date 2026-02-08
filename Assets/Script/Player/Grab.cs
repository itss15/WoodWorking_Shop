using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class Grab : MonoBehaviour
{

    [Header("KEYS")]
    [SerializeField] KeyCode GrabPlankKey;
    [SerializeField] KeyCode GrabNormalSawKey;



    [Header("Grabbed Objects")]
    public GameObject GrabbedPlank;
    public GameObject GrabbedNormalSaw;


    [Header("Plank Var")]
    [SerializeField] float delay = 1f;

    [Header("Normal Saw Var")]
    [SerializeField] Vector3 NormalSawLastGrabbedPosition;
    [SerializeField] Vector3 NormalSawLastGrabbedRotation;
    void Update()
    {
        DropPlank();
        DropNormalSaw();
    }

    private void LateUpdate()
    {
        GrabPlank();
        GrabNormalSaw();
    }


    public bool IsHoldingAnything(GameObject Exception = null)
    {
        GameObject[] GrabbedObjects = { GrabbedNormalSaw, GrabbedPlank };

        if (Exception == null)
        {
            foreach (GameObject GrabbedObj in GrabbedObjects)
            {
                if (GrabbedObj != null)
                {
                    return true;
                }
            }
        }else
        {
            foreach (GameObject GrabbedObj in GrabbedObjects)
            {
                if (GrabbedObj != null && GrabbedObj != Exception)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void GrabPlank()
    {
        RaycastHit hit = GetComponent<RayCastInteraction>().GrabPlankInteraction();

        

        Plank plank;

        if (hit.collider != null && !hit.collider.gameObject.GetComponentInParent<Plank>().IsClamped && !IsHoldingAnything())
        {
            if (hit.collider.gameObject.GetComponentInParent<Plank>() == null)
            {
                print("ERROR. no plank script found");
                print(hit.collider.gameObject.name);
                return;
            }
            plank = hit.collider.gameObject.GetComponentInParent<Plank>();
            if (Input.GetKeyDown(GrabPlankKey) && GrabbedPlank == null)
            {
                plank.IsGrabbed = true;
                plank.AnimateGrabPlank(transform);
                GrabbedPlank = plank.gameObject;
                plank.GetComponentInParent<Rigidbody>().useGravity = false;
                plank.GetComponentInChildren<Collider>().enabled = false;
                plank.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
    }

    public void GrabClampedPlank(GameObject Plank)
    {
        RaycastHit hit = GetComponent<RayCastInteraction>().GrabClampedPlankInteraction();
        
        if (hit.collider != null && !IsHoldingAnything())
        {
            Plank plank = Plank.GetComponent<Plank>();
            if (hit.collider.gameObject.GetComponent<ClampTable>() == null)
            {
                print("ERROR. no clamptable script found");
                print(hit.collider.gameObject.name);
                return;
            }

            plank.IsGrabbed = true;
            plank.AnimateGrabPlank(transform);
            GrabbedPlank = plank.gameObject;
            plank.GetComponentInParent<Rigidbody>().useGravity = false;
            plank.GetComponentInChildren<Collider>().enabled = false;
            plank.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    void DropPlank()
    {
        if (GrabbedPlank != null)
        {
            if (Input.GetKeyDown(GrabPlankKey))
            {
                GrabbedPlank.GetComponentInChildren<Collider>().enabled = true;
                GrabbedPlank.gameObject.GetComponent<Plank>().IsGrabbed = false;
                GrabbedPlank.GetComponent<Plank>().AnimateGrabPlank(transform);
                GrabbedPlank.GetComponent<Rigidbody>().useGravity = true;
                Invoke(nameof(GrabPlankDelay), delay);
            }
        }
    }


    public void Clamp()
    {
        GrabbedPlank.GetComponentInChildren<Collider>().enabled = true;
        GrabbedPlank.gameObject.GetComponent<Plank>().IsGrabbed = false;
        GrabbedPlank.GetComponent<Plank>().AnimateGrabPlank(transform);
        GrabbedPlank = null;
    }
    void GrabPlankDelay()
    {
        GrabbedPlank = null;
    }
    

    public void GrabNormalSaw()
    {
        if (GetComponent<RayCastInteraction>().ANormalSawInteraction().collider != null && Input.GetKeyDown(GrabNormalSawKey) && !IsHoldingAnything())
        {
            RaycastHit hit = GetComponent<RayCastInteraction>().ANormalSawInteraction();
            NormalSawLastGrabbedPosition = hit.collider.gameObject.transform.position;
            GrabbedNormalSaw = hit.collider.gameObject;
            GrabbedNormalSaw.GetComponent<NormalSaw>().IsGrabbed = true;
            GrabbedNormalSaw.GetComponent<Rigidbody>().useGravity = false;
            GrabbedNormalSaw.GetComponent<Collider>().enabled = false;
            GrabbedNormalSaw.transform.position = transform.position;
            NormalSawLastGrabbedRotation = GrabbedNormalSaw.transform.rotation.eulerAngles;
        }
    }

    public void DropNormalSaw()
    {
        if (GrabbedNormalSaw != null && Input.GetKeyDown(GrabNormalSawKey))
        {
            GrabbedNormalSaw.GetComponent<NormalSaw>().IsGrabbed = false;
            GrabbedNormalSaw.GetComponent<Rigidbody>().useGravity = true;
            GrabbedNormalSaw.GetComponent<Collider>().enabled = true;
            GrabbedNormalSaw.transform.position = NormalSawLastGrabbedPosition;
            GrabbedNormalSaw.transform.rotation = Quaternion.Euler(NormalSawLastGrabbedRotation);
            GrabbedNormalSaw = null;
        }
    }
}
