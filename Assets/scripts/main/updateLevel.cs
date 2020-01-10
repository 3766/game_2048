using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class updateLevel : MonoBehaviour {

	private int level=0;
	public Sprite[] nums=new Sprite[12];
	private SpriteRenderer srl;
	Text text;
	Text highText;
	void Start(){
		srl=GetComponent<SpriteRenderer>();
		text=GameObject.Find("score").GetComponent<Text>();
		highText=GameObject.Find("highScore").GetComponent<Text>();
	}

	void updateLevels(){
		level++;
		staticCreate.playScore[staticCreate.playMode-4]+=Mathf.Pow(2,level+1);
		int highscore;
		int.TryParse(highText.text, out highscore);
		if(highscore<staticCreate.playScore[staticCreate.playMode-4]){
			highText.text=(staticCreate.playScore[staticCreate.playMode-4]).ToString();
		}
		print(staticCreate.playScore[staticCreate.playMode-4]);
		text.text=(staticCreate.playScore[staticCreate.playMode-4]).ToString();
		//修改当前对象的名称
		name="n"+level;
		//修改显示效果
		srl.sprite=nums[level];
		if(level>=12){
			staticCreate.isWin=true;
		}

	}
}
