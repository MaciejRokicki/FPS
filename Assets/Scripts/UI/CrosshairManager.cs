using UnityEngine;

public class CrosshairManager : Singleton<CrosshairManager>
{
    [SerializeField]
    private Animator crosshairAnimator;

    public void OnHit()
    {
        crosshairAnimator.Play("OnHitAnimation");
    }
}
