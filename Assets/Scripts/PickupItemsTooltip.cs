using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PickupItemsTooltip : MonoBehaviour, IPointerExitHandler {

    RectTransform panel;
    public GameObject pickupButtonPrefabOriginal;
    List<GameObject> buttonPrefabs;
    GameObject clone;
    public bool isShowing = false;

	void Start () {
        buttonPrefabs = new List<GameObject>();
        panel = GetComponent<RectTransform>();
        pickupButtonPrefabOriginal = (GameObject)Instantiate(panel.GetComponent<PickupItemsTooltip>().pickupButtonPrefabOriginal);
        panel.gameObject.SetActive(false);
	}

    public void show(Collider[] objects) {
        foreach (Collider collider in objects) {
            if (collider.GetComponent<ItemPickup>() != null) {
                ItemPickup itemPickup = collider.GetComponent<ItemPickup>();
                GameObject button = createPrefab(itemPickup.item);
                button.GetComponent<ItemPickup>().item = itemPickup.item;
                button.transform.SetParent(panel.transform);
                button.GetComponent<Button>().onClick.AddListener(delegate { OnClick(collider); });
                buttonPrefabs.Add(button);
            }
        }
        if (objects != null) {
            if (buttonPrefabs!= null && buttonPrefabs.Count >= 1) {
                panel.gameObject.SetActive(true);
                isShowing = true;
                panel.transform.position = Input.mousePosition;
            }
        }
    }

    GameObject createPrefab(Item item) {
        clone = Instantiate(pickupButtonPrefabOriginal);
        Text text = clone.GetComponentInChildren<Text>();
        text.text = "<color=yellow>Pickup </color> " + item.name;
        text.resizeTextForBestFit = false;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;


        text.fontStyle = FontStyle.Bold;
        return clone;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Disable();
    }

    public void OnClick(Collider collider) {
        Interactable interactable = collider.GetComponent<Interactable>();
        if (interactable != null) {
            PlayerController.instance.SetFocus(interactable);
        }
        Disable();
    }

    void Disable() {
        panel.gameObject.SetActive(false);
        isShowing = false;
        foreach (GameObject gameObject in buttonPrefabs) {
            Destroy(gameObject);
        }
        buttonPrefabs.Clear();
    }

}
