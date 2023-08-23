using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [field: SerializeField] public OkDialog OkDialog { get; private set; }

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
}
