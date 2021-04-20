using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGenerator:MonoBehaviour
{
    private Level _level => Level.Instance;
    public List<Tile> UsedTiles = new List<Tile>();
    public List<GameObject> CommonTiles;
    public TilePulls StartPull;
    public GameObject FinsihTile;
    public Vector3 TilePosition;
    void Start()
    {

        CommonTiles = new List<GameObject>(StartPull.Tiles);
        int index = Random.Range(0, StartPull.Tiles.Count);
        _level.CurrentTile = Instantiate(CommonTiles[index], _level.transform).GetComponent<Tile>();
        UsedTiles.Add(_level.CurrentTile);
        _level.CurrentTile.transform.position = _level.transform.position;
        Destroy(_level.CurrentTile.Entrance.gameObject);
        CommonTiles.RemoveAt(index);
        Destroy(_level.CurrentTile.Entrance);

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetNewTile(Tile currentTile)
    {
        var CurrentTilePull = currentTile.Pull.Tiles;

        do
        {
            int index = Random.Range(0, currentTile.Pull.Tiles.Count);
            var newTile = currentTile.Pull.Tiles[index].GetComponent<Tile>();
            if (IsNotUsed(newTile))
            {

                Tile pastTile = _level.CurrentTile;
                _level.CurrentTile = Instantiate(currentTile.Pull.Tiles[index], _level.transform).GetComponent<Tile>();
                UsedTiles.Add(_level.CurrentTile);
                _level.CurrentTile.SetPastTile(pastTile);
                CommonTiles.Remove(currentTile.Pull.Tiles[index]);
                pastTile.NextTile = _level.CurrentTile;
                SetPosition(pastTile.transform.position, _level.CurrentTile);
                return;
            }
            else
            {
                CurrentTilePull.Remove(currentTile.Pull.Tiles[index]);
            }
        } while (CurrentTilePull.Count > 0);

        SetFinish();
    }
    public void SetFinish()
    {
        Tile pastTile = _level.CurrentTile;
        _level.CurrentTile = Instantiate(FinsihTile, _level.transform).GetComponent<Tile>();
        _level.CurrentTile.PastTile = pastTile;
    }

    private void SetPosition(Vector3 pastTilePosition, Tile tile)
    {
        tile.transform.position = pastTilePosition + TilePosition;
    }
    private bool IsNotUsed(Tile tile)
    {
        for (int i = 0; i < UsedTiles.Count; i++)
        {
            if (UsedTiles[i].ID == tile.ID)
            {
                return false;
            }
        }
        return true;
    }

}
