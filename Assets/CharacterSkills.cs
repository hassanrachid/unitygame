using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkills : MonoBehaviour {

    public event System.Action OnTreeHit;
    public float chopSpeed = 1f;
    private float chopCooldown = 0f;

    TreeInteractable tree;

    private void Update() {

        chopCooldown -= Time.deltaTime;
    }

   public void Hit(TreeInteractable tree) {
        if (chopCooldown <= 0f) {
            if (OnTreeHit != null) {
                OnTreeHit();
            }
            chopCooldown = 1f / chopSpeed;
            this.tree = tree;
        }
    }
    
    public void TreeHit_AnimationEvent() {
        int damage = EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon].treeDamage;
        tree.health -= damage;
        if (tree != null && tree.health <= 0) {
            // Remove tree
            // Drop wood
            // TODO:: Find axe model, create axe animation, and add animation event
        }
        tree.currentHealth = tree.health;
    }
}
