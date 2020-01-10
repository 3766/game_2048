using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class initRanking : MonoBehaviour {
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene(1);
		}
	}
    void OnGUI(){
		GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;    //设置背景填充
        fontStyle.normal.textColor= new Color(0,0,0);   //设置字体颜色
        fontStyle.fontSize = 80;       //字体大小
		GUI.Label(new Rect(400, 100, 200, 200), "排行榜", fontStyle);
		GUI.Label(new Rect(250, 200, 200, 200), "玩家", fontStyle);
		GUI.Label(new Rect(650, 200, 200, 200), "得分", fontStyle);
        for (int i = 0; i < User.rankingScore.Length; i++) {
        		GUI.Label(new Rect(200, 400+i*200, 200, 200), User.rankingName[i], fontStyle);
		}
		for (int i = 0; i < User.rankingScore.Length; i++) {
        		GUI.Label(new Rect(600, 400+i*200, 200, 200), User.rankingScore[i], fontStyle);
		}
	}  
}
