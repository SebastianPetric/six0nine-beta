using UnityEngine;
using System.Collections;
using Windows.Kinect;
using UnityEngine.UI;
using AudioSource = UnityEngine.AudioSource;


public class DetectJoints : MonoBehaviour
{

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

    private bool bodyIsTracked= false;


    public GameObject BodySrcManager;
    public JointType TrackedJoint;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    private Rigidbody rb;
    private float defaultZoom;
    private float zoomInterval = 0;
    float zoomfactor = 1;
    private float threshold = 0.1f;
    private float lastPos_Z = 0;
    public GameObject explosion;

    //Min und Max des Field of Views
    float minFov = 30f;
    float maxFov;
    private float curFov;

    //Schnelligkeit mit der die Effekte zu- und abnehmen
    float sensitivity = 10f;
    float alphaSensitivity = 0.7f;

    //Durch diesen Wert wird gecheckt, ob der Effekt getriggert wurde--> Effekt kann dadurch auch wieder abnehmen
    private bool isGoingBack = false;
    private float cur_pos;
    private bool hasToBeCooledDown = false;
    private int timerToCoolDown = 100;

    void Start()
    {
        PossibleSpawnPoints = GameObject.FindGameObjectWithTag("SpawnPoints").GetComponent<SpawnPoints>();
        TargetedItemText = GameObject.FindGameObjectWithTag("TargetedItemText").GetComponent<Text>();
        maxFov = gameObject.GetComponentInChildren<Camera>().fieldOfView;
        InventoryHandler =
            GameObject.FindGameObjectWithTag("InventoryHandler").gameObject.GetComponent<InventoryHandler>();
        TriggerHandler = GameObject.FindGameObjectWithTag("TriggerHandler").gameObject.GetComponent<TriggerHandler>();
        DimensionHandler =
            GameObject.FindGameObjectWithTag("DimensionHandler").gameObject.GetComponent<DimensionHandler>();
        ParticleEffectChangeDimensions = GameObject.FindGameObjectWithTag("ParticleEffectChangeDimensions").gameObject;
        ToolTipPanel = GameObject.FindGameObjectWithTag("ToolTipPanel").gameObject;
        ChangeDimensionsPanel = GameObject.FindGameObjectWithTag("ChangeDimensionsPanel").gameObject;
        defaultZoom = gameObject.GetComponent<Camera>().fieldOfView;
        if (BodySrcManager == null)
        {
            Debug.Log("Assign Game Object with Body Source Manager");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyManager == null)
        {
            return;
        }
        bodies = bodyManager.GetData();

        if (bodies == null)
        {
            return;
        }
        foreach (var body in bodies)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                bodyIsTracked = body.IsTracked;
                GameObject.FindGameObjectWithTag("HeadTrackedText").gameObject.GetComponent<Text>().text = "";
                cur_pos = body.Joints[TrackedJoint].Position.Z;
                //Debug.Log(pos.Z);

                //if (pos.Z > 1.0f && pos.Z < 1.2f)
                //{
                //    movement = new Vector3(0.0f, 0.0f, 0.0f);
                //    gameObject.GetComponent<Rigidbody>().AddForce(movement);
                //    Debug.Log(1);
                //}
                //if (pos.Z < 1.0f)
                //{
                //    movement = new Vector3(0.0f, 0.0f, +pos.Z);
                //    gameObject.GetComponent<Rigidbody>().AddForce(movement * 1000);
                //    Debug.Log(2);
                //}
                //if (pos.Z > 1.2f)
                //{
                //    movement = new Vector3(0.0f, 0.0f, -pos.Z);
                //    gameObject.GetComponent<Rigidbody>().AddForce(movement * 1000);
                //    Debug.Log(3);
                //}
                //gameObject.transform.position = new Vector3(pos.X*10, 0f, pos.Z*10);
            }
        }


