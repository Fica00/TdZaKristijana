using UnityEngine;

public class BulletController : MonoBehaviour
{
    protected float damage;
    [SerializeField] protected GameObject fireEffect;
    [SerializeField] protected Vector3 fireEffectOffset;

    public virtual void SetDamage(float _damage)
    {
        throw new System.Exception("SetDamage must be overriden in BulletController");
    }

    void Start()
    {
        if (fireEffect == null)
        {
            return;
        }

        // AudioManager.Instance.PlaySoundEffect(AudioManager.PROJECTILE);
        GameObject _effect = Instantiate(fireEffect);
        _effect.transform.position = transform.position + fireEffectOffset;
    }
}
