using UnityEngine;

public class ClampTable : MonoBehaviour
{
    [SerializeField] GameObject WoodHologram;
    public bool LookingAtTable = false;
    GameObject player;
    public GameObject ClampedPlank;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        ShowTable();
    }
    void ShowTable()
    {
        if (LookingAtTable && ClampedPlank == null)
        {
            WoodHologram.SetActive(true);
        }else
        {
            WoodHologram.SetActive(false);
        }
    }
}
