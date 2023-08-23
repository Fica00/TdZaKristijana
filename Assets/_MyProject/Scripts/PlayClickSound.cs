using UnityEngine;

public class PlayClickSound : MonoBehaviour
{
    public void PlayMainMenuClick()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.UI_MAINMENU_CLICK);
    }

    public void PlayGameplayClick()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.UI_GAMEPLAY_CLICK);
    }
}
