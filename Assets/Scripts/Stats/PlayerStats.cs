using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	// Use this for initialization
	void Start () {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {

        if (newItem != null) {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            maxHealth += newItem.healthModifier;
            currentHealth += newItem.healthModifier;
        } 

        if (oldItem != null) {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            maxHealth -= oldItem.healthModifier;
            currentHealth += oldItem.healthModifier;
        }
    }

    public override void Die() {
        base.Die();
        PlayerManager.instance.KillPlayer();
        // Kill the player
    }
}
