using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PanelTips : MonoBehaviour, IPointerExitHandler {

    public static PanelTips instance;
    public GameObject dropItem;
    public GameObject equipItem;
    RectTransform panel;
    Item inventoryItem;

    private void Awake() {
        instance = this;
        panel = GetComponent<RectTransform>();
        panel.gameObject.SetActive(false);
        instantiateObjects();
    }

    public void show(Item item) {
        inventoryItem = item;
        equipItem.gameObject.SetActive(false);

        if (item is Equipment) {
            equipItem.gameObject.SetActive(true);
        }

        panel.gameObject.SetActive(true);
        panel.transform.position = new Vector3(Input.mousePosition.x + 35, Input.mousePosition.y - 30, Input.mousePosition.z);
    }

    // Hide tip panel

    public void hide() {
        panel.gameObject.SetActive(false);
    }

    // Instantiates all tips to be used in panel

    void instantiateObjects() {

        equipItem = (GameObject)Instantiate(panel.GetComponent<PanelTips>().equipItem);
        equipItem.transform.SetParent(panel.transform);
        equipItem.GetComponent<Button>().onClick.AddListener(OnEquipTip);

        dropItem = (GameObject)Instantiate(panel.GetComponent<PanelTips>().dropItem);
        dropItem.transform.SetParent(panel.transform);
        dropItem.GetComponent<Button>().onClick.AddListener(OnDropTip);


    }

    // Handles all tip listeners

    public void OnDropTip() {
        Inventory.instance.Remove(inventoryItem);
        panel.gameObject.SetActive(false);
    }

    public void OnEquipTip() {
        inventoryItem.Use();
        panel.gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData) {
        panel.gameObject.SetActive(false);
    }
}
