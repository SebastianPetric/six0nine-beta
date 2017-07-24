using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private GameObject ToolTipPanel;
    private GameObject ChangeDimensionsPanel;
    private TriggerHandler TriggerHandler;
    private InventoryHandler InventoryHandler;
    private DimensionHandler DimensionHandler;
    private Text TargetedItemText;
    private GameObject ParticleEffectChangeDimensions;
    private Ray ray;
    private RaycastHit hit;
    private SpawnPoints PossibleSpawnPoints;

    //anvisiertes Item
    public Collider ItemLookedAt;

    //Min und Max des Field of Views
    float minFov = 30f;
    float maxFov;

    //Schnelligkeit mit der die Effekte zu- und abnehmen
    float sensitivity = 20f;
    float alphaSensitivity = 0.7f;

    //Durch diesen Wert wird gecheckt, ob der Effekt getriggert wurde--> Effekt kann dadurch auch wieder abnehmen
    private bool isGoingBack=false;

    void Start ()
    {
        PossibleSpawnPoints = GameObject.FindGameObjectWithTag("SpawnPoints").GetComponent<SpawnPoints>();
        TargetedItemText = GameObject.FindGameObjectWithTag("TargetedItemText").GetComponent<Text>();
        maxFov = gameObject.GetComponentInChildren<Camera>().fieldOfView;
        InventoryHandler = GameObject.FindGameObjectWithTag("InventoryHandler").gameObject.GetComponent<InventoryHandler>();
        TriggerHandler = GameObject.FindGameObjectWithTag("TriggerHandler").gameObject.GetComponent<TriggerHandler>();
        DimensionHandler = GameObject.FindGameObjectWithTag("DimensionHandler").gameObject.GetComponent<DimensionHandler>();
        ItemLookedAt = GetComponent<Collider>();
        ParticleEffectChangeDimensions = GameObject.FindGameObjectWithTag("ParticleEffectChangeDimensions").gameObject;
        ToolTipPanel = GameObject.FindGameObjectWithTag("ToolTipPanel").gameObject;
        ChangeDimensionsPanel = GameObject.FindGameObjectWithTag("ChangeDimensionsPanel").gameObject;
    }

	void Update () {
	    if (TriggerHandler.getIsActive())
	    {
            //Effect for Changing between Dimension
            changeDimensions();
	    }
        ray = gameObject.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0F))
        {
            if (hit.transform.gameObject.tag.Equals("ItemToSelect"))
            {
                TargetedItemText.text = hit.transform.parent.gameObject.GetComponent<ItemToSelect>().name;
                //Hier Text angezeigt um was für ein Item es sich handelt
            }

            else if (hit.transform.tag.Equals("Switch"))
            {
                TargetedItemText.text = hit.transform.gameObject.GetComponent<Switch>().name;
                //Hier Text angezeigt um was für ein Item es sich handelt
            }
            else
            {
                TargetedItemText.text = "";
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100.0F))
            {
                if (hit.transform.tag.Equals("ItemToSelect"))
                {
                    hit.transform.gameObject.GetComponent<AudioSource>().enabled = true;
                    InventoryHandler.addItemInInventory(hit.transform.parent.gameObject);
                   hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                   hit.transform.parent.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                   hit.transform.parent.gameObject.GetComponent<Light>().enabled = false;
                    hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                    TargetedItemText.text = "";
                }
                else
                {
                    //Hier wird ein Sound abgespielt wenn man kein Item anklickt dass man einsammeln kann
                }

                if (hit.transform.tag.Equals("Switch"))
                {
                    if (InventoryHandler.checkIfAllItemsAreInInventory())
                    {
                        Debug.Log(hit.transform.Find("WholeDisk").tag);
                        hit.transform.Find("WholeDisk").GetComponent<MeshRenderer>().enabled = true;
                        hit.transform.Find("WholeDisk").GetComponent<AudioSource>().Play();
                        hit.transform.GetComponent<BoxCollider>().enabled = false;
                        TriggerHandler.setIfBridgeCanBeActivated(true);
                    }
                    else
                    {
                        //Wenn man auf den Schalter drückt, und man NICHT alle nötigen Items gesammelt hat dann wird irgend ein Sound abgespielt
                        if (!hit.transform.gameObject.GetComponent<AudioSource>().isPlaying)
                        {
                            hit.transform.gameObject.GetComponent<AudioSource>().Play();
                        }
                        
                    }
                }
                else
                {
                    TargetedItemText.text = "";
                }
            }
        }
        // Hier soll mit dem Tastendruck auf I das Inventar angezeigt werden
	    if (Input.GetKeyDown(KeyCode.I))
	    {
	        InventoryHandler.checkIfAllItemsAreInInventory();
	    }
	}

    public void changeDimensions()
    {
        if (!isGoingBack)
        {
            float fov = gameObject.GetComponentInChildren<Camera>().fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            gameObject.GetComponentInChildren<Camera>().fieldOfView = fov;
			Debug.Log(fov);

            Color tempPanelColor = ChangeDimensionsPanel.GetComponent<Image>().color;
            tempPanelColor.a += Input.GetAxis("Mouse ScrollWheel") * -alphaSensitivity;
            tempPanelColor.a = Mathf.Clamp(tempPanelColor.a, 0, 100);
            ChangeDimensionsPanel.GetComponent<Image>().color = tempPanelColor;


            Color tempParticleChangeDimensionsColor =
            ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor;
            tempParticleChangeDimensionsColor.a += Input.GetAxis("Mouse ScrollWheel") * -alphaSensitivity;
            tempParticleChangeDimensionsColor.a = Mathf.Clamp(tempParticleChangeDimensionsColor.a, 0, 100);
            ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor = tempParticleChangeDimensionsColor;

           
            //später mit Field of View == 20 vergleichen
			if (fov == 30)
            {
                if (DimensionHandler.GetCurrentDimension().Equals(Dimension.imagination))
                {    
                    //Hier alles ausblenden was in der Dimension Realität ist
                    DimensionHandler.setCurrentDimension(Dimension.reality);
                    gameObject.transform.position = PossibleSpawnPoints.getNearestPositionToSpawn(gameObject.transform).position + new Vector3(0,1,0);
                }
                else
                {
                    
                    //Hier alles ausblenden was in der Dimension Imagination ist
                    DimensionHandler.setCurrentDimension(Dimension.imagination);
                }
                isGoingBack = true;
                TriggerHandler.setEffectWasTriggeredFirstTime(true);
            }
        }
        else
        {
            float fov = gameObject.GetComponentInChildren<Camera>().fieldOfView;
            fov -= 0.01f * -sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            gameObject.GetComponentInChildren<Camera>().fieldOfView = fov;

            Color tempPanelColor = ChangeDimensionsPanel.GetComponent<Image>().color;
            tempPanelColor.a -= 0.01f * alphaSensitivity;
            tempPanelColor.a = Mathf.Clamp(tempPanelColor.a, 0, 100);
            ChangeDimensionsPanel.GetComponent<Image>().color = tempPanelColor;
            if (ChangeDimensionsPanel.GetComponent<Image>().color.a == 0)
            {
                isGoingBack = false;
            }
            Color tempParticleChangeDimensionsColor =
            ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor;
            tempParticleChangeDimensionsColor.a -= 0.01f * alphaSensitivity;
            tempParticleChangeDimensionsColor.a = Mathf.Clamp(tempParticleChangeDimensionsColor.a, 0, 100);
            ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor =
            tempParticleChangeDimensionsColor;
        }
    }
}
