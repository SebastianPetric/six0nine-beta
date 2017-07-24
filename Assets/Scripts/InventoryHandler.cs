using UnityEngine;
using System.Collections;
using System.Linq;

public class InventoryHandler : MonoBehaviour
{
    /*
    index 0 = Flasche
    index 1= Mud
    index 2= BrokenPiece1
    index 3= BrokenPiece2     
    */
    public GameObject[] CurrentItemsInInventar;
    private int totalItemToSelectCount;
	void Start ()
	{
	    totalItemToSelectCount = GameObject.FindGameObjectsWithTag("ItemToSelect").Length;
        CurrentItemsInInventar= new GameObject[totalItemToSelectCount];
	}

    public bool checkIfAllItemsAreInInventory()
    {
        for (int i = 0; i < totalItemToSelectCount; i++)
        {
            if (CurrentItemsInInventar[i] == null)
            {
                return false;
                break;
            } 
        }
        return true;
    }


    public void addItemInInventory(GameObject _item)
    {
        CurrentItemsInInventar[_item.GetComponent<ItemToSelect>().index] = _item;
    }

    public bool isThisItemInInventory(int _index)
    {
        if (CurrentItemsInInventar[_index] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