//       if (TriggerHandler.getIsActive())
//        {
//            
//            //Effect for Changing between Dimension
//            if (bodyIsTracked)
//            {  
//                    changeDimensions(cur_pos);
//            }
//        }
    }

    public void changeDimensions(float pos_Z)
    {
        if (isGoingBack==false)
        {
            Debug.Log(timerToCoolDown + " tobecooled");
            if (hasToBeCooledDown != true)
            {
                if (pos_Z < 1.0f)
            {
                GameObject.FindGameObjectWithTag("DimensionChangeSound").GetComponent<AudioSource>().Play();
                Debug.Log("Du hast dich nach vorne bewegt");
                float fov = gameObject.GetComponent<Camera>().fieldOfView;
                fov -= 0.02f*sensitivity;
                fov = Mathf.Clamp(fov, minFov, maxFov);
                gameObject.GetComponent<Camera>().fieldOfView = fov;

                Color tempPanelColor = ChangeDimensionsPanel.GetComponent<Image>().color;
                tempPanelColor.a += 0.02f*alphaSensitivity;
                tempPanelColor.a = Mathf.Clamp(tempPanelColor.a, 0, 255);
                ChangeDimensionsPanel.GetComponent<Image>().color = tempPanelColor;


                Color tempParticleChangeDimensionsColor =
                    ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor;
                tempParticleChangeDimensionsColor.a += 0.02f*alphaSensitivity;
                tempParticleChangeDimensionsColor.a = Mathf.Clamp(tempParticleChangeDimensionsColor.a, 0, 255);
                ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor =
                    tempParticleChangeDimensionsColor;

                //später mit Field of View == 20 vergleichen
                if (gameObject.GetComponent<Camera>().fieldOfView < 45)
                {
                      
                    if (DimensionHandler.GetCurrentDimension().Equals(Dimension.imagination))
                    {
                        //Hier alles ausblenden was in der Dimension Realität ist
                        DimensionHandler.setCurrentDimension(Dimension.reality);
                            gameObject.transform.parent.GetComponent<Transform>().position = PossibleSpawnPoints.getNearestPositionToSpawn(gameObject.transform).position + new Vector3(0, 1, 0);
                        
                        }
                    else
                    {

                        //Hier alles ausblenden was in der Dimension Imagination ist
                        DimensionHandler.setCurrentDimension(Dimension.imagination);
                    }
                    isGoingBack = true;
                    TriggerHandler.setEffectWasTriggeredFirstTime(true);
                        hasToBeCooledDown = true;
                        //TriggerHandler.setEffectWasTriggeredFirstTime(true);
                    }
            }
            }
            else
            {
                timerToCoolDown--;
                if (timerToCoolDown <= 0)
                {
                    hasToBeCooledDown = false;
                    timerToCoolDown = 100;
                }
            }
        }
            else
        {
                float fov = gameObject.GetComponent<Camera>().fieldOfView;
                fov += 0.02f * sensitivity;
                fov = Mathf.Clamp(fov, minFov, maxFov);
                gameObject.GetComponent<Camera>().fieldOfView = fov;

                Color tempPanelColor = ChangeDimensionsPanel.GetComponent<Image>().color;
                tempPanelColor.a -= 0.02f * alphaSensitivity;
                tempPanelColor.a = Mathf.Clamp(tempPanelColor.a, 0, 255);
                ChangeDimensionsPanel.GetComponent<Image>().color = tempPanelColor;
                if (ChangeDimensionsPanel.GetComponent<Image>().color.a == 0)
                {
                    isGoingBack = false;
                }
                Color tempParticleChangeDimensionsColor =
                    ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor;
                tempParticleChangeDimensionsColor.a -= 0.02f * alphaSensitivity;
                tempParticleChangeDimensionsColor.a = Mathf.Clamp(tempParticleChangeDimensionsColor.a, 0, 255);
                ParticleEffectChangeDimensions.GetComponent<ParticleSystem>().startColor =
                    tempParticleChangeDimensionsColor;
        }
        }
    }


