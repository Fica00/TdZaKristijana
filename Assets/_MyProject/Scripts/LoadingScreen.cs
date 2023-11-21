using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;
    public static bool isLoadingFinished;

    void OnEnable()
    {
        isLoadingFinished = false;
    }

    void Update()
    {

        if (loadingSlider.value >= 2.5f)
        {
            OnLoaded();
        }
        else
        {
            loadingSlider.value += Time.deltaTime;

        }
    }

    void OnLoaded()
    {
        SceneManager.LoadMainMenu();
        gameObject.SetActive(false);
        return;
    }


}
