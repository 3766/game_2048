using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createFg : MonoBehaviour {
	public GameObject Fg;
	// Use this for initialization
	void Start () {
		if(staticCreate.playMode==4){
			Fg.transform.localScale = new Vector3 (4.3f, 4.3f, 0);
		}
		else if(staticCreate.playMode==5){
			Fg.transform.localScale = new Vector3 (3.5f, 3.5f, 0);
		}
		else if(staticCreate.playMode==6){
			Fg.transform.localScale = new Vector3 (2.9f, 2.9f, 0);
		}
		for(int i=0;i<staticCreate.playMode;i++){
			for (int j=0;j<staticCreate.playMode;j++){
				GameObject obj=Instantiate(Fg)as GameObject;
				obj.transform.position=new Vector3(
					staticCreate.XStartPos+i*staticCreate.XOffset,
					staticCreate.YStartPos-j*staticCreate.YOffset,
					1
				);
				obj.name="fg"+i+"_"+j;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
