using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

    [SerializeField]
    public new string name;
    public GameObject prefab;
    public int damage;
    public int speed;
    public Vector3 targetPoint;

    GameObject clone;
    GameObject player;
    Transform playerPosition;

    public virtual void Cast() {

        player = PlayerManager.instance.player;
        playerPosition = PlayerManager.instance.AbilitySpawnPosition;

        // Gets position of mouse cursor
        Plane playerPlane = new Plane(Vector3.up, player.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 10.0f;
        targetPoint = new Vector3(0, 0, 0);
        if (playerPlane.Raycast(ray, out hitdist)) {
            targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - player.transform.position);
            player.transform.rotation = targetRotation;
        }

        // Instantiate ability
        clone = (GameObject)Instantiate(prefab, playerPosition.position, player.transform.rotation);
        // Launch ability with speed
        clone.GetComponent<Rigidbody>().AddForce(player.transform.forward * speed);
    }
}