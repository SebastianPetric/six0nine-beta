using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Poster : MonoBehaviour
{
    private GameObject ToolTipPanel;
    private TriggerHandler TriggerHandler;
    private Camera PlayerCamera;

	void Start ()
	{
	    PlayerCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
        ToolTipPanel= GameObject.FindGameObjectWithTag("ToolTipPanel").gameObject;
        TriggerHandler = GameObject.FindGameObjectWithTag("TriggerHandler").gameObject.GetComponent<TriggerHandler>();
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.tag.Equals("Player"))
        {
            TriggerHandler.setTriggerActive(true);
            if (TriggerHandler.getIfeffectWasTriggeredFirstTime() == false)
            {
                ToolTipPanel.GetComponent<Image>().enabled = true;
                ToolTipPanel.GetComponentInChildren<Text>().enabled = true;
                ToolTipPanel.GetComponentInChildren<Text>().text =
                    "Bewege deinen Kopf näher zur Kamera um das Poster genauer betrachten zu können. Verweile dabei in dieser Position um die Kamera zu kalibrieren.";
            }
        }
    }

    void OnTriggerExit()
    {
        TriggerHandler.setTriggerActive(false);
        ToolTipPanel.GetComponentInChildren<Text>().text = "";
        ToolTipPanel.GetComponent<Image>().enabled = false;
        ToolTipPanel.GetComponentInChildren<Text>().enabled = false;
    }

    void Update () {
        
        if (PlayerCamera.fieldOfView < 45 && TriggerHandler.getIsActive())
        {
            TriggerHandler.setEffectWasTriggeredFirstTime(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            ToolTipPanel.GetComponent<Image>().enabled = false;
            ToolTipPanel.GetComponentInChildren<Text>().enabled = false;
            ToolTipPanel.GetComponentInChildren<Text>().text =
                "";
        }
    }
}
