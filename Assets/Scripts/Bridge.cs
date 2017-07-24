using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour
{

    private TriggerHandler TriggerHandler;
	// Use this for initialization
	void Start () {
        TriggerHandler = GameObject.FindGameObjectWithTag("TriggerHandler").gameObject.GetComponent<TriggerHandler>();
    }
	
	// Update is called once per frame
	void Update () {

	    if (TriggerHandler.getIfBridgeCanBeActivated())
	    {
           // Hier soll Brücke nach vorne bewegt werden
           //GameObject<AudioSource>().Play();
	    }

	}
}
