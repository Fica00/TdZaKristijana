using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSelectionDisplay : MonoBehaviour
{
    [SerializeField] LevelDisplay levelDownPrefab;
    [SerializeField] LevelDisplay levelUpPrefab;
    [SerializeField] Transform levelHolder;

    [SerializeField] TextMeshProUGUI goldDisplay;

    [SerializeField] Button goHomeButton;

    private void OnEnable()
    {
        goHomeButton.onClick.AddListener(GoHome);
    }

    private void OnDisable()
    {
        goHomeButton.onClick.RemoveListener(GoHome);
    }

    void GoHome()
    {
        SceneManager.LoadMainMenu();
    }

    void Start()
    {
        ShowLevels();
        goldDisplay.text = DataManager.Instance.PlayerData.Gold.ToString();
    }

    void ShowLevels()
    {
        int _counter = 0;
        foreach (var _level in LevelManager.Instance.Get())
        {
            CreateLevel(_counter).Setup(_level);
            _counter++;
        }

        LevelData _lastLevel = LevelManager.Instance.Get(LevelManager.Instance.Get().Count);
        CreateLevel(_counter).Setup(_lastLevel.Id + 1);
    }

    LevelDisplay CreateLevel(int _counter)
    {
        LevelDisplay _levelDisplay;
        if (_counter % 2 == 0)
        {
            _levelDisplay = Instantiate(levelDownPrefab, levelHolder);
        }
        else
        {
            _levelDisplay = Instantiate(levelUpPrefab, levelHolder);
        }

        return _levelDisplay;
    }
}
