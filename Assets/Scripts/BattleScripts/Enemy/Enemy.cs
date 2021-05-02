using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Data", order = 51)]
public class Enemy : ScriptableObject
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public GameObject[] Waves;

    [SerializeField]
    public int[] WaveCounters;
}
