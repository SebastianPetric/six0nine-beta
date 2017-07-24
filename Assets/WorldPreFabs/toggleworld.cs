using UnityEngine;
using System.Collections;

public class toggleworld : MonoBehaviour
{
    private GameObject testmest;
    private GameObject testmest1;

    //private GameObject[] level1;
    //private GameObject[] level2;

    // Use this for initialization
    void Start()
    {

        testmest = GameObject.FindGameObjectWithTag("Level1");
        testmest1 = GameObject.FindGameObjectWithTag("Level2");
        Debug.Log("Start");
        Debug.Log(testmest);
        testmest.SetActive(true);
        testmest1.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Input");
            testmest.SetActive(true);
            testmest1.SetActive(false);

        }
        else
        {
            testmest.SetActive(false);
            testmest1.SetActive(true);

        }
        
    }
}

