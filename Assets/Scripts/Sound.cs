using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour
{
    public SoundBehaviour SoundBehaviour = SoundBehaviour.playOnlyOneTime;
    public Dimension inWhichDimensionShouldThisSoundBeIn;
    private DimensionHandler DimensionHandler;
    private bool soundAlreadyPlayed= false;


    void Start()
    {
        DimensionHandler = GameObject.FindGameObjectWithTag("DimensionHandler").gameObject.GetComponent<DimensionHandler>();
    }


void OnTriggerEnter(Collider _col)
    {
        if (_col.tag.Equals("Player"))
        {
            if (!GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
            if (SoundBehaviour.Equals(SoundBehaviour.playOnlyOneTime))
            {
                this.soundAlreadyPlayed = true;
                GetComponent<SphereCollider>().enabled = false;
            }
           }
        }
    }

    void Update()
    {
        if (!inWhichDimensionShouldThisSoundBeIn.Equals(DimensionHandler.GetCurrentDimension()))
        {
            GetComponentInChildren<SphereCollider>().enabled = false;
        }
        else
        {
            if (soundAlreadyPlayed)
            {
                GetComponent<SphereCollider>().enabled = false;
            }
            else
            {
                GetComponent<SphereCollider>().enabled = true;
            }
        }
    }
}
