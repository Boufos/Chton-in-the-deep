using System.Collections.Generic;
using UnityEngine;

public class ArtefactSpawner : MonoBehaviour
{

    [HideInInspector] public delegate void SpawnHendler(int index, Vector3 position);
    [HideInInspector] public event SpawnHendler Spawn;
    [HideInInspector] public delegate int FrontHendler( Vector3 position);
    [HideInInspector] public event FrontHendler Fronted;

    [SerializeField, Min(1)] private int _countArtefacts;
    [SerializeField, Min(1)] private int _countArtefactSpawned;
    [SerializeField] private float _timeSpawn;

    [SerializeField] private GameObject[] _artefactTypes;

    [SerializeField] private GameObject _front;
    [SerializeField] private GameObject _spawner;

    private GameObject[] _artefacts;


    [SerializeField]private List<Vector3> _pointFront = new List<Vector3>();
    private List<Vector3> _pointSpawn = new List<Vector3>();

    private float _timer;

    private int _currentIndexFront;
    private int CurrentIndexFront
    {
        get
        {
            return _currentIndexFront;
        }
        set
        {
            if (value == _pointFront.Count)
            {
                _currentIndexFront = 1;
            }
            else
            {
                _currentIndexFront = value;
            }
        }
    }


    private void Awake()
    {
        CurrentIndexFront = 1;

        _artefacts = new GameObject[_countArtefacts];

        for (int i = 0; i < _countArtefacts; i++)
        {
            _artefacts[i] = GameObject.Instantiate(_artefactTypes[i % _artefactTypes.Length]);
            _artefacts[i].name = $"{i}";
        }

        foreach (var item in _front.GetComponentsInChildren<Transform>())
        {
            _pointFront.Add(new Vector3(item.position.x, item.position.y, item.position.z - 1));
        }

        foreach (var item in _spawner.GetComponentsInChildren<Transform>())
        {
            _pointSpawn.Add(new Vector3(item.position.x, item.position.y, item.position.z - 1));
        }
    }

    private void Update()
    {
        if (_timer > _timeSpawn)
        {
            SpawnArtefact();
            _timer = 0;
        }
        _timer += Time.deltaTime;
        
    }

    public void MoveToFront()
    {
        Fronted.Invoke(_pointFront[CurrentIndexFront]);
        CurrentIndexFront++;
    }

    private void SpawnArtefact()
    {
        int[] _currentPoints = new int[_countArtefactSpawned];
        bool _isDifPoint = false;
        int indexA, indexB = 0;
        for (int i = 0; i < _countArtefactSpawned; i++)
        {

            while (!_isDifPoint)
            {
                indexB = Random.Range(0, _pointSpawn.Count);

                for (int j = 0; j <= i; j++)
                {
                    if (indexB == _currentPoints[j])
                    {
                        _isDifPoint = false;
                        break;
                    }
                    _isDifPoint = true;
                }
            }

            _isDifPoint = false;
            int _count = 0;
            indexA = Random.Range(0, _countArtefacts);
            _currentPoints[i] = indexB;

            Spawn?.Invoke(indexA, _pointSpawn[_currentPoints[i]]);
        }

    }
}

