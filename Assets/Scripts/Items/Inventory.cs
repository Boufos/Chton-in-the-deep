using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<AssetItem> _items;
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    public static Inventory Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        //  gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        Render(_items);
    }
    private void Render(List<AssetItem> items)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
        _items.ForEach(item =>
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Render(item);
        });
    }
    private void Render(AssetItem item)
    {
        var cell = Instantiate(_inventoryCellTemplate, _container);
        cell.Render(item);
    }
    public void AddItem(AssetItem item)
    {
        _items.Add(item);
        Render(item);
    }
    public void RemoveItem(AssetItem item)
    {
        _items.Remove(item);
        Render(_items);
    }
    public bool IsContaineItem(AssetItem item)
    {
        return _items.Contains(item);
    }
}
