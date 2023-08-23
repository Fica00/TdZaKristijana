using System.Collections;
using UnityEngine;

public class MGK : GunController
{
    float spreed;

    int amountOfBulletsToShoot = 1;
    float cooldownCounter = 0;

    private void Awake()
    {
        GunShots = gun.AmountOfBullets[DataManager.Instance.PlayerData.GetUpgrade2Level(gun.Id)];
        spreed = gun.Spread;
        AmountOfClips = gun.AmountOfClips[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];

        CurrentGunShotsAmount = GunShots;
    }

    public override void Fire(Vector3 _position)
    {
        if (cooldownCounter > 0 || CurrentGunShotsAmount == 0)
        {
            return;
        }
        Fired?.Invoke();
        AudioManager.Instance.PlaySoundEffect(AudioManager.HIT);
        for (int i = 0; i < amountOfBulletsToShoot; i++)
        {
            GameObject _bullet = Instantiate(bullet, shootPoint);
            _bullet.GetComponent<BulletController>().SetDamage(gun.Bullet.Damage[DataManager.Instance.PlayerData.GetUpgrade2Level(gun.Id)]);
            Vector2 _dir = (_position - transform.position).normalized;
            float _angle = Random.Range(-spreed, spreed);
            _dir = Quaternion.Euler(0, 0, _angle) * _dir;
            _dir = _dir.normalized;
            _bullet.GetComponent<Rigidbody2D>().velocity = _dir * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade2Level(gun.Id)];
        }
        cooldownCounter = gun.Cooldown[DataManager.Instance.PlayerData.GetUpgrade2Level(gun.Id)];
        CurrentGunShotsAmount--;
        if (CurrentGunShotsAmount == 0)
        {
            Reload();
        }
    }

    protected override IEnumerator ReloadRoutine()
    {
        Reloading?.Invoke();
        yield return new WaitForSeconds(gun.ReloadSpeed[DataManager.Instance.PlayerData.GetUpgrade2Level(gun.Id)]);
        if (AmountOfClips <= 0)
        {
            yield break;
        }
        AmountOfClips--;
        FinishedReloading?.Invoke();
        CurrentGunShotsAmount = GunShots;
    }

    private void FixedUpdate()
    {
        if (cooldownCounter <= 0)
        {
            return;
        }
        else
        {
            cooldownCounter -= Time.deltaTime;
        }
    }

}