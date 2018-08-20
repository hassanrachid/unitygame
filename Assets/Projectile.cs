using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    GameObject player;
    PlayerStats playerStats;
    EnemyStats enemyStats;

    private void Start() {
        player = PlayerManager.instance.player;
        playerStats = player.GetComponent<PlayerStats>();
    }

    public void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject != player) {
            if (collision.gameObject.GetComponent<EnemyStats>()) {
                enemyStats = collision.gameObject.GetComponent<EnemyStats>();
                enemyStats.TakeDamage(playerStats.damage.getValue());
            }    
            Debug.Log("Collided");
            Destroy(gameObject);
        }
        
    }
}
