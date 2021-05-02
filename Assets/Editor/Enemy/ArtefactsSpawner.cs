using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArtefactSpawner))]
public class ArtefactsSpawner : Editor
{
    private ArtefactSpawner _artefactSpawner;

    private List<Enemy> _enemies = new List<Enemy>();

    private List<string> _namesEnemies = new List<string>();

    private int _indexCurrentEnemy;

    private bool _isEnemy = false;

    public int CountTypes
    {
        get
        {
            return _artefactSpawner._artefactTypes.Length;
        }
        set
        {
            if(value != _artefactSpawner._artefactTypes.Length && value > 0)
            {
                _artefactSpawner._artefactTypes = new GameObject[value];
            }
        }
    }

    public void OnEnable()
    {
        _artefactSpawner = (ArtefactSpawner)target;
    }

    public override void OnInspectorGUI()
    {
        _artefactSpawner.Player = EditorGUILayout.ObjectField("Игрок", _artefactSpawner.Player, typeof(GameObject)) as GameObject;
        _artefactSpawner.BattleWindow = EditorGUILayout.ObjectField("Боевое окно", _artefactSpawner.BattleWindow, typeof(GameObject)) as GameObject;
        _artefactSpawner._isEnemy = EditorGUILayout.Toggle("Это враг?", _artefactSpawner._isEnemy);
        _isEnemy = _artefactSpawner._isEnemy;
        if (!_isEnemy)
        {
            _artefactSpawner._countArtefacts = EditorGUILayout.IntField("Количество артефактов", _artefactSpawner._countArtefacts);
            _artefactSpawner._countArtefactSpawned = EditorGUILayout.IntField("Количество артефактов за раз", _artefactSpawner._countArtefactSpawned);
        }

        GameObject _front = EditorGUILayout.ObjectField("Объект фронта", _artefactSpawner._front, typeof(GameObject)) as GameObject;
        _artefactSpawner._front = _front;

        if (!_isEnemy)
        {
            GameObject _spawner = EditorGUILayout.ObjectField("Объект фронта", _artefactSpawner._spawner, typeof(GameObject)) as GameObject;
            _artefactSpawner._spawner = _spawner;
            _artefactSpawner._timeSpawn = EditorGUILayout.FloatField("Время между спавнами", _artefactSpawner._timeSpawn);

            CountTypes = EditorGUILayout.IntField("Количество видов артефоктов", CountTypes);

            for (int i = 0; i < _artefactSpawner._artefactTypes.Length; i++)
            {
                _artefactSpawner._artefactTypes[i] = EditorGUILayout.ObjectField($"{i} ", _artefactSpawner._artefactTypes[i], typeof(GameObject)) as GameObject;
            }

        }



        if (_isEnemy)
        {
            _enemies.Clear();
            _namesEnemies.Clear();
            foreach (var item in Resources.FindObjectsOfTypeAll<Enemy>())
            {
                _enemies.Add(item);
                _namesEnemies.Add(item.Name);
            }

            EditorGUILayout.LabelField("Выбрать врага");
            _artefactSpawner._indexNames = EditorGUILayout.Popup(_artefactSpawner._indexNames, _namesEnemies.ToArray());
            _artefactSpawner._enemy = _enemies[_artefactSpawner._indexNames];
            _artefactSpawner._enemy.Waves = _enemies[_artefactSpawner._indexNames].Waves;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_artefactSpawner); 
        }
    }
}
