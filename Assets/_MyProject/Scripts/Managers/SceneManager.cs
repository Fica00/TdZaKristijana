public static class SceneManager
{
    const string MAIN_MENU_KEY = "MainMenu";
    const string LEVEL_SELECTION_KEY = "LevelSelection";
    const string GAMEPLAY_LEY = "Gameplay";
    const string SHOP_KEY = "Shop";

    public static void LoadMainMenu()
    {
        LoadScene(MAIN_MENU_KEY);
    }

    public static void LoadLevel(int _level)
    {
        LoadScene(_level.ToString());
    }

    public static void ResetScene()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public static void LoadShop()
    {
        LoadScene(SHOP_KEY);
    }

    public static void LoadLevelSelection()
    {
        LoadScene(LEVEL_SELECTION_KEY);
    }

    public static void LoadGameplay()
    {
        LoadScene(GAMEPLAY_LEY);
    }

    static void LoadScene(string _sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
    }
}
