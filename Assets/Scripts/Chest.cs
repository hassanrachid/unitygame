using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable {


    public GameObject[] items;
    public int[] chances;
    private Dictionary<GameObject, int> possibleItems = new Dictionary<GameObject, int>();

    private void Start() {
        chances = new int[items.Length];
        for (int i = 0; i < items.Length; i++) {
            possibleItems.Add(items[i], chances[i]);
        }
    }

    public override void Interact() {
        Instantiate(items[0], interactionTransform.position, items[0].gameObject.transform.rotation);
        Instantiate(items[1], interactionTransform.position, items[1].gameObject.transform.rotation);
        Instantiate(items[2], interactionTransform.position, items[2].gameObject.transform.rotation);
        Instantiate(items[3], interactionTransform.position, items[3].gameObject.transform.rotation);
        Instantiate(items[4], interactionTransform.position, items[4].gameObject.transform.rotation);
        Instantiate(items[5], interactionTransform.position, items[5].gameObject.transform.rotation);
        // Destroy(this.gameObject);
    }

    protected override void OnMouseOver() {
        base.OnMouseOver();
        TooltipManager.instance.setText("<color=yellow>Open </color>" + this.name);
    }

}
