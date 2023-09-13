using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GunsManager : MonoBehaviour
{
    [SerializeField] Button[] gunButtons;
    [SerializeField] TextMeshProUGUI[] clipDisplay;
    [SerializeField] AmmoDisplayHandler ammoDisplayHandler;
    [SerializeField] Transform gunHolder;
    [SerializeField] SpriteRenderer weaponImage;

    List<GunController> gunControllers = new List<GunController>();

    GunController selectedGun;
    bool isAlive = true;
    Sequence hatSequence;


    private void OnEnable()
    {
        gunButtons[0].onClick.AddListener(SelectFirstGun);
        gunButtons[1].onClick.AddListener(SelectSecoundGun);
        gunButtons[2].onClick.AddListener(SelectThirdGun);
        GunController.UpdatedClips += UpdateClips;
        BaseHealthHandler.ILost += ILost;
        LosePanel.PlayerReviewd += IReviewd;

        hatSequence = DOTween.Sequence();
    }

    private void OnDisable()
    {
        gunButtons[0].onClick.RemoveListener(SelectFirstGun);
        gunButtons[1].onClick.RemoveListener(SelectSecoundGun);
        gunButtons[2].onClick.RemoveListener(SelectThirdGun);
        GunController.UpdatedClips -= UpdateClips;
        BaseHealthHandler.ILost -= ILost;
        LosePanel.PlayerReviewd -= IReviewd;
    }

    private void IReviewd()
    {
        isAlive = true;
    }

    private void ILost()
    {
        isAlive = false;
    }

    void UpdateClips(GunController _updatedGunController)
    {
        int _counter = 0;
        foreach (var _gunController in gunControllers)
        {
            if (_updatedGunController == _gunController)
            {
                clipDisplay[_counter].text = _updatedGunController.AmountOfClips.ToString();
                return;
            }
            _counter++;
        }
    }

    void SelectFirstGun()
    {
        SelectGun(0);
    }

    void SelectSecoundGun()
    {
        SelectGun(1);
    }

    void SelectThirdGun()
    {
        SelectGun(2);
    }

    void SelectGun(int _index)
    {
        if (selectedGun != null)
        {
            selectedGun.gameObject.SetActive(false);
        }
        selectedGun = gunControllers[_index];
        selectedGun.gameObject.SetActive(true);
        if (selectedGun.CurrentGunShotsAmount == 0)
        {
            selectedGun.Reload();
        }

        if (hatSequence.IsPlaying())
        {
            hatSequence.Kill();
        }

        string hueKey = "_Hue";
       
        weaponImage.sprite = GunSO.Get(DataManager.Instance.PlayerData.SelectedGuns[_index]).Sprite;

        hatSequence.Play();
        //weaponImage.sprite = GunSO.Get(DataManager.Instance.PlayerData.SelectedGuns[_index]).Sprite;
    }

    private void Start()
    {
        SetupWeapons();
        SelectGun(0);
        for (int i = 0; i < gunControllers.Count; i++)
        {
            UpdateClips(gunControllers[i]);
        }
    }

    void SetupWeapons()
    {
        int counter = 0;
        for (int i = 0; i < DataManager.Instance.PlayerData.SelectedGuns.Count; i++)
        {
            if (DataManager.Instance.PlayerData.SelectedGuns[i] == -1)
            {
                continue;
            }

            GunSO _selectedGun = GunSO.Get(DataManager.Instance.PlayerData.SelectedGuns[i]);
            gunButtons[counter].image.sprite = _selectedGun.UIPreview;
            gunButtons[counter].gameObject.SetActive(true);
            gunControllers.Add(Instantiate(_selectedGun.Prefab, gunHolder).GetComponent<GunController>());
            counter++;
        }

        foreach (var _gun in gunControllers)
        {
            _gun.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        if (Helpers.IsOverUI())
        {
            return;
        }
        HandleFire();
    }

    void HandleFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedGun.Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (selectedGun.HoldToFire&&Input.GetMouseButton(0))
        {
            selectedGun.Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            return;
        }
        
        AudioManager.Instance.PlaySoundEffect(selectedGun.GunSO.Sound);
    }
}
