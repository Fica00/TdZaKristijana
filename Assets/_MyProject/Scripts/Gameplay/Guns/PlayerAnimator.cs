using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator armAnimator;

    private void OnEnable()
    {
        GunController.Fired += FireAnimation;
        GunController.Reloading += ReloadAnimation;
        GunController.FinishedReloading += IdleAnimation;
        BaseHealthHandler.ILost += DieAnimation;

        LosePanel.PlayerReviewd += Reviwe;
    }

    private void OnDisable()
    {
        GunController.Fired -= FireAnimation;
        GunController.Reloading -= ReloadAnimation;
        GunController.FinishedReloading -= IdleAnimation;
        BaseHealthHandler.ILost -= DieAnimation;

        LosePanel.PlayerReviewd -= Reviwe;
    }

    void FireAnimation()
    {
        bodyAnimator.Play("Attack");
        armAnimator.Play("Attack");
    }

    void ReloadAnimation()
    {
        bodyAnimator.Play("Ammo");
        armAnimator.Play("Ammo");
    }

    void DieAnimation()
    {
        bodyAnimator.Play("Died");
        armAnimator.Play("Died");
    }

    void IdleAnimation()
    {
        bodyAnimator.Play("Idle");
        armAnimator.Play("Idle");
    }

    void Reviwe()
    {
        IdleAnimation();
    }
}
