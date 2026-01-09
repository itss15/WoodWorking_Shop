using UnityEngine;

public class cutter : MonoBehaviour
{
    [SerializeField] float lengthToCut;
    [SerializeField] float widthToCut;

    [SerializeField] KeyCode key;


    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Plank plank = GetComponent<Plank>();

            plank.Cut("length", lengthToCut);
            plank.Cut("width", widthToCut);
        }
    }
}
