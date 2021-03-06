using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using UI;

public class UIManager:MonoBehaviour{
	public GameObject startUI,endUI,playUI,pauseUI,gameMgr;
	public GameObject helpUI;
	private GameManager gameManager;
	
	private int state;//0:start_1:end_2:play_3:pause
	
    void Awake(){
		state=0;
		startUI.SetActive(true);
		endUI.SetActive(false);
		playUI.SetActive(false);
		pauseUI.SetActive(false);
//		gameMgr.SetActive(false);
//		gameMgr.GetComponent<GameManager>().GameOver.AddListener(GameEnd);

		gameManager = gameMgr.GetComponent<GameManager>();
		gameManager.GameOver.AddListener(GameEnd);
	}
	
    void Update(){
		if(state==2&&Input.GetKey("escape"))GamePause();
	}
	
	public void GameReset(){
		state=2;
		startUI.SetActive(false);
		endUI.SetActive(false);
		playUI.SetActive(true);
		pauseUI.SetActive(false);
		gameMgr.SetActive(true);

		gameManager.GameReset.Invoke();
	}
	
	public void GameEnd(){
		state=1;
//		startUI.SetActive(false);
		endUI.SetActive(true);
		playUI.SetActive(false);
//		pauseUI.SetActive(false);
//		gameMgr.SetActive(false);
	}
	
	public void GamePause(){
		state=3;
//		startUI.SetActive(false);
//		endUI.SetActive(false);
		playUI.SetActive(false);
		pauseUI.SetActive(true);
		gameMgr.SetActive(false);
	}
	
	public void GameContinue(){
		state=2;
//		startUI.SetActive(false);
//		endUI.SetActive(false);
		playUI.SetActive(true);
		pauseUI.SetActive(false);
		gameMgr.SetActive(true);
	}
	
	public void HelpEnter()
    {
		helpUI.SetActive(true);
		helpUI.GetComponent<HelpUI>().frame = 0;
    }

	public void GameExit(){
		Application.Quit();
	}
}
