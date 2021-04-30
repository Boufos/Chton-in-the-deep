using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity;

public class CreatorWaveEnemy : EditorWindow
{
    private Enemy _enemy;

    private List<GameObject> _arrayCopyWaves = new List<GameObject>();

    private List<List<GameObject>> _waves = new List<List<GameObject>>();

    private Vector2 _scrollVector;

    private string _name;

    private int _fade = 0;

    private int _countWave = 0;

    public int CountWave
    {
        get => _countWave;
        set
        {
            if (value > 0)
            {
                if (value > _countWave)
                {
                    for (int i = 0; i < value - _countWave; i++)
                    {
                        _waves.Add(new List<GameObject>());
                        _waves[_waves.Count - 1].Add(new GameObject());
                    }
                }
                else if (value < _countWave)
                {
                    for (int i = 0; i < _countWave - value; i++)
                    {
                        _waves.Remove(_waves[_waves.Count - 1]);
                    }
                }
            }
            _countWave = value;
        }
    }

    #region Style
    private List<GUILayoutOption> _button = new List<GUILayoutOption>();
    private List<GUILayoutOption> _buttonCopyPaste = new List<GUILayoutOption>();

    private List<GUILayoutOption> _label = new List<GUILayoutOption>();
    #endregion

    [MenuItem("Window/Creator Wave Enemy")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CreatorWaveEnemy));
    }

    private void Awake()
    {
        _button.Add(GUILayout.MaxHeight(20));
        _button.Add(GUILayout.MaxWidth(20));
        _button.Add(GUILayout.MinHeight(20));
        _button.Add(GUILayout.MinWidth(20));

        _buttonCopyPaste.Add(GUILayout.MaxWidth(45));
        _buttonCopyPaste.Add(GUILayout.MinWidth(45));

        _label.Add(GUILayout.MaxWidth(130));

    }

    private void OnGUI()
    {
        if (CountWave > 0 && _fade == 0)
        {
            _fade = 1;
        }

        if (CountWave == 0)
        {
            _fade = 0;
        }


        _scrollVector = GUILayout.BeginScrollView(_scrollVector);
        
        EditorGUILayout.BeginHorizontal();
        _enemy = EditorGUILayout.ObjectField(_enemy, typeof(Object), true) as Enemy;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Количество волн", _label.ToArray());
        if (GUILayout.Button("-", _button.ToArray()))
        {
            CountWave--;
        }
        if (GUILayout.Button("+", _button.ToArray()))
        {
            CountWave++;
        }
        CountWave = EditorGUILayout.IntField(CountWave,GUILayout.Width(92.5f));
        EditorGUILayout.LabelField("     Артефакты", _label.ToArray());
        EditorGUILayout.EndHorizontal();

        if (EditorGUILayout.BeginFadeGroup(_fade) && _waves.Count > 0)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < CountWave; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Волна {i + 1}", _label.ToArray());

                if (GUILayout.Button("Copy", _buttonCopyPaste.ToArray()))
                {
                    _arrayCopyWaves = new List<GameObject>(_waves[i]);
                }
                if (GUILayout.Button("Paste", _buttonCopyPaste.ToArray()) && _arrayCopyWaves.Count > 0)
                {
                    _waves[i] = new List<GameObject>(_arrayCopyWaves);
                }

                if (GUILayout.Button("-", _button.ToArray()) && _waves[i].Count > 1)
                {
                    _waves[i].RemoveAt(_waves[i].Count - 1);
                }
                if (GUILayout.Button("+", _button.ToArray()))
                {
                    _waves[i].Add(new GameObject());
                }

                for (int j = 0; j < _waves[i].Count; j++)
                {
                    _waves[i][j] = EditorGUILayout.ObjectField(_waves[i][j], typeof(Object), true) as GameObject;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFadeGroup();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save", _buttonCopyPaste.ToArray()))
        {

        }
        EditorGUILayout.EndHorizontal();

        GUILayout.EndScrollView();
    }
}
