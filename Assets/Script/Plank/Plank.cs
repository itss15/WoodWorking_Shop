using UnityEngine;

public class Plank : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform PlankModel;

    [Header("Stats")]
    public float width;
    public float length;
    public float thickness;

    private void Update()
    {
        UpdatePlank();
    }

    public void UpdatePlank()
    {
        transform.localScale = new Vector3(width, thickness, length);
    }
}
