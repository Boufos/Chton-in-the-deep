using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactsLine : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    [SerializeField] private int _countPoints;

    private GameObject[] _arrayArtefact;


    private bool[] _busyPoints;
    
    void Start()
    {
        _arrayArtefact = new GameObject[_countPoints];

        _busyPoints = new bool[_countPoints];
        for (int i = 0; i < _countPoints; i++)
        {
            _busyPoints[i] = false;
        }
    }

    public void SetArtefact(GameObject artefact)
    {
        for (int i = 0; i < _countPoints; i++)
        {
            if (!_busyPoints[i])
            {
                Debug.Log(1);
                _arrayArtefact[i] = Instantiate(artefact, _points[i].position, _points[i].rotation);
                _busyPoints[i] = true;
                return;
            }
        }
    }
}
