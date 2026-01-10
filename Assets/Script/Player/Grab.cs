using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] KeyCode GrabKey;
    [SerializeField] Camera PlayerCamera;
    [SerializeField] float MaxGrabDistance = 3f;
    [SerializeField] GameObject GrabbedPlank;
    void Update()
    {
        
        DropPlank();
    }

    private void FixedUpdate()
    {
        GrabPlank();
    }

    void GrabPlank()
    {
        RaycastHit hit;

        Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, MaxGrabDistance);

        GameObject objectHit;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Plank"))
            {
                objectHit = hit.collider.gameObject;
                if (Input.GetKeyDown(GrabKey) && GrabbedPlank == null)
                {
                    objectHit.GetComponentInParent<Plank>().IsGrabbed = true;
                    objectHit.GetComponentInParent<Plank>().AnimateGrabPlank(transform);
                    StopCoroutine("PlankDropDelay");
                    StartCoroutine(IPlankDropDelay(objectHit));
                }
            }

        }
    }

    IEnumerator IPlankDropDelay(GameObject plank)
    {
        yield return new WaitForSecondsRealtime(1f);
        GrabbedPlank = plank;
    }

    void DropPlank()
    {
        if (GrabbedPlank != null)
        {
            if (Input.GetKeyDown(GrabKey))
            {
                GrabbedPlank.gameObject.GetComponent<Plank>().IsGrabbed = false;
                GrabbedPlank.GetComponent<Plank>().AnimateGrabPlank(transform);
            }
        }
    }
}
