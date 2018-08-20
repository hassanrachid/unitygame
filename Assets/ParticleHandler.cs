using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour {

    ParticleSystem system;
    int health;
    EnemyStats enemyStats;
	// Use this for initialization
	void Start () {
        system = GetComponentInChildren<ParticleSystem>();
        enemyStats = GetComponent<EnemyStats>();
        health = enemyStats.currentHealth;
        Debug.Log(system);

    }

    private void Update() {
        if (enemyStats.currentHealth != health) {
            system.Emit(5);
            health = enemyStats.currentHealth;
        }
    }
}
