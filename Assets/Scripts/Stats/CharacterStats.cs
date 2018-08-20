using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth { get; set; }
    public Stat damage;
    public Stat armor;


    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
    }

    public void TakeDamage(int damage) {

        damage -= armor.getValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " took " + damage + " damage");

        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        // Overridable method
        Debug.Log(transform.name  + " died.");
    }
}
