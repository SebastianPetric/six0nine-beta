using UnityEngine;
using System.Collections;

public class DimensionHandler : MonoBehaviour
{
    private Dimension currentDimension= Dimension.imagination;

    public void setCurrentDimension(Dimension currentDimension)
    {
        this.currentDimension = currentDimension;
    }

    public Dimension GetCurrentDimension()
    {
        return this.currentDimension;
    }
}
