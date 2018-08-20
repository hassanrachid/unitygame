using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image icon;
    public Item item;
    public Text text;
    public RectTransform panel;

    Button button;

    private void Awake() {
        button = GetComponentInChildren<Button>();
    }

    public Button getButton() {
        return this.button;
    }

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
           
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnSlotClick() {
        if (item != null) {
            PanelTips.instance.show(item);
        } else {
            if (PanelTips.instance.gameObject.activeSelf) {
                PanelTips.instance.hide();
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!panel.gameObject.activeSelf) {
            if (item != null) {
                text.text = item.ToString();
                panel.transform.position = Input.mousePosition;
                panel.gameObject.SetActive(true);
                
                
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (panel.gameObject.activeSelf) {
                panel.gameObject.SetActive(false);
            }
    }
}
