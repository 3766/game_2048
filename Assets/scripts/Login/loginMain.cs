using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loginMain : MonoBehaviour {
    public GameObject exitMessage;
    AudioSource audio;
    private bool  IsTiming;  //是否开始计时
    private float CountDown; //倒计时
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
        EixtDetection();
    }



    
    void EixtDetection()
    {
        if (Input.GetKeyDown(KeyCode.Escape))            //如果按下退出键
        {
            if (CountDown == 0)                          //当倒计时时间等于0的时候
            {
                CountDown = Time.time;                   //把游戏开始时间，赋值给 CountDown
                IsTiming  = true;                        //开始计时
                // LoginDate.ShowToast("再按我就把自己关掉"); //显示提示信息 —— 这里的提示方法，需要根据自己需求来完成（用你自己所需要的方法完成提示）
                // PrompPanel.isStartShowText = true;
                // PromptMsg.Instance.Change("大荣真丑", Color.black);
                print("我要关掉了");
            }
            else
            {
                // Application.Quit();                      //退出游戏
                #if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#else
				Application.Quit();
				#endif
            }
        }
 
        if (IsTiming) //如果 IsTiming 为 true 
        {
            if ((Time.time - CountDown) > 2.0)           //如果 两次点击时间间隔大于2秒
            {
                CountDown = 0;                           //倒计时时间归零
                IsTiming  = false;                       //关闭倒计时
            }
        }
    }

    void OnGUI(){
        if (IsTiming){
            GUIStyle style = new GUIStyle();
            // style.normal.background=Texture2D.whiteTexture;
            style.normal.textColor =new Color(0,0,0);
            style.fontSize=40;
            style.fontStyle=FontStyle.Bold;
            GUI.Label(new Rect(Screen.width*0.35f,Screen.height*0.75f,300,100),"再按一次退出游戏",style);
        }
    }
}
