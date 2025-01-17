using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class TextUpdate : MonoBehaviour {
    Text textbox;

	// Use this for initialization
	void Start () {
        textbox = GetComponent<Text>();
        sliderupdate(1.0f);
    }

    // Update is called once per frame

    public void sliderupdate (float Value) {
        string day = " days";
       Value = (float)System.Math.Round((double)Value, 2); 
        if (Value == 1.0f) { day = " day"; }
        
        textbox.text = "1 second = " + Value.ToString() + day; 

	}
}
