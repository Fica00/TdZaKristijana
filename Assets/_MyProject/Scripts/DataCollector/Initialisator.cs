using UnityEngine;

public class Initialisator : MonoBehaviour
{
    private void OnEnable()
    {
        PremmisionManager.Finished += InitScriptableObjects;
    }

    private void OnDisable()
    {
        PremmisionManager.Finished -= InitScriptableObjects;
    }

    void InitScriptableObjects()
    {
        EnemySO.Init();
        GunSO.Init();
        AbilitiesSO.Init();
        LevelManager.Instance.Init();
        FinishInit();
    }

    void FinishInit()
    {
        SceneManager.LoadMainMenu();
    }
}
