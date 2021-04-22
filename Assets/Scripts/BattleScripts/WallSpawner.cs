using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    enum StatePoint
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
    
    [SerializeField] private Transform _upPoint;
    [SerializeField] private Transform _downPoint;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;

    [SerializeField] private GameObject _wall;

    [SerializeField] private int _countWall;

    [SerializeField] private float _forceWall;
    [SerializeField] private float _waitTime;

    private StatePoint _statePoint;

    private GameObject[] _arrayWall;

    private Rigidbody2D[] _arrayRbWall;

    private float _timer;

    private int _currentIndex;

    public int CurrentIndex
    {
        get
        {
            return _currentIndex;
        }
        set
        {
            if(value >= _countWall)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex = value;
            }
        }
    }
    
    private void Start()
    {
        _arrayWall = new GameObject[_countWall];
        _arrayRbWall = new Rigidbody2D[_countWall];
        for (int i = 0; i < _arrayWall.Length; i++)
        {
            _arrayWall[i] = Instantiate(_wall);
            _arrayRbWall[i] = _arrayWall[i].GetComponent<Rigidbody2D>();
        }
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        SpawnWall();
    }

    private void SpawnWall()
    {
        if (_timer > _waitTime)
        {
            CurrentIndex++;
            _timer = 0;
            _statePoint = (StatePoint)Mathf.RoundToInt(Random.Range(0, 4));
            _arrayRbWall[CurrentIndex].velocity = Vector2.zero;
            switch (_statePoint)
            {
                case StatePoint.Up:
                    _arrayWall[CurrentIndex].transform.position = new Vector3(_upPoint.position.x + Mathf.RoundToInt(Random.Range(0, Mathf.Abs(_upPoint.position.x * 2 + 1))), _upPoint.position.y, _upPoint.position.z);
                    _arrayRbWall[CurrentIndex].AddForce(Vector2.down * _forceWall);
                    break;
                case StatePoint.Down:
                    _arrayWall[CurrentIndex].transform.position = new Vector3(_downPoint.position.x + Mathf.RoundToInt(Random.Range(0, Mathf.Abs(_downPoint.position.x * 2 + 1))), _downPoint.position.y, _downPoint.position.z);
                    _arrayRbWall[CurrentIndex].AddForce(Vector2.up * _forceWall);
                    break;
                case StatePoint.Left:
                    _arrayWall[CurrentIndex].transform.position = new Vector3(_leftPoint.position.x, _leftPoint.position.y + Mathf.RoundToInt(Random.Range(0, Mathf.Abs(_leftPoint.position.y * 2 - 1))), _leftPoint.position.z);
                    _arrayRbWall[CurrentIndex].AddForce(Vector2.right * _forceWall);
                    break;
                case StatePoint.Right:
                    _arrayWall[CurrentIndex].transform.position = new Vector3(_rightPoint.position.x, _leftPoint.position.y + Mathf.RoundToInt(Random.Range(0, Mathf.Abs(_leftPoint.position.y * 2 - 1))), _rightPoint.position.z);
                    _arrayRbWall[CurrentIndex].AddForce(Vector2.left * _forceWall);
                    break;
                default:
                    break;
            }
        }
    }
}
