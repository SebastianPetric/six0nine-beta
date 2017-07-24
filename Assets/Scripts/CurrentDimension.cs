using UnityEngine;
using System.Collections;

public class CurrentDimension : MonoBehaviour {
    private DimensionHandler DimensionHandler;
    private GameObject dimensionImag;
    private GameObject dimensionReal;
    public Dimension currentDimension;
    void Start () {
        DimensionHandler = GameObject.FindGameObjectWithTag("DimensionHandler").gameObject.GetComponent<DimensionHandler>();
        dimensionImag = GameObject.FindGameObjectWithTag("DimensionImag");
        dimensionReal = GameObject.FindGameObjectWithTag("DimensionReal");

    }
	
	void Update () {

	    if (DimensionHandler.GetCurrentDimension().Equals(dimensionImag.GetComponent<CurrentDimension>().currentDimension))
	    {
            dimensionImag.SetActive(true);
            dimensionReal.SetActive(false);
	    }
	    else
	    {
            dimensionImag.SetActive(false);
            dimensionReal.SetActive(true);
        }
	}
}
