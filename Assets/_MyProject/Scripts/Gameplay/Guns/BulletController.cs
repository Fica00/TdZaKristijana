using DG.Tweening;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    protected float damage;
    [SerializeField] protected GameObject fireEffect;
    [SerializeField] protected Vector3 fireEffectOffset;
    public Vector3 flip2;
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

        GameObject _effect = Instantiate(fireEffect);
        _effect.transform.position = transform.position + fireEffectOffset * (PlayerMovement.isFlipped ? -1 : 1);
        transform.rotation = Quaternion.Euler(0, PlayerMovement.isFlipped ? 180 : 0, 0);

        ParticleSystemRenderer _psr = _effect.GetComponent<ParticleSystemRenderer>();
        _psr.flip = new Vector3(PlayerMovement.isFlipped ? 1 : 0, 0, 0);
    }
    
    
}