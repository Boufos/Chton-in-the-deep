using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Item")]
public class AssetItem : ScriptableObject, IItem
{
    public Sprite UIIcon => _uIIcon;
    public string Name => _name;

    [SerializeField] private Sprite _uIIcon;
    [SerializeField] private string _name;
}
