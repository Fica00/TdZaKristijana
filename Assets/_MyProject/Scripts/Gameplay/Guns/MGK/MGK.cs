using System.Collections;
using UnityEngine;

public class MGK : GunController
{
    private float spread;

    private int amountOfBulletsToShoot = 1;
    private float cooldownCounter = 0;

    private bool isPlaying;

    private void OnEnable()
    {
        isPlaying = false;
        CurrentGunShotsAmount = CurrentGunShotsAmount;
    }
    private void Awake()
    {
        GunShots = gun.AmountOfBullets[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
        spread = gun.Spread;
        AmountOfClips = gun.AmountOfClips[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];

        CurrentGunShotsAmount = GunShots;

        isPlaying = false;
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

            float _angle = Random.Range(-spread, spread);

            _dir = Quaternion.Euler(0, 0, _angle) * _dir;
            _dir = _dir.normalized;

            if (PlayerMovement.isFlipped)
            {
                if (PlayerManager.player.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x <= 0)
                {
                    Vector2 _flipped = new(_dir.x * -1, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = _flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
                else
                {
                    Vector2 _flipped = new(_dir.x, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = _flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
            }
            else
            {
                if (PlayerManager.player.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= 0)
                {
                    Vector2 _flipped = new(_dir.x * -1, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = _flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
                else
                {
                    Vector2 _flipped = new(_dir.x, _dir.y);
                    _bullet.GetComponent<Rigidbody2D>().velocity = _flipped * gun.Bullet.Speed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
                }
            }

        }

        cooldownCounter = gun.Cooldown[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)];
        if (isPlaying == false)
        {
            StartCoroutine(PlaySound());
        }
        CurrentGunShotsAmount--;
        if (CurrentGunShotsAmount <= 0)
        {
            Reload();
        }
    }

    protected override IEnumerator ReloadRoutine()
    {
        Reloading?.Invoke();
        yield return new WaitForSeconds(gun.ReloadSpeed[DataManager.Instance.PlayerData.GetUpgrade1Level(gun.Id)]);
        AmountOfClips--;
        CurrentGunShotsAmount = GunShots;
        FinishedReloading?.Invoke();
    }

    private void FixedUpdate()
    {
        if (!(cooldownCounter <= 0))
            cooldownCounter -= Time.deltaTime;
    }

    IEnumerator PlaySound()
    {
        isPlaying = true;
        AudioManager.Instance.PlaySoundEffect(GunSO.Sound);
        yield return new WaitForSeconds(GunSO.Sound.length);
        isPlaying = false;
    }
}