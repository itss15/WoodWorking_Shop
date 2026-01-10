using UnityEngine;

public class ClampTable : MonoBehaviour
{
    [SerializeField] GameObject WoodHologram;
    public bool LookingAtTable = false;


    private void Update()
    {
        ShowTable();
    }
    void ShowTable()
    {
        if (LookingAtTable)
        {
            WoodHologram.SetActive(true);
        }else
        {
            WoodHologram.SetActive(false);
        }
    }
}
