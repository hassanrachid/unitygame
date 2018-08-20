using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
	public Interactable focus;
	public LayerMask movementMask;
	public RectTransform inventory;
	public RectTransform panel;
    public RectTransform pickupItemsPanel;
	Camera cam;
	PlayerMotor motor;
    Interactable interactable;

	// Use this for initialization
	void Start () {
        instance = this;
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
			panel.gameObject.SetActive(false);
			inventory.gameObject.SetActive(false);
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 100, movementMask)) {
				motor.MoveToPoint(hit.point);
				RemoveFocus();
			}

		}

        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {

                interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    if (interactable is ItemPickup) {
                        if (!EventSystem.current.IsPointerOverGameObject()) {
                            Collider[] hitColliders = Physics.OverlapSphere(interactable.transform.position, 2);
                            if (!pickupItemsPanel.GetComponent<PickupItemsTooltip>().isShowing) {
                                pickupItemsPanel.GetComponent<PickupItemsTooltip>().show(hitColliders);
                            }
                        }
                    } else {
                        
                        SetFocus(interactable);
                        
                        
                    }
                }
            }
        }
	}

    void SetFocusRange() {
        if (focus != null) {
            if (focus is Enemy) {
                if (EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon] != null) {
                    focus.radius = EquipmentManager.instance.getEquip()[(int)EquipmentType.Weapon].stoppingDistance;
                } else {
                    focus.radius = 2;
                }
            }
        }
    }

    public void SetFocus(Interactable newFocus) {
		if (newFocus != focus) {
			if (focus != null) {
				focus.OnDeFocused();
			}
			
			focus = newFocus;
			motor.FollowTarget(newFocus);
		}
        SetFocusRange();
        newFocus.onFocused(transform);
		
	}
	
	public void RemoveFocus() {
		if (focus != null) {
			focus.OnDeFocused();
		}
		focus = null;
		motor.StopFollowingTarget();
	}
}
