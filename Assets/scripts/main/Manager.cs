using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class Manager : MonoBehaviour {
	public int playManageModeRows=4;
	public GameObject num_2; 
	private GameObject[,] numList=new GameObject[6,6];

	private GameObject num;
	private GameObject preNum;

    private Vector2 beginPos;
	public Texture2D win;

	private int Iay=0;
	private int Iax=0;
	bool buttonClicked=false;
	AudioSource audio;
	DateTime beforeDT;
	Text text;
	Text highScoreText;
	// Use this for initialization
	void Start () {
		// 设置屏幕在游戏界面不熄灭
	 	Screen.sleepTimeout = SleepTimeout.NeverSleep;
		// 初始化用户当前最高分
		staticCreate.playMode=playManageModeRows;
		beforeDT = System.DateTime.Now;
		text=GameObject.Find("score (1)").GetComponent<Text>();
		highScoreText=GameObject.Find("highScore").GetComponent<Text>();
		highScoreText.text =User.highScore[staticCreate.playMode-4].ToString();
		setPlay();
		audio = GetComponent<AudioSource> ();
		createNum();
		createNum();
	}
	void setPlay(){

		if(playManageModeRows==4){
			num_2.transform.localScale = new Vector3 (2.3f, 2.3f, 0);
		}
		else if(playManageModeRows==5){
			staticCreate.XStartPos=-3.2f;
			staticCreate.YStartPos=6.7f;
			staticCreate.XOffset=1.6f;
			staticCreate.YOffset=1.6f;
			num_2.transform.localScale = new Vector3 (1.8f, 1.8f, 0);
		}
		else if (playManageModeRows==6){
			staticCreate.XStartPos=-3.6f;
			staticCreate.YStartPos=6.7f;
			staticCreate.XOffset=1.4f;
			staticCreate.YOffset=1.4f;
			num_2.transform.localScale = new Vector3 (1.5f, 1.5f, 0);
		}


	}
	// Update is called once per frame
	void Update () {

		// 计时
		DateTime afterDT = System.DateTime.Now;
		TimeSpan ts = afterDT.Subtract(beforeDT);
		
		string []timeSecond=new string[]{"0","0"};
		timeSecond=(ts.TotalSeconds).ToString().Split(new char[]{'.'},2);
		text.text=timeSecond[0];
		// print(ts.TotalMinutes);
		// print(ts.TotalMilliseconds);
		if(buttonClicked&&preNum!=null){
			// 设置图形恢复原状
			preNum.transform.DORewind();
		}
		buttonClicked=false;

		if (staticCreate.isWin==true){
			return;
		}
		TouchDirection();
		// 设置手机接近平衡时重置
		if(Mathf.Abs(Input.acceleration.x)<0.15){
			Iax=0;
		}
		if(Mathf.Abs(Input.acceleration.y)<0.15){
			Iay=0;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)||(Input.acceleration.y>0.5&&Iay>=0)){
			buttonClicked=true;
			Iay=-1;
			upEvent();
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow)||(Input.acceleration.y<-0.5&&Iay<=0)){
			buttonClicked=true;
			Iay=1;
			downEvent();
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow)||(Input.acceleration.x<-0.5&&Iax<=0)){
			buttonClicked=true;
			Iax=1;
			leftEvent();
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow)||(Input.acceleration.x>0.5&&Iax>=0)){
			buttonClicked=true;
			Iax=-1;
			rightEvent();
		}

		if (Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene(1);
		}
	}
	void createNum(){//创建新的数字2
		int x1=-1,y1=-1;
		do{
		x1=UnityEngine.Random.Range(0,playManageModeRows);
		y1=UnityEngine.Random.Range(0,playManageModeRows);
		// print(x1+"_"+y1);
		}while(numList[x1,y1]!=null);

		GameObject n=Instantiate(num_2) as GameObject;
		n.name="n0";
		n.transform.position=new Vector3(
			staticCreate.XStartPos+x1*staticCreate.XOffset,
			staticCreate.YStartPos-y1*staticCreate.YOffset,
			0
		);
		numList[x1,y1]=n;
	}


    void TouchDirection()//触摸判断
    {
        if (Input.touchCount <= 0)
            return;
 
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                beginPos = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
				
            }
 
            if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[0].phase != TouchPhase.Canceled)
            {
				buttonClicked=true;
                Vector2 pos = Input.touches[0].position;
                if (Mathf.Abs(beginPos.x - pos.x) > Mathf.Abs(beginPos.y - pos.y))
                {
                    if (beginPos.x > pos.x)
                    {
                        //向左
						print("向左");
						leftEvent();
                    }
                    else
                    {
                        //向右
						print("向you");
						rightEvent();
                    }
                }
                else
                {
                    if (beginPos.y > pos.y)
                    {
                        //向下
						print("向xia");
						downEvent();
                    }
                    else
                    {
                        //向上
						print("向shang");
						upEvent();
                    }
                }
            }
        }
    }

    void OnGUI(){   
		if (staticCreate.isWin==true){// 游戏结束标志
			GUI.DrawTexture(new Rect(
				(Screen.width/2f-win.width/3f)/2f,
				(Screen.height/2f-win.height/3f)/2f,
				win.width*8f,
				win.height*8f
			),win);
			return;
		}    
    }


