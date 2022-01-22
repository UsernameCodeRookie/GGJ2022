using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager:MonoBehaviour{
	public GameObject startUI,endUI,playUI,pauseUI,gameMgr;
	
	private int state;//0:start_1:end_2:play_3:pause
	
    void Start(){
		state=0;
		startUI.SetActive(true);
		endUI.SetActive(false);
		playUI.SetActive(false);
		pauseUI.SetActive(false);
		gameMgr.SetActive(false);
	}
	
    void Update(){
//		if(state==2&&Input.GetKey("escape")){
			
//		}
	}
}
