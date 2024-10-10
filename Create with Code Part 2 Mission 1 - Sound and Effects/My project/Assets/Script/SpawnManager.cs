using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    private Vector3 spawnPos = new Vector3(25, 0.2f, 0);

    private float startDelay = 2.0f;

    private float repeatRate = 2.0f;
    private PlayerController PlayerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        // Creating spawn point for objects that spawn at intervals
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (PlayerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
