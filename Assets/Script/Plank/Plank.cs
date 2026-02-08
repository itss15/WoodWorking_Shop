using System.Collections;
using UnityEngine;

enum EPlankType {
    Pine,
    Birch,
    Oak,
    Walnut,
    Ebony
}

enum EPlankSize
{
    S1x1,
    S1x2,
    S2x2,
    S2x3,
    S3x3,
    S4x5
}
public class Plank : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform PlankModel;
    [SerializeField] Material[] WoodTypesMaterials;

    [Header("Stats")]
    public float width;
    public float length;
    public float thickness;
    [SerializeField] EPlankType plankType;
    [SerializeField] EPlankSize plankSize;
    public bool IsGrabbed = false;
    public bool IsClamped = false;

    private void Start()
    {
        switch (plankSize)
        {
            case EPlankSize.S1x1:
                PlankModel.localScale = new Vector3(1, thickness, 1); width = 1; length = 1; break;
            case EPlankSize.S1x2:
                PlankModel.localScale = new Vector3(1, thickness, 2); width = 1; length = 2; break;
            case EPlankSize.S2x2:
                PlankModel.localScale = new Vector3(2, thickness, 2); width = 2; length = 2; break;
            case EPlankSize.S2x3:
                PlankModel.localScale = new Vector3(2, thickness, 3); width = 2; length = 3; break;
            case EPlankSize.S3x3:
                PlankModel.localScale = new Vector3(3, thickness, 3); width = 3; length = 3; break;
            case EPlankSize.S4x5:
                PlankModel.localScale = new Vector3(4, thickness, 5); width = 4; length = 5; break;
            default:
                PlankModel.localScale = new Vector3(1, thickness, 1); width = 1; length = 1; break;
        }
    }


    private void Update()
    {
        UpdatePlank();
    }

    private void LateUpdate()
    {
        ClampedUpdate();
    }

    bool didSetPos = false;
    Vector3 clampedPos;
    Vector3 clampedRot;
    void ClampedUpdate()
    {
        if (IsClamped)
        {
            if (!didSetPos)
            {
                clampedPos = transform.position;
                clampedRot = transform.rotation.eulerAngles;
                didSetPos = true;
            }
            else
            {
                transform.position = clampedPos;
                transform.rotation = Quaternion.Euler(clampedRot);
            }
        }else
        {
            if (didSetPos)
            {
                didSetPos = false;
            }
        }
    }

    public void AnimateGrabPlank(Transform pos)
    {
        if (IsGrabbed)
        {
            if (GetComponent<Animator>().GetBool("IsGrabbed") == false)
            {
                GetComponent<Animator>().SetBool("IsGrabbed", true);
            }

            StartCoroutine(IGrabSetPos(pos));
        }
        else
        {
            GetComponent<Animator>().SetBool("IsGrabbed", false);
        }
    }

    IEnumerator IGrabSetPos(Transform pos)
    {
        while (IsGrabbed)
        {
            transform.position = pos.transform.position;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSecondsRealtime(.001f);
        }
    }

    public void UpdatePlank()
    {
        switch (plankType)
        {
            case (EPlankType.Pine):
                PlankModel.gameObject.GetComponent<MeshRenderer>().material = WoodTypesMaterials[0]; break;
            case (EPlankType.Birch):
                PlankModel.gameObject.GetComponent<MeshRenderer>().material = WoodTypesMaterials[1]; break;
            case (EPlankType.Oak):
                PlankModel.gameObject.GetComponent<MeshRenderer>().material = WoodTypesMaterials[2]; break;
            case (EPlankType.Walnut):
                PlankModel.gameObject.GetComponent<MeshRenderer>().material = WoodTypesMaterials[3]; break;
            case (EPlankType.Ebony):
                PlankModel.gameObject.GetComponent<MeshRenderer>().material = WoodTypesMaterials[4]; break;
            default:
                PlankModel.gameObject.GetComponent<MeshRenderer>().material = WoodTypesMaterials[0]; break;
        }
    }

    public void Cut(string Axis = "length", float Amount = 1f)
    {
        Axis = Axis.ToLower();
        if (Axis == "length")
        {
            if (Amount > 0 && Amount < length && (length - Amount) >= 1)
            {
                length -= Amount;
            }
        }

        if (Axis == "width")
        {
            if (Amount > 0 && Amount < width && (width - Amount) >= 1)
            {
                width -= Amount;
            }
        }

        PlankModel.localScale = new Vector3(length, thickness, width);
    }
}
