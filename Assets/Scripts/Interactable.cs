using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public float radius = 1f;
    public new string name;
	bool isFocus = false;
	protected Transform player;
	public bool hasInteracted = false;
	public Transform interactionTransform;
    float distance;
    bool isShowing = false;

	public virtual void Interact() {
        distance = Vector3.Distance(player.position, interactionTransform.position);
        
    }

	void Update() {
		if (isFocus && !hasInteracted) {
            distance = Vector3.Distance(player.position, interactionTransform.position);
			if (distance <= radius) {
                hasInteracted = true;
                Interact();
            }
        }
	}
	
	void OnDrawGizmosSelected() {
		if (interactionTransform == null) {
			interactionTransform = transform;
		}
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}
	
	public void onFocused(Transform playerTransform) {
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
		
	}
	
	public void OnDeFocused() {
        if (distance <= radius) {
           player.gameObject.GetComponentInChildren<Animator>().Rebind();
        }
        isFocus = false;
		player = null;
		hasInteracted = false;

		
	}

    protected virtual void OnMouseOver() {
        if (!isShowing) {
            TooltipManager.instance.enemyInfo.SetActive(true);
            TooltipManager.instance.enemyInfo.transform.position = Input.mousePosition;
            isShowing = true;
        }
        
    }

    protected virtual void OnMouseExit() {
        if (isShowing) {
            TooltipManager.instance.enemyInfo.SetActive(false);
            isShowing = false;
        }
        
    }
}
