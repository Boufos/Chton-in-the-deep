using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public OuterPass Exit;
    [HideInInspector]
    public InnerPass Entrance;
    public Tile NextTile;
    public Tile PastTile;
    public TilePulls Pull;
    [HideInInspector]
    public Borders Borders;
    private bool _isOuterPassOpen;
    public bool IsGoalAchived
    {
        get => _isOuterPassOpen;
        set
        {
            if (value)
            {
                _isOuterPassOpen = value;
                SetExitActive(value);
            }

        }
    }
    public int ID;
    private Hero _player;
    private void Awake()
    {
        Borders = GetComponentInChildren<Borders>();
        Entrance = GetComponentInChildren<InnerPass>();
       // Exit = GetComponentInChildren<OuterPass>();
        _player = FindObjectOfType<Hero>();
    }

    public void Enter()
    {
        Level.Instance.ToBlackout();
        Level.Instance.CurrentTile = this;
        StartCoroutine(SetCameraPosition());
    }
    public void SetPastTile(Tile tile)
    {
        PastTile = tile;
    }
    public void SetNextTile(Tile tile)
    {
        NextTile = tile;
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    private IEnumerator SetCameraPosition()
    {
        int i = 0;
        do
        { 
            i++;
            yield return new WaitForSeconds(1f);
        } while (i < 0);
        Level.Instance.SetCameraPosition();

    }
    public void SetExitActive(bool isActive)
    {
        if(isActive)
            Exit.gameObject.SetActive(_isOuterPassOpen);
        if (isActive)
        {
            Exit.Animation.Play();
            print("Exit open");
        }
        
    }
 
    public void SetIsActive(bool value)
    {
        IsGoalAchived = value;
    }
}
