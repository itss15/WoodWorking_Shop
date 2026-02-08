using System;
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    [Header("Serializetion")]
    [SerializeField] GameObject PlayerCamera;

    [Header("Keys")]
    [SerializeField] KeyCode ClampTableInteractionKey;
    [SerializeField] KeyCode CutPlankKey;
    [Header("Interactions")]

    [Header("GrabPlank")]
    [SerializeField] float GrabPlankDistance;
    [SerializeField] LayerMask PlankLayerMask;

    [Header("InteractClampTable")]
    [SerializeField] float InteractClampTableDistance;
    [SerializeField] GameObject LastLookedClampTable; 
    [SerializeField] LayerMask ClampTableLayerMask;
    [SerializeField] Transform Woodpos;
    public bool IsClamping = false;

    [Header("GrabSaw")]
    [SerializeField] float GrabANormalSawDistance;
    [SerializeField] LayerMask ANormalSawLayerMask;

    [Header("CutPlank")]
    [SerializeField] float CutPlankDistance;

    private void Update()
    {
        ClampTableInteraction();
        CutPlankNormalSaw();
    }

    public RaycastHit GrabPlankInteraction()
    {
        RaycastHit hit;
        Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, GrabPlankDistance, PlankLayerMask);
        return hit;
    }

    public RaycastHit GrabClampedPlankInteraction()
    {
        RaycastHit hit;
        Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, GrabPlankDistance, ClampTableLayerMask);
        return hit;
    }

    public void ClampTableInteraction()
    {
        if (Input.GetKeyDown(ClampTableInteractionKey) && Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, InteractClampTableDistance, ClampTableLayerMask) && !IsClamping && !GetComponent<Grab>().IsHoldingAnything(GetComponent<Grab>().GrabbedPlank))
        {
            IsClamping = true;
            RaycastHit hit;
            Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, InteractClampTableDistance, ClampTableLayerMask);
            GameObject Plank;
            if (hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank == null)
            {
                try
                {
                    Plank = GetComponent<Grab>().GrabbedPlank;
                }
                catch
                {
                    print("No Grabbed Plank");
                    return;
                }

                Woodpos = hit.collider.gameObject.GetComponentInChildren<WoodPos>().gameObject.transform;

                GetComponent<Grab>().Clamp();
                GameObject PlankChild = null;

                foreach (Transform child in Plank.transform)
                {
                    PlankChild = child.gameObject;
                }
                Plank.transform.localRotation = Quaternion.Euler(Vector3.zero);
                Plank.GetComponentInParent<Transform>().transform.position = new Vector3(Woodpos.position.x - (PlankChild.transform.localScale.x / 2), Woodpos.position.y, Woodpos.position.z);
                hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank = Plank;
                hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank.GetComponent<Plank>().IsClamped = true;
            }

        }
        else if (Input.GetKeyDown(ClampTableInteractionKey) && Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, InteractClampTableDistance, ClampTableLayerMask) && !GetComponent<Grab>().IsHoldingAnything())
        {
            RaycastHit hit;
            Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, InteractClampTableDistance, ClampTableLayerMask);
            if (hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank != null)
            {
                IsClamping = false;
                GetComponent<Grab>().GrabClampedPlank(hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank);
                hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank.GetComponent<Plank>().IsClamped = false;
                hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank.GetComponent<Plank>().IsGrabbed = true;
                hit.collider.gameObject.GetComponent<ClampTable>().ClampedPlank = null;
            }
            
        }

        RaycastHit hitLook;
        Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hitLook, InteractClampTableDistance, ClampTableLayerMask);
        if (hitLook.collider != null) { hitLook.collider.gameObject.GetComponent<ClampTable>().LookingAtTable = true; LastLookedClampTable = hitLook.collider.gameObject; }
        if (hitLook.collider == null && LastLookedClampTable != null) { LastLookedClampTable.GetComponent<ClampTable>().LookingAtTable = false; LastLookedClampTable = null; }
    }
     
    public RaycastHit ANormalSawInteraction()
    {
        RaycastHit hit;
        Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, GrabANormalSawDistance, ANormalSawLayerMask);
        return hit;
    }

    public void CutPlankNormalSaw()
    {
        if (Input.GetKeyDown(CutPlankKey) && !GetComponent<Grab>().IsHoldingAnything(GetComponent<Grab>().GrabbedNormalSaw) && GetComponent<Grab>().GrabbedNormalSaw != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, CutPlankDistance, PlankLayerMask))
            {
                if (hit.collider.gameObject.GetComponentInParent<Plank>().IsClamped)
                {
                    GetComponent<Grab>().GrabbedNormalSaw.GetComponent<NormalSaw>().Cutt(hit.collider.gameObject);
                    hit.collider.gameObject.GetComponentInParent<Plank>().GetComponentInParent<Transform>().transform.position = new Vector3(Woodpos.position.x - (hit.collider.gameObject.transform.localScale.x / 2), Woodpos.position.y, Woodpos.position.z);
                }
            }
        }
    }
}
