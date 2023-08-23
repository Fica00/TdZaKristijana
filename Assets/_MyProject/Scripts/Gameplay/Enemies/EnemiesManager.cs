using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    LevelData level;
    [SerializeField] Transform topBoundries;
    [SerializeField] Transform botBoundries;

    [SerializeField] Transform airTopBoundrie;
    [SerializeField] Transform airBotBoundrie;

    float counter;
    List<EnemySpawns> allEnemySpawsn;

    public void Setup(LevelData _level)
    {
        level = _level;
        allEnemySpawsn = _level.EnemySpawns.ToList();
        counter = 0.01f;
    }

    private void FixedUpdate()
    {
        if (counter == default)
        {
            return;
        }
        foreach (var _enemy in allEnemySpawsn.ToList())
        {
            if (counter > _enemy.SpawnAtTime)
            {
                SpawnEnemy(EnemySO.Get(_enemy.EnemyId));
                allEnemySpawsn.Remove(_enemy);
            }
        }
        counter += Time.deltaTime;
    }

    void SpawnEnemy(EnemySO _enemySO)
    {
        EnemyObject _enemy = Instantiate(_enemySO.Prefab).GetComponent<EnemyObject>();
        Vector3 _position = new Vector3();
        int _sortingLayer = 0;
        if (_enemySO.MoveType == EnemyMoveType.Ground)
        {
            _position.x = topBoundries.position.x;
            _position.z = topBoundries.position.z;
            _position.y = UnityEngine.Random.Range(botBoundries.position.y, topBoundries.position.y);

            _enemy.transform.position = _position;
            _sortingLayer = (int)Mathf.Abs((topBoundries.position.y - _enemy.transform.position.y) * 100);
        }
        else
        {
            _position.x = airTopBoundrie.position.x;
            _position.z = airTopBoundrie.position.z;
            _position.y = UnityEngine.Random.Range(airBotBoundrie.position.y, airTopBoundrie.position.y);
            _sortingLayer = (int)Mathf.Abs((airTopBoundrie.position.y - _enemy.transform.position.y) * 100);

            _enemy.transform.position = _position;
        }

        _enemy.Setup(_sortingLayer);
        _enemy.Scale();
    }
}
