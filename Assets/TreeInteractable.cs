using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteractable : Interactable {

    public int health;
    public int amount;

    public int currentHealth;

    GameObject playerInteract;
    CharacterSkills characterSkills;

    private void Start() {
        currentHealth = health;
    }

    public override void Interact() {
        if (EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon] != null) {
            if (EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon].isAxe) {
                playerInteract = player.transform.gameObject;
                characterSkills = playerInteract.GetComponent<CharacterSkills>();
                if (characterSkills != null) {
                    characterSkills.Hit(this);
                }
                Debug.Log("Interacted " + characterSkills);
            }
        }
        
    }

    protected override void OnMouseOver() {
        base.OnMouseOver();
        TooltipManager.instance.setText("<color=blue>Chop </color>" + this.name + "\n<color=green>Health " + currentHealth + "/" + health + "</color>");

    }

    protected override void OnMouseExit() {
        base.OnMouseExit();
    }
}
