using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useAnimation : MonoBehaviour {
	private Animation animation;
	private Button button;
	private Button buttonBack;
	// Use this for initialization
	void Start () {
		buttonBack=transform.GetChild(0).GetComponent<Button>();
		animation=transform.GetChild(5).GetComponent<Animation>();
		button=transform.GetChild(6).GetComponent<Button>();
		button.onClick.AddListener(buttonOnClick);
		buttonBack.onClick.AddListener(buttonBackOnClick);
	}
	void buttonOnClick(){
		animation.Play("animation");
	}
	void buttonBackOnClick(){
		animation.Play("back");
	}
	
}
