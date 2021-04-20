using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public Transform SpawnPoint;
    public int MeleeEnemyCount = 5;
    public int RangeEnemyCount;
    public GameObject MeleeEnemyPrefab;
    public GameObject RangeEnemyPrefab;
    [SerializeField] private List<GameObject> EnemysArray;

    private void Awake()
    {
        FillTheArray(MeleeEnemyCount, MeleeEnemyPrefab);
        FillTheArray(RangeEnemyCount, RangeEnemyPrefab);

    }

    private void FillTheArray( int arrayCount, GameObject prefab)
    {
        for (int i = 0; i < arrayCount; i++)
        {
            EnemysArray.Add(Instantiate(prefab, SpawnPoint.position, transform.rotation));
        }
    }



}
