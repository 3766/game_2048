using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gotoScene : MonoBehaviour {

	// Use this for initialization
	public int buttonNum;
	public AudioSource audio;
	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake(){
		Button button=gameObject.GetComponent<Button>() as Button;
		button.onClick.AddListener(btClick);

	}
	void btClick () {
		audio.Play();
		print("Button clicked");
		if(buttonNum==4){
			SceneManager.LoadScene(2);
		}
		else if(buttonNum==5){
			SceneManager.LoadScene(3);
		}
		else if(buttonNum==1){
			SceneManager.LoadScene(5);
		}
		else{
			SceneManager.LoadScene(4);
		}

	}
}
