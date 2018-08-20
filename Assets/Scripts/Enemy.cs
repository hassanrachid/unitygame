using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles interaction with the Enemy
[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

    PlayerManager playerManager;
    CharacterStats enemyStats;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void Start() {
        playerManager = PlayerManager.instance;
        enemyStats = GetComponent<CharacterStats>();
    }

    public override void Interact() {
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null) {
            playerCombat.Attack(enemyStats);
        }
    }

    protected override void OnMouseOver() {
        base.OnMouseOver();
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        TooltipManager.instance.setText("<color=red>Attack </color>" + this.name + "\n<color=green>Health " + enemyStats.currentHealth + "/" + enemyStats.maxHealth + "</color>");
       
    }

    protected override void OnMouseExit() {
        base.OnMouseExit();
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
