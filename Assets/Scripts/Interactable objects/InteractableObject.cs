using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterReaction))]
public class InteractableObject : MonoBehaviour,IObject
{
    public RectMenu MenuPrefab;
    [HideInInspector]
    public Collider2D Collider;
    public List<AssetItem> Items;
    public bool IsActive;

    protected CharacterReaction _reactions;
    protected Inventory _invetory => Inventory.Instance;
    protected DialogPanel _dialogPanel;
    [SerializeField]
    protected Hero _hero;
    [SerializeField]
    protected float toPlayerDistanceLimit;
    protected RaycastHit2D _ray;
    protected RectMenu _menu;
    [SerializeField]
    protected int _layerMask;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        _reactions = GetComponent<CharacterReaction>();
        _hero = FindObjectOfType<Hero>();
        _layerMask = (1 << _hero.gameObject.layer) | (1 << LayerMask.NameToLayer("Ground"));
    }
    virtual public void Look()
    {
        //Íàâåðíîå íóæíî, ÷òî - òî ñêàçàòü
       // _reactions.Reaction(_reactions.LookingPhrase);
    }
    virtual public void Interact()
    {

        //Íàâåðíîå íóæíî, ÷òî - òî ñêàçàòü
    }

    protected void OnMouseDown()
    {

        _ray = GetToPlayerPlayerRaycast();
        if (IsOnPlayer() && _ray.distance < toPlayerDistanceLimit)
        {
            EnableRectMenu();
        }
        else
        {
            _reactions.Reaction("Íå ìîãó. Ñëèøêîì äàëåêî. ");
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
        return IsNeedLayer(_ray, _hero.gameObject.layer);
    }
    private RaycastHit2D GetToPlayerPlayerRaycast()
    {

        var heading = _hero.transform.position - transform.position;
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
