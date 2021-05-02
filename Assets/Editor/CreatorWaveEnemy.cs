using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreatorWaveEnemy : EditorWindow
{
    [SerializeField]
    private Enemy _enemy;
    
    [SerializeField]
    private List<Enemy> _enemies = new List<Enemy>();

    private List<string> _namesEnemies = new List<string>();

    private GameObject _currentObject;

    private List<GameObject> _arrayCopyWaves = new List<GameObject>();

    private List<List<GameObject>> _waves = new List<List<GameObject>>();

    private Vector2 _scrollVector;

    private string _name;
    private string _pathAsset = "Assets/ScriptableObject/Enemy/";

    private int _fade = 0;
    private int _countWave = 0;
    private int _indexCurrentEnemy;

    public int CountWave
    {
        get => _countWave;
        set
        {
            

            if (value >= 0)
            {
                if (value > _countWave)
                {
                    for (int i = 0; i < value - _countWave; i++)
                    {
                        _waves.Add(new List<GameObject>());
                        _waves[_waves.Count - 1].Add(null);
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
            if (value <= 0)
            {
                _countWave = 0;
            }
            else
            {
                _countWave = value;
            }
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
        _name = EditorGUILayout.TextField("Имя", _name); 
        _enemies.Clear();
        _namesEnemies.Clear();
        foreach (var item in Resources.FindObjectsOfTypeAll<Enemy>())
        {
            _enemies.Add(item);
            _namesEnemies.Add(item.Name);
            Debug.Log(item.Name);
        }
        if (_enemies.Count > 0)
        {
            EditorGUILayout.LabelField("Выбрать врага");
            _indexCurrentEnemy = EditorGUILayout.Popup(_indexCurrentEnemy, _namesEnemies.ToArray());
            _enemy = _enemies[_indexCurrentEnemy];
        }
        if (GUILayout.Button("Load", _buttonCopyPaste.ToArray()) && _enemies.Count > 0)
        {
            if(_enemy)
            {
                _name = _enemy.Name;
                Debug.Log(_name);
                _waves.Clear();

                int _currentCountWaves = 0;
                for (int i = 0; i < _enemy.WaveCounters.Length; i++)
                {
                    _waves.Add(new List<GameObject>());
                    for (int j = 0; j < _enemy.WaveCounters[i]; j++)
                    {
                        _waves[i].Add(null);
                        _waves[i][j] = _enemy.Waves[_currentCountWaves];
                        _currentCountWaves++;
                    }
                }

                _countWave = _waves.Count;
            }
        }
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
        CountWave = EditorGUILayout.IntField(CountWave, GUILayout.Width(92.5f));

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
                    DestroyImmediate(_waves[i][_waves[i].Count - 1]);
                    _waves[i].RemoveAt(_waves[i].Count - 1);
                }
                if (GUILayout.Button("+", _button.ToArray()))
                {
                    _waves[i].Add(null);
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
        if (GUILayout.Button("Save", _buttonCopyPaste.ToArray()) && _name != null)
        {

            if (!_namesEnemies.Contains(_name))
            {
                _enemy = ScriptableObject.CreateInstance<Enemy>();
                AssetDatabase.CreateAsset(_enemy, $"{_pathAsset}{_name}.asset");
            }

            int _maxWaves = 0;
            foreach (var item in _waves)
            {
                foreach (var item2 in item)
                {
                    _maxWaves++;
                }
            }
            _enemy.WaveCounters = new int[_waves.Count];
            _enemy.Waves = new GameObject[_maxWaves];
            int _currentCountWave = 0;
            int _currentCountWaves = 0;
            foreach (var item in _waves)
            {
                _enemy.WaveCounters[_currentCountWave] = 0;
                foreach (var item2 in item)
                {
                    _enemy.Waves[_currentCountWaves] = item2;
                    _currentCountWaves++;
                    _enemy.WaveCounters[_currentCountWave]++;
                }
                _currentCountWave++;
            }

            _enemy.Name = _name;
            AssetDatabase.WriteImportSettingsIfDirty($"{_pathAsset}{_name}.asset");
            AssetDatabase.SaveAssets();

            _enemies.Clear();
            _namesEnemies.Clear();
            foreach (var item in Resources.FindObjectsOfTypeAll<Enemy>())
            {
                _enemies.Add(item);
                _namesEnemies.Add(item.Name);
            }
            EditorUtility.SetDirty(_enemy);
        }
        if (GUILayout.Button("Delete", _buttonCopyPaste.ToArray()))
        {
            FileUtil.DeleteFileOrDirectory($"{_pathAsset}{_name}.asset");
            FileUtil.DeleteFileOrDirectory($"{_pathAsset}{_name}.asset.meta");
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.EndScrollView();
    }
}
