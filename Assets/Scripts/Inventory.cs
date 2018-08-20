using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;
    public int space = 20;

    #region

    private void Awake() {

        if (instance != null) {
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<Item> items = new List<Item>();
    
    public bool Add(Item item) {

        if (!item.isDefaultItem) {
            if (items.Count >= space) {
                Debug.Log("Inventory full.");
                return false; ;
            }
            items.Add(item);
            if (OnItemChangedCallback != null) {
                OnItemChangedCallback.Invoke();
            }
        }
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
        if (OnItemChangedCallback != null) {
            OnItemChangedCallback.Invoke();
        }
    }
}
