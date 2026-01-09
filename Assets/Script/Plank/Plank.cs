using UnityEngine;

enum EPlankType {
    Pine,
    Birch,
    Oak,
    Walnut,
    Ebony
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

    private void Start()
    {
        
    }


    private void Update()
    {
        UpdatePlank();
    }

    public void UpdatePlank()
    {
        PlankModel.localScale = new Vector3(width * 2, thickness, length * 2);

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
    }
}
