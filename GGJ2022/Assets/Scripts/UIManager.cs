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
		gameManager.GameOver.AddListener(End);
	}
	
    void Update(){
		if(gameManager.isPlaying && Input.GetKeyDown("escape"))
        {
			if (state != 3)
				Pause();
			else
				Resume();
        }
	}
	
	public void GameReset(){
		state=2;
		startUI.SetActive(false);
		endUI.SetActive(false);
		playUI.SetActive(true);
		pauseUI.SetActive(false);
		Time.timeScale = 1;

		gameManager.Reset();
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

	public void Pause()
    {
		state = 3;
		pauseUI.SetActive(true);
		Time.timeScale = 0;
	}

	public void Resume()
    {
		state = 2;
		pauseUI.SetActive(false);
		Time.timeScale = 1;
    }

	public void End() 
	{
		state = 1;
		endUI.SetActive(true);
		Time.timeScale = 0;
	}
    
	public void GameExit(){
		Application.Quit();
	}
}
