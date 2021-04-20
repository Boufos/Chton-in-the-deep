using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(EnemyPrefab, SpawnPoint.position, transform.rotation);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
