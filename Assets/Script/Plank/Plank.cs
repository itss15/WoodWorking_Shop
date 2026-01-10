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
    S3x3,
    S3x5,
    S5x5,
    S5x7,
    S8x8,
    S10x10
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

    private void Start()
    {
        switch (plankSize)
        {
            case EPlankSize.S3x3:
                PlankModel.localScale = new Vector3(3, thickness, 3); break;
            case EPlankSize.S3x5:
                PlankModel.localScale = new Vector3(3, thickness, 5); break;
            case EPlankSize.S5x5:
                PlankModel.localScale = new Vector3(5, thickness, 5); break;
            case EPlankSize.S5x7:
                PlankModel.localScale = new Vector3(5, thickness, 7); break;
            case EPlankSize.S8x8:
                PlankModel.localScale = new Vector3(8, thickness, 8); break;
            case EPlankSize.S10x10:
                PlankModel.localScale = new Vector3(10, thickness, 10); break;
            default:
                PlankModel.localScale = new Vector3(3, thickness, 3); break;
        }
    }


    private void Update()
    {
        UpdatePlank();
    }

    public void AnimateGrabPlank(Transform pos)
    {
        if (IsGrabbed)
        {
            if (GetComponent<Animator>().GetBool("IsGrabbed") == false)
            {
                GetComponent<Animator>().SetBool("IsGrabbed", true);
            }

            while (IsGrabbed)
            {
                transform.position = pos.transform.position;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("IsGrabbed", false);
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
            if (Amount > 0 && Amount < length)
            {
                length -= Amount;
            }
        }

        if (Axis == "width")
        {
            if (Amount > 0 && Amount < width)
            {
                width -= Amount;
            }
        }

        PlankModel.localScale = new Vector3(length, thickness, width);
    }
}
