using UnityEngine;
using System.Collections;

public class ItemToSelect : MonoBehaviour
{
    private TriggerHandler TriggerHandler;
    private DimensionHandler DimensionHandler;
    private InventoryHandler InventoryHandler;

    public int index;
    public string name;
    public Dimension inWhichDimensionShouldThisItemBeIn;
    

    void Start () {
        TriggerHandler = GameObject.FindGameObjectWithTag("TriggerHandler").gameObject.GetComponent<TriggerHandler>();
        DimensionHandler= GameObject.FindGameObjectWithTag("DimensionHandler").gameObject.GetComponent<DimensionHandler>();
        InventoryHandler = GameObject.FindGameObjectWithTag("InventoryHandler").gameObject.GetComponent<InventoryHandler>();
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag.Equals("Player"))
        {
            gameObject.GetComponent<Light>().enabled = true;
        } 
    }

    void OnTriggerExit(Collider _col)
    {
        if (_col.gameObject.tag.Equals("Player"))
        {
            gameObject.GetComponent<Light>().enabled = false;
        }
    }

    void Update()
    {
        if (!inWhichDimensionShouldThisItemBeIn.Equals(DimensionHandler.GetCurrentDimension()))
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponentInChildren<BoxCollider>().enabled = false;
        }
        else
        {
            if (!InventoryHandler.isThisItemInInventory(index))
            {
                GetComponentInChildren<MeshRenderer>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;
                GetComponentInChildren<BoxCollider>().enabled = true;
            }
        }
    }
}
