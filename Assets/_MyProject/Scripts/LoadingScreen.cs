using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private float duration;

    private void Start()
    {
        loadingSlider.DOValue(1, duration).OnComplete(SceneManager.LoadMainMenu);
    }
}