using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour {

    public Image icon;
    public Equipment item;


    public void setItem(Equipment newItem) {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;



    }
}
