using System;
using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] protected GunSO gun;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform shootPoint;
    [field: SerializeField] public bool HoldToFire { get; protected set; }

    public static Action<int> UpdatedAmmo;
    public static Action<GunController> UpdatedClips;
    public static Action Fired;
    public static Action Reloading;
    public static Action FinishedReloading;

    int gunShots;
    int currentGunShotsAmount;
    int amountOfClips;

    public GunSO GunSO => gun;

   
    public int GunShots
    {
        get
        {
            return gunShots;
        }
        set
        {
            gunShots = value;
        }
    }
    public int CurrentGunShotsAmount
    {
        get
        {
            return currentGunShotsAmount;
        }
        set
        {
            currentGunShotsAmount = value;
            UpdatedAmmo?.Invoke(currentGunShotsAmount);
        }
    }
    public int AmountOfClips
    {
        get
        {
            return amountOfClips;
        }
        set
        {
            amountOfClips = value;
            UpdatedClips?.Invoke(this);
        }
    }

    public virtual void Fire(Vector3 _position)
    {
        throw new Exception("Fire method must be overriden");
    }

    private void OnEnable()
    {
        UpdatedAmmo?.Invoke(currentGunShotsAmount);
        UpdatedClips?.Invoke(this);
    }

    public void Reload()
    {
        StartCoroutine(ReloadRoutine());
    }

    protected virtual IEnumerator ReloadRoutine()
    {
        throw new Exception("Reload routine must be overriden");
    }
}