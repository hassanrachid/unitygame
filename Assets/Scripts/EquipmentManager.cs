using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public static EquipmentManager instance;
   
    // Callback for whenever an item is equipped/unequipped;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Awake() {
        instance = this;
    }

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment; // Items we currently have equipped
    SkinnedMeshRenderer[] currentMeshes;
    Inventory inventory; // Reference to our inventory

    public Equipment[] getEquip() {
        return currentEquipment;
    }

    void Start() {
        inventory = Inventory.instance;
        int slots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new Equipment[slots];
        currentMeshes = new SkinnedMeshRenderer[slots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);
        
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            if (currentMeshes[slotIndex] != null) {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            if (!oldItem.isDefaultItem) {
                inventory.Add(oldItem);
            }
            SetEquipmentBlendShapes(oldItem, 0);
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }

    public void UnequipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            Unequip(i);
        }
    }

    void SetEquipmentBlendShapes(Equipment item, int weight) {
        foreach (EquipmentMeshRegion blendShape in item.meshRegions) {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems() {
        foreach(Equipment item in defaultItems) {
            Equip(item);
        }
    }

}
