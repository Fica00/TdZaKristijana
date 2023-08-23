using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public const string UI_GAMEPLAY_CLICK = "UIclick";
    public const string UI_MAINMENU_CLICK = "UIclick2";
    public const string WIN = "Win";
    public const string RAIN = "Rain";
    public const string LOSE = "Lose";
    public const string HIT = "Hit";
    public const string FREEZE = "Freeze";
    public const string BOMB_EXPLOSION = "BombExplosion";
    public const string PROJECTILE = "Projectile";

    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;

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
    }

    private void OnEnable()
    {
        PlayerData.UpdatedMusic += ToggleMusic;
    }

    private void OnDisable()
    {
        PlayerData.UpdatedMusic -= ToggleMusic;
    }

    private void Start()
    {
        ToggleMusic();
    }

    void ToggleMusic()
    {
        if (DataManager.Instance.PlayerData.PlayMusic)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void PlaySoundEffect(string _key)
    {
        AudioClip _audioClip = audioClips.FirstOrDefault(element => element.name == _key);
        if (_audioClip == null)
        {
            Debug.Log("Cloudnt find audio clip for " + _key);
        }
        else
        {
            if (DataManager.Instance.PlayerData.PlaySoundEffect)
            {
                audioSource.PlayOneShot(_audioClip);
            }
        }
    }

    public void PlayRandomHitNoise()
    {
        int _random = Random.Range(0, 5);
        switch (_random)
        {
            case 0:
                PlaySoundEffect("HitEnemy1");
                break;
            case 1:
                PlaySoundEffect("HitEnemy2");
                break;
            case 2:
                PlaySoundEffect("HitEnemy3");
                break;
            case 3:
                PlaySoundEffect("HitEnemy4");
                break;
            case 4:
                PlaySoundEffect("HitEnemy5");
                break;
            default:
                PlaySoundEffect("HitEnemy1");
                break;
        }
    }
}
