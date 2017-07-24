using UnityEngine;
using System.Collections;

public class BlattRandom : MonoBehaviour
{
    private int rand1, rand2;
    private float intens = 0;
    private GameObject[] ArrayOfPositions;
    public GameObject blatt;

    // Use this for initialization
    void Start()
    {

        //blatt = gameObject.GetComponentsInChildren<Transform>();
        ArrayOfPositions = GameObject.FindGameObjectsWithTag("PossiblePosition");


       





    }

    // Update is called once per frame
    void Update()
    {
        intens++;
        rand1 = Random.Range(0, 4);
        if (intens > 100)
        {
            Instantiate(blatt, ArrayOfPositions[rand1].transform.position, Quaternion.identity);
            intens = 0;
        }



        //rand1 = Random.Range(0, 4);
        //Instantiate(blatt, ArrayOfPositions[rand1].transform.position, Quaternion.identity);
        ////Debug.Log(blatt[rand1].transform.position);






    }
}
