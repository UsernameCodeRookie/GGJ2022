using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Gameplay;

public class GameUI : MonoBehaviour{
	public string path="Assets\\GameAssets\\Textures\\UI\\White";
	public string playerName="LeftGrid/Origin/PlayerL";
	
//	public PlayerScript sc;
	private Texture2D[] mp=new Texture2D[7],hp=new Texture2D[4],skill=new Texture2D[4];
	private Texture2D spBar;
	
    void Start(){
		for(int i=0;i<=6;i++)mp[i]=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\mp"+i+".png",typeof(Texture2D));
		for(int i=1;i<=3;i++)hp[i]=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\hp"+i+".png",typeof(Texture2D));
		for(int i=1;i<=3;i++)skill[i]=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\skill"+i+".png",typeof(Texture2D));
		spBar=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\spBar.png",typeof(Texture2D));
    }
	
    public void Init(PlayerScript t){
//		GameObject tar=GameObject.Find(playerName);
//		sc=t;
	}
	
    void Update(){
		
    }
}
