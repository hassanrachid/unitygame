using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour {

    public static TooltipManager instance;
    public GameObject enemyInfo;

    Text text;

	void Start () {
        instance = this;
        text = enemyInfo.GetComponentInChildren<Text>();
        text.resizeTextForBestFit = true;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
    }
	
    public void setText(string text) {
        this.text.text = text;
    }
}
