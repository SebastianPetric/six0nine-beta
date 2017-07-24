using UnityEngine;
using System.Collections;

public class blatt : MonoBehaviour {
    private bool turn = false;
    private float max, min;

    // Use this for initialization
    void Start () {
        min = gameObject.transform.position.z - 0.15f;
        max = gameObject.transform.position.z + 0.15f;
        //Debug.Log("StartPos: " + gameObject.transform.position.z);
        //Debug.Log("min: " + min);
        //Debug.Log("max: " + max);

    }
    // Update is called once per frame
    void Update()
    {
        if (turn == false)
        {
           // Debug.Log("AktuellePos: " + gameObject.transform.position.z);
            if (gameObject.transform.position.z < max)
            {
                gameObject.transform.Translate(0, -0.5f * Time.deltaTime, 0.2f * Time.deltaTime);
            }
            else
            {
                turn = true;
            }
        }
        else
        {
            //Debug.Log("Hier");
            if (gameObject.transform.position.z > min)
            {
                gameObject.transform.Translate(0, -0.5f * Time.deltaTime, -0.2f * Time.deltaTime);
            }
            else
            {
                turn = false;
            }
        }



    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Kollidiert mit Player");
            gameObject.GetComponent<AudioSource>().Play();

        }

        Destroy(gameObject);
    }


}
