using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability {

    public override void Cast() {
        base.Cast();
    }

    //private void Update() {
    //    float step = this.speed * Time.deltaTime;
    //    transform.position = Vector3.MoveTowards(transform.position, base.targetPoint, step);
    //}


    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject);
        Destroy(gameObject);
    }
}
