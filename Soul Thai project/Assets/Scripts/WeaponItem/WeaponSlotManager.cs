using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    PlayerManager playerManager;
    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();

        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    #region Handle Weapon's Damage Collider

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
        }
    }

    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void OpenDamageCollider()
    {
        Debug.Log("OpenDamageCollider called on " + gameObject.name);
       
            if (rightHandDamageCollider != null)
            {
                rightHandDamageCollider.EnableDamageCollider();
                Debug.Log("Right hand damage collider enabled on " + rightHandDamageCollider.gameObject.name);
            }
            else
            {
                Debug.LogError("Right hand damage collider is null");
            }
        
       
            if (leftHandDamageCollider != null)
            {
                leftHandDamageCollider.EnableDamageCollider();
                Debug.Log("Left hand damage collider enabled on " + leftHandDamageCollider.gameObject.name);
            }
            else
            {
                Debug.LogError("Left hand damage collider is null");
            }
        
    }
    public void CloseDamageCollider()
    {
        if (rightHandDamageCollider != null)
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        if (leftHandDamageCollider != null)
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
    }

    #endregion
}
