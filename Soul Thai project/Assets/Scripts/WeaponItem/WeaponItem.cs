using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Attack animations")]
    public string MeleeAttack_OneHanded;
    public string MeleeAttack_TwoHanded;

}
