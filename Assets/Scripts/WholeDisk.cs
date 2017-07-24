using UnityEngine;
using System.Collections;

public class WholeDisk : MonoBehaviour {
    private TriggerHandler TriggerHandler;

    void Start () {
        TriggerHandler = GameObject.FindGameObjectWithTag("TriggerHandler").gameObject.GetComponent<TriggerHandler>();
    }
	
	
	void Update () {
	    if (TriggerHandler.getIfBridgeCanBeActivated())
	    {

	    }
	    if (GetComponent<MeshRenderer>().enabled == true)
	    {
            TriggerHandler.setIfBridgeCanBeActivated(true);
	    }
	}
}
