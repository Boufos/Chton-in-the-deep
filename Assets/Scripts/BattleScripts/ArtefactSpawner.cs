using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactSpawner : MonoBehaviour
{
    [SerializeField] private int _countPlaces;
    [SerializeField] private int _countArtefactsSpawn;
    [SerializeField] private int _countArtefacts;
    [SerializeField] private int _waitSpawn;

    [SerializeField] private Transform[] _arrayPlaces;
    
    [SerializeField] private GameObject[] _arrayArtefacts;
    
    private float _timer;

    private bool[] _busyPoint;

    // Start is called before the first frame update
    void Start()
    {

        _busyPoint = new bool[_countPlaces];
        for (int i = 0; i < _countPlaces; i++)
        {
            _busyPoint[i] = false;
        }

        for (int i = 0; i < _countArtefacts; i++)
        {
            _arrayArtefacts[i].SetActive(false);
        }
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer > _waitSpawn / 2)
        {
            for (int i = 0; i < _countArtefacts; i++)
            {
                _arrayArtefacts[i].SetActive(false);
            }
        }

        if(_timer > _waitSpawn)
        {
            _timer = 0;

            for (int i = 0; i < _countArtefactsSpawn; i++)
            {
                int index = Random.Range(0, _countArtefacts);
                int indexPoint = Random.Range(0, _countPlaces);

                while (_busyPoint[indexPoint])
                {
                    indexPoint = Random.Range(0, _countPlaces);
                }

                _arrayArtefacts[index].SetActive(true);
                _arrayArtefacts[index].transform.position = _arrayPlaces[indexPoint].position;
            }
        }
        _timer += Time.deltaTime;
    }
}
