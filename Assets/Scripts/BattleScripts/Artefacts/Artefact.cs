using System;
using UnityEngine;

public class Artefact : MonoBehaviour
{
    public enum State
    {
        Await,
        Spawned,
        Fronted
    }

    public State _state = State.Await;

    public string Type;

    public bool IsFriendly;

    [HideInInspector] public GameObject Target;


    [SerializeField, Min(1)] private float _helth;
    [SerializeField, Range(0, 5)] private float _speedEffect = 1;
    [SerializeField, Min(1)] private float _timeWork;
    [SerializeField, Min(1)] private float _timeSpawned;

    [SerializeField] private LayerMask _playerLayer;

    private ArtefactSpawner _artefactSpawner;

    private float _timer;

    private void Awake()
    {
        _artefactSpawner = FindObjectOfType<ArtefactSpawner>();
        _artefactSpawner.Spawn += Spawn;
        _artefactSpawner.Fronted += Fronted;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_timer > _timeSpawned && _state == State.Spawned)
        {
            _artefactSpawner.Spawn += Spawn;
            _state = State.Await;
            _timer = 0;
            this.gameObject.SetActive(false);
        }

        if (_timer > _timeWork && _state == State.Fronted)
        {
            _artefactSpawner.Fronted += Fronted;
            _artefactSpawner.Spawn += Spawn;
            _state = State.Await;
            _timer = 0;
            this.gameObject.SetActive(false);
        }

        if (_state != State.Await)
        {
            _timer += Time.deltaTime;
        }
    }
    
    private int Fronted(Vector3 position)
    {
        if (_state == State.Fronted)
        {
            transform.position = position;
            _artefactSpawner.Fronted -= Fronted;
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void Spawn(int index, Vector3 position)
    {
        if (this.name == $"{index}" && _state == State.Await)
        {
            this.gameObject.SetActive(true);
            _artefactSpawner.Spawn -= Spawn;
            transform.position = position;
            _state = State.Spawned;
            _timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
               _state = State.Fronted;
            _timer = 0;
            _artefactSpawner.MoveToFront();
        }
    }
}

