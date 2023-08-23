using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI levelDisplay;
    LevelData level;

    private void OnEnable()
    {
        button.onClick.AddListener(SelectLevel);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(SelectLevel);
    }

    void SelectLevel()
    {
        LevelManager.Instance.SelectedLevel = level;
        SceneManager.LoadGameplay();
    }

    public void Setup(LevelData _level)
    {
        level = _level;
        levelDisplay.text = _level.Id.ToString();
        if (level.Id > DataManager.Instance.PlayerData.UnlockedLevel)
        {
            button.interactable = false;
        }
    }

    public void Setup(int _levelId)
    {
        levelDisplay.text = _levelId.ToString();
        button.interactable = false;
    }
}
