using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;


    private void Start() {
        interactionTransform = GetComponent<Transform>();
    }
    public override void Interact() {

        PickUp();
    }

    public void PickUp() {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) {
            Destroy(gameObject);
        }
        
    }

    protected override void OnMouseOver() {
        
    }

    protected override void OnMouseExit() {

    }

}
