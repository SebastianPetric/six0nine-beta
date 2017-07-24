using System;
using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class SpawnPoints : MonoBehaviour
{

    public Transform[] SpawnPointsArray;
    private GameObject[] SpawnPointArray;
    private int SpawnPointsCount;
	void Start ()
	{

	    SpawnPointsCount = GameObject.FindGameObjectsWithTag("SpawnPoint").Length;
        SpawnPointsArray= new Transform[SpawnPointsCount];
        for (int j = 0; j < SpawnPointsCount; j++)
	    {
	        SpawnPointsArray[j] = GameObject.FindGameObjectsWithTag("SpawnPoint")[j].transform;
	    }
	}

    public Transform getNearestPositionToSpawn(Transform _current_position)
    {
        float[] distances = new float[SpawnPointsCount];
        int minimumValueIndex;

        for (int i = 0; i < SpawnPointsCount; i++)
        {
            distances[i] = Vector3.Distance(_current_position.position, SpawnPointsArray[i].position);
        }

        minimumValueIndex = Array.IndexOf(distances, Mathf.Min(distances));

        return SpawnPointsArray[minimumValueIndex];
    }
}
