using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;
    const float combatCooldown = 5;
    public Equipment weapon;
    float lastAttackTime;

    public bool InCombat { get; private set; }
    // delegate with noarguments
    // Handles attack event
    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats enemyStats;
    GameObject clone;
    Transform playerPosition;

    private void Start() {
        myStats = GetComponent<CharacterStats>();
        playerPosition = PlayerManager.instance.AbilitySpawnPosition;
    }

    private void Update() {

        if (EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon] != null) {
            if (EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon] != weapon) {
                weapon = EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon];
            }
        }
        attackCooldown -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown) {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats) {
        if (attackCooldown <= 0f) {
            enemyStats = targetStats;
            if (OnAttack != null) {
                // Triggers attack event
                OnAttack();
            }
            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            
        }
    }


    public void AttackHit_AnimationEvent() {
        lastAttackTime = Time.time;
        enemyStats.TakeDamage(myStats.damage.getValue());

        if (enemyStats.currentHealth <= 0) {
            InCombat = false;
        }
    }

    public void RangeHit_AnimationEvent() {
        lastAttackTime = Time.time;
        Vector3 shoot = (enemyStats.transform.position - transform.position).normalized;
        clone = Instantiate(weapon.arrowPrefab, playerPosition.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(shoot * 160f);

        if (enemyStats.currentHealth <= 0) {
            InCombat = false;
        }
    }
}
