using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnNextStage : MonoBehaviour
{

    public GameObject[] TestStagePrefabs;
    public GameObject nextSpawn;
    public GameObject lastSpawn;
    private bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&&!hasSpawned)
        {
            spawnStage();
            hasSpawned = true;
        }
    }

    private void spawnStage()
    {
        int stageToSpawn = Random.Range(0, TestStagePrefabs.Length);
        GameObject spawnedstage = Instantiate(TestStagePrefabs[stageToSpawn]);
        var DesiredSpawn = FindGameObjectUsingTag(spawnedstage, "StageSpawn");
        DesiredSpawn.transform.position = nextSpawn.transform.position;
        //spawnedstage.transform.Find("DesiredSpawn").position = nextSpawn.transform.position;
    }
    private GameObject FindGameObjectUsingTag(GameObject parent, string tag)// there might be a better way to spawn in prefabs and line them up but this is what i've got
    {
        Transform parentTransform = parent.transform;
        

        for(int i = 0; i < parentTransform.childCount;i++)
        {
            if(parentTransform.GetChild(i).gameObject.tag != tag)
            {
                var temp = parentTransform.GetChild(i).gameObject;
                for(int o = 0; o < temp.transform.childCount;o++)
                {
                    if(temp.transform.GetChild(o).gameObject.tag == tag)
                    {
                        Debug.Log("Entered 2nd loop");
                        return temp.transform.GetChild(i).gameObject;
                        
                    }
                    
                }
                temp = temp.transform.GetChild(0).gameObject;
                for(int o = 0; o < temp.transform.childCount;o++)
                {
                    if(temp.transform.GetChild(o).gameObject.tag == tag)
                    {
                        Debug.Log("Entered 3rd loop");
                        return temp.transform.GetChild(i).gameObject;
                        
                    }
                }
                
            }
            else if(parentTransform.GetChild(i).gameObject.tag == tag){
                return parentTransform.GetChild(i).gameObject;
            }
        }
        return null;
    }
}
