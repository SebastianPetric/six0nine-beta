using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{

    private InventoryHandler InventoryHandler;
    public string name= "Schalter betätigen";

	void Start () {
        InventoryHandler = GameObject.FindGameObjectWithTag("InventoryHandler").gameObject.GetComponent<InventoryHandler>();
    }
}
