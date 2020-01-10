using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
// using Client;
public class loginInButton : MonoBehaviour {

	// Use this for initialization
	// public GameObject login;
	public InputField inputId;
	public InputField inputPassword;
	private string tip;
	private bool needTip=false;
	private float CountDown; //计时
	client myClient	=null;
	int btClickNum=0;
	void Update () {
		if(CountDown==0){//设置计时2s显示登录提示
			CountDown = Time.time;
		}
		else if(Time.time-CountDown>2){
			CountDown=0;
			needTip=false;
		}
	}

	void OnGUI()
    {
		if(needTip){
			GUIStyle style = new GUIStyle();
			// style.normal.background=Texture2D.whiteTexture;
			style.normal.textColor =new Color(0,0,0);
			style.fontSize=40;
			style.fontStyle=FontStyle.Bold;
			GUI.Label(new Rect(Screen.width*0.35f,Screen.height*0.75f,300,100),tip,style);
			GUI.TextField(new Rect(Screen.width*0.30f,Screen.height*0.74f,500,80), "");
		}
    }
	void Awake(){
		Button button=gameObject.GetComponent<Button>() as Button;
		// button.onClick.AddListener(btClick);
		button.onClick.AddListener(btClick);

	}
	void btClick () {
		if(btClickNum==0){
			myClient =new client();
			btClickNum++;
		}

		print("quit Button clicked");
		print(inputId.text);
		// conn.Start();
		if(inputId.text=="" || inputPassword.text==""){
			tip="账号或者密码不能为空！";
			needTip=true;
		}
		else {
			client.makeIdAndPassword(inputId.text,inputPassword.text);
			// bool result=conn.judgeIdAndPassword(inputId.text,inputPassword.text);
			Thread.Sleep(1000);
			print (client.returnValue);
			if(client.returnValue=="true"){
				tip="登陆成功！";
				needTip=true;


				client.getScore();
				SceneManager.LoadScene(1);
			}
			else {
				tip="账号或者密码错误！";
				needTip=true;
			}
		}
	}
}
