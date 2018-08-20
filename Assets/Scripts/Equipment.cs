using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentType equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] meshRegions;

    [Space]
    public int armorModifier;
    public int damageModifier;
    public int healthModifier;
    public int stoppingDistance;

    [Space]
    public bool isRangeAttack;
    public GameObject arrowPrefab;

    [Space]
    public bool isAxe;
    public int treeDamage;

    private void Awake() {
    }

    public override void Use() {
        Debug.Log("Equipping " + this.name);
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    // Get item currently wearing to get difference of modifiers

    public override string ToString() {
        return "Equipment Type: " + equipSlot + "\nAttributes:\n<color=lightgreen>" + healthModifier + " Health</color>";
    }

}

public enum EquipmentType {
    Helmet,
    Chestplate,
    Leggings,
    Weapon,
    Shield,
}

public enum EquipmentMeshRegion { // Corresponds to body blend shapes
    Legs,
    Arms,
    Torso
}
