using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile pull", menuName = "ScriptableObjects/Tile pull", order = 1)]
public class TilePulls : ScriptableObject
{
    public List<GameObject> Tiles;
}
