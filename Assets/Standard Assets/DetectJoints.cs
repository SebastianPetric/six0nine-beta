using UnityEngine;
using System.Collections;
using Windows.Kinect;


public class DetectJoints : MonoBehaviour
{

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
    void Start()
    {
        
        defaultZoom = gameObject.GetComponent<Camera>().fieldOfView;
        if (BodySrcManager == null)
        {
            Debug.Log("Assign Game Object with Body Source Manager");
        }
        else
        {
            Debug.Log("Body is tracked");
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
                
                var pos = body.Joints[TrackedJoint].Position;
                Debug.Log(pos);
                float moveHorizontal = Input.GetAxisRaw("Horizontal");
                float moveVeritcal = Input.GetAxisRaw("Vertical");
                //Vector3 movement;
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

                Debug.Log(pos.Z);
                //Zoom
                Debug.Log(pos.Z);
                if (pos.Z > 1.0f && pos.Z < 2.0f)
                {

                    if (zoomInterval != 0)
                    {
                        gameObject.GetComponent<Camera>().fieldOfView += 1;
                        // Debug.Log(zoomInterval + " zoominterval");
                        zoomInterval--;
                    }
                    else
                    {
                        gameObject.GetComponent<Camera>().fieldOfView = defaultZoom;
                    }
                    //
                    //zoomInterval = 0;
                    // Debug.Log(1);
                }
                if (pos.Z < 1.0f)
                {
                    if (gameObject.GetComponent<Camera>().fieldOfView > 30)
                    {
                        gameObject.GetComponent<Camera>().fieldOfView -= zoomfactor;
                        zoomInterval++;
                        Debug.Log(zoomInterval + " zoominterval");
                    }

                    // Debug.Log(2);
                }

                //gameObject.transform.position = new Vector3(pos.X*10, 0f, pos.Z*10);
            }
        }
    }
}
