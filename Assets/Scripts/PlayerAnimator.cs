using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimation {

    // Array for weapon and their animation clips
    // Add weapon and animation clips in inspector when new item is added to the game
    // Reminder: Add array for shields to replace "Blocking" in Animator scene
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimDictionary;

    protected override void Start() {
        base.Start();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        
        weaponAnimDictionary = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations a in weaponAnimations) {
            weaponAnimDictionary.Add(a.weapon, a.clips);
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {
        if (newItem != null && newItem.equipSlot == EquipmentType.Weapon) {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimDictionary.ContainsKey(newItem)) {
                currentAttackAnimSet = weaponAnimDictionary[newItem];
                // Set speed of clip based on attack speed
            }
        } else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentType.Weapon) {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
        }

        if (newItem != null && newItem.equipSlot == EquipmentType.Shield) {
            animator.SetLayerWeight(2, 1);
        } else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentType.Shield) {
            animator.SetLayerWeight(2, 0);
        }

    }
	
    [System.Serializable]
    public struct WeaponAnimations {
        public Equipment weapon;
        public AnimationClip[] clips;
    }

}
