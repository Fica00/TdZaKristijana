using System.Collections;
using UnityEngine;

public class Shotgun : GunController
{
    float spreed;

    int amountOfBulletsToShoot = 1;
    float cooldownCounter = 0;

    private void Awake()
    {
        GunShots = gun.AmountOfBullets[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
        spreed = gun.Spread;
        AmountOfClips = gun.AmountOfClips[DataManager.Instance.PlayerData.GetUpgrade2Level(gun.Id)];

        CurrentGunShotsAmount = GunShots;
    }


    public override void Fire(Vector3 _position)
    {
        if (PlayerManager.player.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x <= 0)
        {
            PlayerMovement.isFlipped = false;
            PlayerManager.player.GetComponent<PlayerMovement>().FlipSprites(PlayerMovement.isFlipped);
        }
        else
        {
            PlayerMovement.isFlipped = true;
            PlayerManager.player.GetComponent<PlayerMovement>().FlipSprites(PlayerMovement.isFlipped);
        }


        if (cooldownCounter > 0 || CurrentGunShotsAmount == 0)
        {
            return;
        }

        Fired?.Invoke();

        for (int i = 0; i < amountOfBulletsToShoot; i++)
        {
            GameObject _bullet = Instantiate(bullet);
            _bullet.transform.position = shootPoint.transform.position;
            _bullet.GetComponent<BulletController>().SetDamage(gun.Bullet.Damage[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)]);
            Vector2 _dir = (_position - transform.position).normalized;
            float _angle = Random.Range(-spreed, spreed);
            _dir = Quaternion.Euler(0, 0, _angle) * _dir;
            _dir = _dir.normalized;

            if (PlayerMovement.isFlipped)
            {
                if (PlayerManager.player.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x <= 0)
                {
                    Vector2 flipped = new(_dir.x * -1, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
                else
                {
                    Vector2 flipped = new(_dir.x, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
            }
            else
            {
                if (PlayerManager.player.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= 0)
                {
                    Vector2 flipped = new(_dir.x * -1, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
                else
                {
                    Vector2 flipped = new(_dir.x, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
            }

        }

        cooldownCounter = gun.Cooldown[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
        AudioManager.Instance.PlaySoundEffect(GunSO.Sound);
        CurrentGunShotsAmount--;

        if (CurrentGunShotsAmount == 0)
        {
            Reload();
        }
       
            
        
    }

    protected override IEnumerator ReloadRoutine()
    {
        Reloading?.Invoke();
        yield return new WaitForSeconds(gun.ReloadSpeed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)]);
        CurrentGunShotsAmount = GunShots;
        FinishedReloading?.Invoke();
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
