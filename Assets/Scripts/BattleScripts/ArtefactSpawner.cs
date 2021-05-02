using System.Collections.Generic;
using UnityEngine;

public class ArtefactSpawner : MonoBehaviour
{
    [HideInInspector] public delegate void SpawnHendler(string index, Vector3 position);
    [HideInInspector] public event SpawnHendler Spawn;
    [HideInInspector] public delegate int FrontHendler(Vector3 position);
    [HideInInspector] public event FrontHendler Fronted;

    public GameObject Player;
    public GameObject BattleWindow;

    public int _countArtefacts;
    public int _countArtefactSpawned;
    public float _timeSpawn;

    public GameObject[] _artefactTypes;

    public GameObject _front;
    public GameObject _spawner;

    public bool _isEnemy;

    public Enemy _enemy;

    public int _indexNames;

    private List<GameObject> _artefacts = new List<GameObject>();

    private List<Vector3> _pointFront = new List<Vector3>();
    private List<Vector3> _pointSpawn = new List<Vector3>();

    private float _timer;


    private int _currentIndexFront;
    private int _currentWave = 0;
    private int _countFrontedArtefact = 0;
    private int _countArtefactActivated = 0;
    private int _countArtefactDisActivated = 0;
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

        if (_isEnemy)
        {
            _countArtefactActivated = _enemy.WaveCounters[_currentWave];
        }
        CurrentIndexFront = 1;

        if (_isEnemy)
        {
            _countArtefacts = _enemy.Waves.Length;
        }

        for (int i = 0; i < _countArtefacts; i++)
        {
            if (_isEnemy)
            {
                _artefacts.Add(GameObject.Instantiate(_enemy.Waves[i]));
                _artefacts[i].name = $"E{i}";
                _artefacts[i].SetActive(true);
                _artefacts[i].GetComponent<Artefact>().IsFriendly = false;
            }
            else
            {
                _artefacts.Add (GameObject.Instantiate(_artefactTypes[i % _artefactTypes.Length]));
                _artefacts[i].name = $"{i}";
                _artefacts[i].GetComponent<Artefact>().IsFriendly = true;
            }
        }

        foreach (var item in _front.GetComponentsInChildren<Transform>())
        {
            _pointFront.Add(new Vector3(item.position.x, item.position.y, item.position.z - 1));
        }
        if(!_isEnemy)
        foreach (var item in _spawner.GetComponentsInChildren<Transform>())
        {
            _pointSpawn.Add(new Vector3(item.position.x, item.position.y, item.position.z - 1));
        }

        _currentIndexFront = 1;
        if (_isEnemy)
        {
            for (int i = _countFrontedArtefact ; i < _countFrontedArtefact + _countArtefactActivated; i++)
            {
                _artefacts[i].transform.position = _pointFront[_currentIndexFront];
                _currentIndexFront++;
            }
        }
    }

    private void Update()
    {
        if (_timer > _timeSpawn && !_isEnemy)
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

            Spawn?.Invoke($"{indexA}", _pointSpawn[_currentPoints[i]]);
        }

    }

    public void DisactivatedArtefact()
    {
        if (_isEnemy)
        {
            _countArtefactDisActivated++;
            if (_countArtefactDisActivated == _countArtefactActivated)
            {
                if (_currentWave < _enemy.WaveCounters.Length - 1)
                {
                    _currentWave++;
                    _countArtefactDisActivated = 0;
                    _countArtefactActivated = _enemy.WaveCounters[_currentWave];

                    for (int i = _countFrontedArtefact + _countArtefactActivated; i < _countFrontedArtefact + _countArtefactActivated + 1; i++)
                    {
                        Debug.Log(i);
                        _artefacts[i].SetActive(true);
                        _artefacts[i].transform.position = _pointFront[_currentIndexFront];
                        _currentIndexFront++;
                    }
                    _countFrontedArtefact += _countArtefactActivated;
                    _currentIndexFront = 0;
                }
                else
                {
                    Player.SetActive(true);
                    BattleWindow.SetActive(false);
                }
            }
        }
    }

    public GameObject GetArtefact(string name)
    {
        GameObject _return;

        foreach (var item in _artefacts)
        {
            if (item.name == name)
            {
                _return = item;
                return _return;
            }
        }
        return null;
    }

    public GameObject GetRandomArtefactInFront(int startIndex, int endIndex)
    {
        GameObject _return = _artefacts[Random.Range(startIndex, endIndex)];

        return _return;
    }
}

