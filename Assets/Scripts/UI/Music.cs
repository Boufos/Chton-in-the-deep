using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> Clips;
    AudioSource _source;
    int ClipId;
    public static Music Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Object.DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = Clips[ClipId++];
        _source.Play(); 
    }
    private void Update()
    {

        if (_source.time >= _source.clip.length - 1)
        {
            if (ClipId >= Clips.Count)
            {
                ClipId = 0;
            }
            _source.clip = Clips[ClipId++];
            _source.Play();
        }

    }
}
