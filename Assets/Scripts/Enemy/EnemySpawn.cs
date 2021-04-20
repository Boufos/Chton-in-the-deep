using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemySpawnPoints;
    [SerializeField] private GameObject _hero;
    [SerializeField] private bool[] _readyEnemy; 
    public Transform SpawnPoint;
    public int MeleeEnemyCount;
    public int RangeEnemyCount;
    public GameObject MeleeEnemyPrefab;
    public GameObject RangeEnemyPrefab;
    private RangeEnemy _gameObjectRangeEnemy;
    [SerializeField] private List<GameObject> _enemiesArray;
    [SerializeField] private int _maxEnemy = 10;

    private int _currentSpawns = 0;
    DateTime _timeNow;

    // Start is called before the first frame update
    private void Start()
    {
        _timeNow = DateTime.Now;
        _readyEnemy = new bool[MeleeEnemyCount + RangeEnemyCount];
        for (int i = 0; i < _readyEnemy.Length; i++)
        {
            _readyEnemy[i] = true;
        }
        FillTheArray(MeleeEnemyCount, MeleeEnemyPrefab);
        FillTheArray(RangeEnemyCount, RangeEnemyPrefab);
    }

    private void Update()
    {
        if (_currentSpawns < _maxEnemy && GetTotalSeconds(DateTime.Now) - GetTotalSeconds(_timeNow) > 1.4)
        {
            SpawnEnemy();
            _timeNow = DateTime.Now;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void FillTheArray(int arrayCount, GameObject prefab)
    {
        for (int i = 0; i < arrayCount; i++)
        {
            _enemiesArray.Add(Instantiate(prefab, SpawnPoint.position, transform.rotation));
            prefab.TryGetComponent<RangeEnemy>(out _gameObjectRangeEnemy);
            if (_gameObjectRangeEnemy != null)
            {
                prefab.GetComponent<RangeEnemy>()._player = _hero.transform;
            }
            else
            {
                prefab.GetComponent<MeleeEnemy>()._player = _hero.transform;
            }
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < _readyEnemy.Length; i++)
        {
            if (_readyEnemy[i])
            {
                _enemiesArray[i].transform.position = _enemySpawnPoints[UnityEngine.Random.Range(0, _enemySpawnPoints.Count)].transform.position;
                _readyEnemy[i] = false;
                _currentSpawns++;
                return;
            }
        }
    }

    public void ReturnEnemyOnSpawn(string deadEnemy)
    {

        for (int i = 0; i < _readyEnemy.Length; i++)
        {
            string test = _enemiesArray[i].name + "(Clone)";
            if (deadEnemy == test)
            {
                
                _readyEnemy[i] = true;
                _enemiesArray[i].transform.position = transform.position;
                _enemiesArray[i].TryGetComponent<RangeEnemy>(out _gameObjectRangeEnemy);
                if(_gameObjectRangeEnemy != null)
                {
                    _enemiesArray[i].GetComponent<RangeEnemy>()._helth = _enemiesArray[i].GetComponent<RangeEnemy>()._maxHelth;
                }
                else
                {
                    _enemiesArray[i].GetComponent<MeleeEnemy>()._helth = _enemiesArray[i].GetComponent<MeleeEnemy>()._maxHelth;
                }
            }
        }
    }

    private float GetTotalSeconds(DateTime t)
    {
        return t.Second + t.Minute * 60 + t.Hour * 3600;

    }
}
