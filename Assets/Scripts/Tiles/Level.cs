using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    public Tile CurrentTile;
    public TilesGenerator Generator;
    [HideInInspector]
    public Animation BlackoutAnimation;
    public int MaxTileCount;
    public static int TileCount;
    static public Level Instance;
    public string NextSceneName;
    [SerializeField]
    private GameObject _blackoutPanel;
    private Camera _camera;
    private Hero _player;
    public void Awake()
    {
        BlackoutAnimation = _blackoutPanel.GetComponent<Animation>();
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        _camera = FindObjectOfType<Camera>();
    }
    void Start()
    {
        TileCount = 1;
        _player = FindObjectOfType<Hero>();
        CurrentTile = FindObjectOfType<Tile>();
        _player.transform.position = CurrentTile.Entrance.transform.position;
        CurrentTile.Entrance.gameObject.SetActive(false);
        var position = CurrentTile.transform.position;
        position.z = -40;
        FindObjectOfType<Camera>().transform.position = position;
        ToBlackout("ToBlackoutEnd");
    }
    public void ToBlackout(string name)
    {
        BlackoutAnimation.clip = BlackoutAnimation.GetClip(name);
        ToBlackout();
    }
    public void ToBlackout()
    {
        BlackoutAnimation.Play();
    }
    public void Instantiate()
    {
        if (TileCount < MaxTileCount)
        {
            TileCount++;
            Generator.SetNewTile(CurrentTile);
        }
        else
        {
            Generator.SetFinish();
        }
        CurrentTile.Enter();
    }
    public void SetCameraPosition()
    {
        Vector3 position = CurrentTile.transform.position;
        position.z = -40;
        _camera.transform.position = position;
    }

}
