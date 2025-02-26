using Unity.Profiling;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimationManager animatorHandler;
    public void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimationManager>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        animatorHandler.PlayTargetAnimation(weapon.MeleeAttack_OneHanded,true);
    }
    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animatorHandler.PlayTargetAnimation(weapon.MeleeAttack_TwoHanded,true);
    }
}
