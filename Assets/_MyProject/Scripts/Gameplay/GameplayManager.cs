using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static Action<float> TakeDamage;
    public static GameplayManager Instance;

    public static Action UpdatedScore;
    public static Action UpdatedMoney;

    [SerializeField] EnemiesManager enemiesManager;
    [SerializeField] WaveProgress waveProgress;
    [SerializeField] BaseHealthHandler healthHandler;
    [SerializeField] float[] houseHealth;
    [SerializeField] GameObject playerObject;
    LevelData selectedLevel;
    float score = 0;
    int money = 0;

    private void OnEnable()
    {
        EnemyObject.Died += UpdateScoreAndMoney;
        WaveProgress.Won += Won;
    }

    private void OnDisable()
    {
        EnemyObject.Died -= UpdateScoreAndMoney;
        WaveProgress.Won -= Won;
    }

    private void UpdateScoreAndMoney(EnemyObject _enemyObject)
    {
        Score += _enemyObject.EnemySO.ScoreReward;
        Money += _enemyObject.EnemySO.CoinReward;
    }

    void Won()
    {
        if (selectedLevel.Id == DataManager.Instance.PlayerData.UnlockedLevel)
        {
            DataManager.Instance.PlayerData.UnlockedLevel++;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (LevelManager.Instance.SelectedLevel == null)
        {
            LevelManager.Instance.SelectedLevel = LevelManager.Instance.Get(1);
        }

        selectedLevel = LevelManager.Instance.SelectedLevel;
        enemiesManager.Setup(selectedLevel);
        waveProgress.Setup(selectedLevel);
        healthHandler.Setup(houseHealth[DataManager.Instance.PlayerData.HouseLevel]);
    }

    public void DamageHouse(float _damage)
    {
        TakeDamage?.Invoke(_damage);
    }

    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UpdatedScore?.Invoke();
        }
    }

    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            UpdatedMoney?.Invoke();
        }
    }
}
