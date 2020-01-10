using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class travelLogin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void Awake(){
		Button button=gameObject.GetComponent<Button>() as Button;
		// button.onClick.AddListener(btClick);
		button.onClick.AddListener(btClick);

	}
	// Update is called once per frame
	void Update () {
		
	}
	void btClick () {
		SceneManager.LoadScene(1);
	}
}
