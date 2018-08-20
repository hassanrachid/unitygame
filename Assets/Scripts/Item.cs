﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "Sample Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;


    public virtual void Use() {

    }

    public void RemoveFromInventory() {
        Inventory.instance.Remove(this);
    }

}