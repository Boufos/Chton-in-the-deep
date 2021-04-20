using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public OuterPass Exit;
    [HideInInspector]
    public InnerPass Entrance;
    public Tile NextTile;
    public Tile PastTile;
    public TilePulls Pull;
    [HideInInspector]
    public Borders Borders;
    public bool IsGoalAchived;
    public int ID;
    private Hero _player;
    private void Awake()
    {
        Borders = GetComponentInChildren<Borders>();
        Entrance = GetComponentInChildren<InnerPass>();
        Exit = GetComponentInChildren<OuterPass>();
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
 
    public void SetIsActive(bool value)
    {
        if (value)
        {
            Exit.Animation.Play();
        }
        IsGoalAchived = value;
    }
}
