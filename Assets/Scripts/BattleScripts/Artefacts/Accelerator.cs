using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    [SerializeField] private float accelerate;

    private Artefact _artefact;

    private bool _flag = false;

    private void Awake()
    {
        _artefact = GetComponent<Artefact>();
    }
    

}
