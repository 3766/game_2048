using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class getScoreRanking : MonoBehaviour {


	private Animation animation;
	private Button button;
	private Button buttonBack;
	// Use this for initialization
	void Start () {
		buttonBack=transform.GetChild(0).GetComponent<Button>();
		animation=transform.GetChild(7).GetComponent<Animation>();
		button=transform.GetChild(8).GetComponent<Button>();
		button.onClick.AddListener(buttonOnClick);
		buttonBack.onClick.AddListener(buttonBackOnClick);
	}
	void buttonOnClick(){
		animation.Play("animation");
	}
	void buttonBackOnClick(){
		animation.Play("back");
	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene(1);
		}
	}
}
