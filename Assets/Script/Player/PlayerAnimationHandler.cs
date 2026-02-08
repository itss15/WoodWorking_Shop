using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("Propertys")]
    public bool IsWalking = false;
    public bool IsGrabbingPlank = false;

    [Header("Animators")]
    [SerializeField] Animator HandsAnimator;

    private void Update()
    {
        UpdateHandsState();
    }

    void UpdateHandsState()
    {
        HandsAnimator.SetBool("IsWalking", IsWalking);
        HandsAnimator.SetBool("IsGrabbingPlank", IsGrabbingPlank);
    }
}
