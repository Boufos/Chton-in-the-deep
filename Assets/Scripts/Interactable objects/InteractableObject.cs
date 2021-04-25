using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterReaction))]
[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour, IObject
{

    [HideInInspector]
    public Collider2D Collider;
    public bool IsActivated;

    protected CharacterReaction _reactions;
    protected Inventory _invetory => Inventory.Instance;
    protected DialogPanel _dialogPanel => DialogPanel.Instance;
    [SerializeField]
    protected Hero _hero;
    [SerializeField]
    protected float toPlayerDistanceLimit;
    protected RaycastHit2D _ray;
    protected RectMenu _menu;
    [SerializeField]
    protected int _layerMask;
    protected RectMenu MenuPrefab;

    private void Awake()
    {
        MenuPrefab = Resources.Load<RectMenu>("Prefabs/Rect menu");
        Collider = GetComponent<Collider2D>();
        _reactions = GetComponent<CharacterReaction>();
        _hero = FindObjectOfType<Hero>();
        _layerMask = (1 << _hero.gameObject.layer) | (1 << LayerMask.NameToLayer("Ground"));
    }
    virtual public void Look() { }
    virtual public void Interact() { }

    protected void OnMouseDown()
    {
        _ray = GetToPlayerPlayerRaycast(transform.position);
        if (IsOnPlayer() && _ray.distance < toPlayerDistanceLimit)
        {
            EnableRectMenu();
        }
        else
        {
            _reactions.SetReaction("Не могу. Слишком далеко.");
        }

    }
    virtual protected void EnableRectMenu()
    {

        _menu = Instantiate(MenuPrefab, transform.position, MenuPrefab.transform.rotation);
        _menu.Parent = this;
        Collider.enabled = false;
    }
    protected bool IsOnPlayer()
    {   
        if(_ray.collider == null)
        {
            return true;
        }
        return IsNeedLayer(_ray, _hero.gameObject.layer);
    }
    private RaycastHit2D GetToPlayerPlayerRaycast(Vector3 target)
    {

        var heading = _hero.transform.position - target;
        var toPlyerDistance = Vector2.Distance(_hero.transform.position, transform.position);
        var toPlayerDirection = heading / heading.magnitude;
        Debug.DrawRay(transform.position, toPlyerDistance * toPlayerDirection, Color.red);
        return Physics2D.Raycast(transform.position, toPlayerDirection, toPlyerDistance, _layerMask);
    }
    private bool IsNeedLayer(RaycastHit2D raycast, int layer)
    {
        return raycast.collider.gameObject.layer == layer;
    }

}