void leftEvent(){
	print("left");
	bool needCreate=false;//保证没有物体移动时，不产生新方块
		//第0列不需要移动
		for (int y=0;y<playManageModeRows;y++){
			for(int x=1;x<playManageModeRows;x++){
				num=numList[x,y];
				if(num==null){
					continue;
				}
				int markPos=-1;
				for(int x1=x-1;x1>=0;x1--){
				preNum=numList[x1,y];
				if(preNum!=null){
					//有元素，停止

					if(num.name.Equals(preNum.name)){
						//相同对象，合并，不在一栋当前对象
						markPos=-1;
						Destroy(num);
						numList[x,y]=null;
						preNum.SendMessage("updateLevels");
						preNum.transform.DOShakeScale(0.5f, new Vector3(0.5f,0.5f, 0));
						needCreate=true;
						audio.Play();
					}
					break;
				}
				else{
					markPos=x1;
					//移动
					// System.Threading.Thread.Sleep(500);
					// num.transform.position-=new Vector3(staticCreate.YOffset,0,0);
				}
				}
			if (markPos>-1){
				//更新位置矩阵
				numList[x,y]=null;
				numList[markPos,y]=num;
				// //移动
				num.transform.position-=new Vector3((x-markPos)*staticCreate.YOffset,0,0);
				// num.transform.DOShakeScale(1, new Vector3(1, 1, 0));
				// num.transform.position=new Vector3(staticCreate.XStartPos+x*staticCreate.XOffset,staticCreate.YStartPos+markPos*staticCreate.YOffset,0);
				needCreate=true;
			}
			}
		}
	if(needCreate){
		createNum();
	}

}
void rightEvent(){

		print("right");
		bool needCreate=false;
		//最后行不需要移动
		for(int y=playManageModeRows-1;y>=0;y--){
			for(int x=playManageModeRows-2;x>=0;x--){
				num=numList[x,y];
				if(num==null){
					continue;
				}
				int markPos=-1;
				for(int x1=x+1;x1<playManageModeRows;x1++){
					preNum=numList[x1,y];
					if(preNum!=null){
						if(num.name.Equals(preNum.name)){
							//相同对象，合并，不在一栋当前对象
							markPos=-1;
							Destroy(num);
							numList[x,y]=null;
							preNum.SendMessage("updateLevels");
							preNum.transform.DOShakeScale(0.5f, new Vector3(0.5f,0.5f, 0));
							needCreate=true;
							audio.Play();
						}

						break;
					}
					else{
						markPos=x1;
					}
				}
				if(markPos>-1){
					numList[x,y]=null;
					numList[markPos,y]=num;
					num.transform.position+=new Vector3((markPos-x)*staticCreate.XOffset,0,0);
					needCreate=true;
				}
			}
		}
	if(needCreate){
		createNum();
	}

}
void upEvent(){

	print("up");
	bool needCreate=false;
	//第0行不需要移动
	for (int y=1;y<playManageModeRows;y++){
		for(int x=0;x<playManageModeRows;x++){
			num=numList[x,y];
			if(num==null){
				continue;
			}
			int markPos=-1;
			for(int y1=y-1;y1>=0;y1--){
			preNum=numList[x,y1];
			if(preNum!=null){
				//有元素，停止

				if(num.name.Equals(preNum.name)){
					//相同对象，合并，不在一栋当前对象
					markPos=-1;
					Destroy(num);
					numList[x,y]=null;
					preNum.SendMessage("updateLevels");
					preNum.transform.DOShakeScale(0.5f, new Vector3(0.5f,0.5f, 0));
					needCreate=true;
					audio.Play();
				}
				break;
			}
			else{
				markPos=y1;
			}
			}
		if (markPos>-1){
			//更新位置矩阵
			numList[x,y]=null;
			numList[x,markPos]=num;
			//移动
			float speed=1f;
			float step=speed*Time.deltaTime;
			num.transform.position+=new Vector3(0,(y-markPos)*staticCreate.YOffset,0);
			// test.transform.localPosition=new Vector3(Mathf.Lerp(gameObject.transform.localPosition.x, 10, step),Mathf.Lerp(gameObject.transform.localPosition.y, -3, step),Mathf.Lerp(gameObject.transform.localPosition.z, 50, step));
			// num.transform.position=new Vector3(staticCreate.XStartPos+x*staticCreate.XOffset,staticCreate.YStartPos+markPos*staticCreate.YOffset,0);
			
			// num.transform.localPosition=Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(10, -3, 50), step);
			// num.transform.Translate (Vector3.forward * speed * Time.deltaTime);
			// num.transform.Translate(new Vector3(1,0,0)*1000000f*Time.deltaTime);
			// num.transform.localPosition=Vector3.MoveTowards(num.transform.localPosition, new Vector3(-20, -3, 0), 1 * Time.deltaTime);
			// test.transform.localPosition=Vector3.MoveTowards(test.transform.localPosition, new Vector3(-20, -3, 0), 100 * Time.deltaTime);
			needCreate=true;
		}

		}
	}
	if(needCreate){
		createNum();
	}
}
void downEvent(){

	print("down");
	bool needCreate=false;
	//最后行不需要移动
	for(int y=playManageModeRows-2;y>=0;y--){
		for(int x=playManageModeRows-1;x>=0;x--){
			num=numList[x,y];
			if(num==null){
				continue;
			}
			int markPos=-1;
			for(int y1=y+1;y1<playManageModeRows;y1++){
				preNum=numList[x,y1];
				if(preNum!=null){

					if(num.name.Equals(preNum.name)){
						//相同对象，合并，不在一栋当前对象
						markPos=-1;
						Destroy(num);
						numList[x,y]=null;
						preNum.SendMessage("updateLevels");
						preNum.transform.DOShakeScale(0.5f, new Vector3(0.5f,0.5f, 0));
						needCreate=true;
						audio.Play();
					}

					break;
					
				}
				else{
					markPos=y1;
				}
			}
			if(markPos>-1){
				numList[x,y]=null;
				numList[x,markPos]=num;
				num.transform.position-=new Vector3(0,(markPos-y)*staticCreate.YOffset,0);

				needCreate=true;
			}
		}
	}
	if(needCreate){
		createNum();
	}


}

}
