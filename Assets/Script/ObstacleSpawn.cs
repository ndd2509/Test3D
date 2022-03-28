using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] ObstaclePrefs;
    public float spawnTime = 1f;
    private float timer = 0;
 

    // Update is called once per frame
    void Update()
    {
        if(timer > spawnTime){
            int rand = Random.Range(0, ObstaclePrefs.Length);
            GameObject obs = Instantiate(ObstaclePrefs[rand]);
            obs.transform.position = transform.position + new Vector3(-4.136203f,-0.4f,23.01679f) ;
            Destroy(obs,15);
            timer = 0;
        }
        timer += Time.deltaTime;
        Debug.Log("Player");
        
    }
}
