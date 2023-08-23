using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public EnemyType Type { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float AttackCooldown { get; private set; }
    [field: SerializeField] public int CoinReward { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public GameObject AttackPrefab { get; private set; }
    [field: SerializeField] public float ScoreReward { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }
    [field: SerializeField] public EnemyMoveType MoveType { get; private set; }

    static List<EnemySO> allEnemies;

    public static void Init()
    {
        allEnemies = Resources.LoadAll<EnemySO>("Enemies/").ToList();
    }

    public static EnemySO Get(int _id)
    {
        return allEnemies.Find(element => element.Id == _id);
    }

    public static List<EnemySO> Get()
    {
        return allEnemies;
    }
}