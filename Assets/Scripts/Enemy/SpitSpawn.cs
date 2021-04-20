using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _spit;
    [SerializeField] private int _countSpit;
    public GameObject[] _spitArray;
    public bool[] _readySpit;
    // Start is called before the first frame update
    void Start()
    {
        _spitArray = new GameObject[_countSpit];
        _readySpit = new bool[_spitArray.Length];
        for (int i = 0; i < _spitArray.Length; i++)
        {
            _readySpit[i] = true;
            _spitArray[i] = Instantiate<GameObject>(_spit);
            _spitArray[i].transform.position = transform.position;
        }
    }

    public GameObject TakeSpit()
    {
        Debug.Log(_readySpit.Length);
        for (int i = 0; i < _countSpit; i++)
        {
            Debug.Log(_readySpit[i]);
            if (_readySpit[i])
            {
                _readySpit[i] = false;
                return _spitArray[i];
            }
        }
        return null;
    }

}
