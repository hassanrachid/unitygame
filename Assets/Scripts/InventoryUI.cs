using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public Transform equipParent;
    Inventory inventory;
    EquipmentManager equipment;
    public GameObject inventoryUI;

    InventorySlot[] slots;
    EquipmentSlot[] equipSlots;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        equipment = EquipmentManager.instance;

        inventory.OnItemChangedCallback += UpdateUI;
        equipment.onEquipmentChanged += UpdateEquip;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        equipSlots = equipParent.GetComponentsInChildren<EquipmentSlot>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory")) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    void UpdateUI() {

        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("Updating UI...");
    }

   void UpdateEquip(Equipment newItem, Equipment oldItem) {
        if (newItem != null) {
            Debug.Log((int)newItem.equipSlot);
            Debug.Log(equipSlots.Length);
            equipSlots[(int)newItem.equipSlot].setItem(newItem);
        }
    }
}
