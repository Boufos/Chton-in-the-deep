using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private float _speedBullet;
    [SerializeField] private float _speedShooting;

    [SerializeField] private int _countBullet;

    private Artefact _artefact;

    private GameObject[] _bullets;

    private float _timer;
    private float _timeShoot;

    private int _currentBullet;

    private void Awake()
    {
        _currentBullet = 0;
        _timer = 0;
        _artefact = GetComponent<Artefact>();
        _bullets = new GameObject[_countBullet];
        for (int i = 0; i < _countBullet; i++)
        {
            _bullets[i] = GameObject.Instantiate(_bullet, transform);
        }

        
    }

    private void Update()
    {
        _timeShoot = 2 / _artefact._speedEffect * _speedShooting;

        if(_timer > _timeShoot)
        {
            Activate();
            _timer = 0;
        }
        _timer += Time.deltaTime; 
    }

    public void Activate()
    {
        _bullets[_currentBullet].GetComponent<Rigidbody2D>().AddForce(Vector2.right * _speedBullet);

        _currentBullet++;

        if (_currentBullet == _countBullet)
        {
            _currentBullet = 0;
            for (int i = 0; i < _countBullet; i++)
            {
                _bullets[i].transform.position = transform.position;
            }
        }
    }
}
