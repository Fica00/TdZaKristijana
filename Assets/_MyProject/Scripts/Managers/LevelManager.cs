using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    const string VERSION_KEY = "Version";

    [HideInInspector] public LevelData SelectedLevel;

    [Tooltip("Update me when you add new enemy")]
    [SerializeField] int _version;

    List<LevelData> levels;
    string filePath => Application.persistentDataPath + "/Levels.txt";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("PathLevel: " + filePath);
    }

    public void Init()
    {
        bool _hasDeletedFile = false;
        if (PlayerPrefs.GetInt(VERSION_KEY, -1) < _version)
        {
            _hasDeletedFile = true;
            File.Delete(filePath);
            PlayerPrefs.SetInt(VERSION_KEY, _version);
        }

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();

            StreamWriter _writer = new StreamWriter(filePath);
            TextAsset _startingLevelsFile = Resources.Load<TextAsset>("StartingLevels");
            _writer.Write(_startingLevelsFile.text);
            _writer.Close();
        }

        LoadLevels();

        if (_hasDeletedFile)
        {
            for (int i = 0; i < DataManager.Instance.PlayerData.UnlockedLevel; i++)
            {
                Get(i);
            }
        }
    }

    void LoadLevels()
    {
        StreamReader _reader = new StreamReader(filePath);
        string _levelsData = _reader.ReadToEnd();
        _reader.Close();
        levels = JsonConvert.DeserializeObject<List<LevelData>>(_levelsData);
    }

    public LevelData Get(int _levelId)
    {
        foreach (var _level in levels)
        {
            if (_level.Id == _levelId)
            {
                return _level;
            }
        }

        LevelData _newLevel = GenerateLevel();
        levels.Add(_newLevel);
        SaveLevels(JsonConvert.SerializeObject(levels));

        return _newLevel;
    }

    LevelData GenerateLevel()
    {
        //copy previous level
        LevelData _levelData = new LevelData();
        _levelData.EnemySpawns = new List<EnemySpawns>();
        LevelData _lastLevel = levels[levels.Count - 1];
        _levelData.Id = _lastLevel.Id + 1;
        foreach (var _enemy in _lastLevel.EnemySpawns)
        {
            _levelData.EnemySpawns.Add(_enemy);
        }

        //increase level duration for 1s
        float _lastEnemySpawnTime = 0;
        for (int i = 0; i < _levelData.EnemySpawns.Count; i++)
        {
            if (_levelData.EnemySpawns[i].SpawnAtTime > _lastEnemySpawnTime)
            {
                _lastEnemySpawnTime = _levelData.EnemySpawns[i].SpawnAtTime;
            }
        }
        _lastEnemySpawnTime += 1;
        _levelData.EnemySpawns.Add(new EnemySpawns() { EnemyId = 0, SpawnAtTime = _lastEnemySpawnTime });


        //get posible enemies
        List<int> _possibleEnemieIds = new List<int>();
        foreach (var _enemy in EnemySO.Get())
        {
            if (_enemy.Type == EnemyType.Unit)
            {
                _possibleEnemieIds.Add(_enemy.Id);
            }
        }

        //add random enemies
        int _randomAmouuntOfEnemies = Random.Range(3, 10);
        for (int i = 0; i < _randomAmouuntOfEnemies; i++)
        {
            _levelData.EnemySpawns.Add(new EnemySpawns()
            {
                EnemyId = _possibleEnemieIds[Random.Range(0, _possibleEnemieIds.Count)],
                SpawnAtTime = Random.Range(0, _lastEnemySpawnTime)
            }
            ); ;
        }

        //add boss
        if (_levelData.Id % 10 == 0)
        {
            List<int> _possibleBosses = new List<int>();
            foreach (var _enemy in EnemySO.Get())
            {
                if (_enemy.Type == EnemyType.Boss)
                {
                    _possibleBosses.Add(_enemy.Id);
                }
            }

            float _spawnBossAtTime = Random.Range(0, 2) == 0 ? Random.Range(0, _lastEnemySpawnTime) : _lastEnemySpawnTime;

            _levelData.EnemySpawns.Add(new EnemySpawns()
            {
                EnemyId = Random.Range(0, _possibleBosses.Count),
                SpawnAtTime = _spawnBossAtTime
            });
        }

        return _levelData;
    }

    void SaveLevels(string _data)
    {
        StreamWriter _writer = new StreamWriter(filePath);
        _writer.Write(_data);
        _writer.Close();
    }

    public List<LevelData> Get()
    {
        return levels;
    }
}
