using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

    private InventoryHandler InventoryHandler;
    public int index;
    public bool showInventory=false;
	void Start () {
        InventoryHandler = GameObject.FindGameObjectWithTag("InventoryHandler").gameObject.GetComponent<InventoryHandler>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.I))
	    {
	        if (showInventory == false)
	        {
	            showInventory = true;
                if (InventoryHandler.isThisItemInInventory(index))
	            {
	                gameObject.GetComponent<Image>().enabled = true;
	            }
	        }
	        else
	        {
	            showInventory = false;
                gameObject.GetComponent<Image>().enabled = false;
            }
	    }
	}
}
