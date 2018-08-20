using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilities : MonoBehaviour {

    [SerializeField]

    public Ability[] abilities;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            foreach (Ability a in abilities) {
                if (a.name == "Fireball") {
                    a.Cast();
                }
            }
        }
    }

    // Create add method to add to player's active abilities.
}
