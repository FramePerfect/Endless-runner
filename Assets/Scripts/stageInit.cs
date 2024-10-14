using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageInit : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject[] collectableList;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spawn in spawnLocations)
        {
            var NewSpawn = Instantiate(collectableList[Random.Range(0,collectableList.Length)]);
            NewSpawn.transform.position = spawn.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
