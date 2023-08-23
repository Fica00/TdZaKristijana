using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;
using System.Collections;
using System;

public class PremmisionManager : MonoBehaviour
{
    public static PremmisionManager Instance;

    const string STORAGE_READ_KEY = "storageRead";
    const string STORAGE_WRITE_KEY = "storageWrite";

    public static Action Finished;
    int counter = 0;

    private Dictionary<string, string> requiredPermissions = new Dictionary<string, string>()
    {
        {STORAGE_READ_KEY,Permission.ExternalStorageRead},
        {STORAGE_WRITE_KEY,Permission.ExternalStorageWrite}
    };

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

    IEnumerator Start()
    {
        if (Application.platform!= RuntimePlatform.WindowsEditor)
        {
            if (int.Parse(SystemInfo.operatingSystem.Split('/')[0].Split(' ')[2])>=13)
            {
                Finished?.Invoke();
                yield break;
            }
            while (!CheckForStoragePremmision())
            {
                if (counter >= 5)
                {
                    UIManager.Instance.OkDialog.Show("Looks like you declined storage permission which is necessary for data storage, please go and manually allow it");
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
                counter++;
            }
        }

        Finished?.Invoke();
    }

    public bool CheckForStoragePremmision()
    {
        if (!HasGivenPremmisions(STORAGE_READ_KEY))
        {
            RequestPremmsion(STORAGE_READ_KEY);
            return false;
        }

        if (!HasGivenPremmisions(STORAGE_WRITE_KEY))
        {
            RequestPremmsion(STORAGE_WRITE_KEY);
            return false;
        }

        return true;
    }

    bool HasGivenPremmisions(string premmisionKey)
    {
        if (Permission.HasUserAuthorizedPermission(requiredPermissions[premmisionKey]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void RequestPremmsion(string _key)
    {
        Permission.RequestUserPermission(requiredPermissions[_key]);
    }
}
