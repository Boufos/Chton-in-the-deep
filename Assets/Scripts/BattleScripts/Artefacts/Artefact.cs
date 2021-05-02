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

    public State StateArtefact = State.Await;

    public string Type;

    public bool IsFriendly;

    [HideInInspector] public GameObject Target;


    [SerializeField, Min(1)] public float _maxHealth;
    [SerializeField, Range(0, 5)] public float _speedEffect = 1;
    [SerializeField, Min(1)] private float _timeWork;
    [SerializeField, Min(1)] private float _timeSpawned;

    [SerializeField] private LayerMask _playerLayer;

    private ArtefactSpawner _artefactSpawner;

    private float _timer;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        if (IsFriendly)
        {
            _artefactSpawner = FindObjectOfType<ArtefactSpawner>();
            _artefactSpawner.Spawn += Spawn;
            _artefactSpawner.Fronted += Fronted;
            this.gameObject.SetActive(false);
        }
        else
        {
            foreach (var item in FindObjectsOfType<ArtefactSpawner>())
            {
                if (item._isEnemy)
                {
                    _artefactSpawner = item;
                }
            }
        }
    }

    private void Update()
    {
        if (IsFriendly)
        {
            if (_timer > _timeSpawned && StateArtefact == State.Spawned)
            {
                _artefactSpawner.Spawn += Spawn;
                StateArtefact = State.Await;
                _timer = 0;
                this.gameObject.SetActive(false);
            }

            if (_timer > _timeWork && StateArtefact == State.Fronted)
            {
                StateArtefact = State.Await;
                _timer = 0;
                this.gameObject.SetActive(false);
                _artefactSpawner.Fronted += Fronted;
                _artefactSpawner.Spawn += Spawn;
            }

            if (StateArtefact != State.Await)
            {
                _timer += Time.deltaTime;
            }
        }
    }
    
    private int Fronted(Vector3 position)
    {
        if (StateArtefact == State.Fronted)
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

    public void Spawn(string index, Vector3 position)
    {
        if (this.name == index && StateArtefact == State.Await)
        {
            this.gameObject.SetActive(true);
            transform.position = position;
            if(IsFriendly)
            {
                StateArtefact = State.Spawned;
                _timer = 0;
                _artefactSpawner.Spawn -= Spawn;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet _bullet;

        if (collision.gameObject.layer == 11)
        {
               StateArtefact = State.Fronted;
            _timer = 0;
            _artefactSpawner.MoveToFront();
        }

        if (collision.gameObject.TryGetComponent<Bullet>(out _bullet))
        {
            Bullet[] _childrens = GetComponentsInChildren<Bullet>();
            foreach (var item in _childrens)
            {
                if(item == _bullet)
                {
                    return;
                }
            }

            _currentHealth -= _bullet.Damage;
            if (_currentHealth < 1)
            {
                _artefactSpawner.DisactivatedArtefact();
                _timer += _timeWork;
                _currentHealth = _maxHealth;
                _bullet.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}

