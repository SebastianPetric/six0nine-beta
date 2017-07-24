using UnityEngine;
using System.Collections;

public class toggle1 : MonoBehaviour
{
    private float rand1, rand2;
    private float intens = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        intens++;
        rand1 = Random.Range(0, 100);
        if (rand1 > 80 && intens > 50)
        {
            gameObject.GetComponent<Light>().intensity = 0;
            intens = 0;
        }
        else if (rand1 < 20 && intens > 50)
        {
            gameObject.GetComponent<Light>().intensity = 2.5f;
            intens = 0;

        }


    }
}
