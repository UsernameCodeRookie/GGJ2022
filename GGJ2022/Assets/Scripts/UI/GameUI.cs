using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Gameplay;

public class GameUI : MonoBehaviour{
	public string path="Assets\\GameAssets\\Textures\\UI\\White";
//	public string playerName="LeftGrid/Origin/PlayerL";
    public PlayerScript sc;

//	public int hpVal,skillCnt;
//	public float mpVal,spVal,spMax=5f;
	
//	public PlayerScript sc;
	private Texture2D[] mp=new Texture2D[7],hp=new Texture2D[4],skill=new Texture2D[4];
	private Texture2D spBar;
	
    void Start(){
		for(int i=0;i<=6;i++)mp[i]=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\mp"+i+".png",typeof(Texture2D));
		hp[0]=(Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\GameAssets\\Textures\\Opaque.png",typeof(Texture2D));
		for(int i=1;i<=3;i++)hp[i]=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\hp"+i+".png",typeof(Texture2D));
		skill[0]=(Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\GameAssets\\Textures\\Opaque.png",typeof(Texture2D));
		for(int i=1;i<=3;i++)skill[i]=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\skill"+i+".png",typeof(Texture2D));
		spBar=(Texture2D)AssetDatabase.LoadAssetAtPath(path+"\\spBar.png",typeof(Texture2D));
    }
	
    public void Init(PlayerScript t){
//		GameObject tar=GameObject.Find(playerName);
//		sc=t;
	}
	
    void Update(){
		var mpTar=transform.Find("mp").GetComponent<RawImage>();
		mpTar.texture=mp[Mathf.FloorToInt(sc.mp)];
		
    	var hpTar=transform.Find("hp").GetComponent<RawImage>();
		hpTar.texture=hp[sc.hp];
		
    	var skillTar=transform.Find("skill").GetComponent<RawImage>();
		skillTar.texture=skill[sc.availableAttackCount];
		
    	var spMTar=transform.Find("spBarMid");
		spMTar.localScale=new Vector3(0.3925f*sc.sp/sc.data.maxSp,0.08750676f,1);
    	var spRTar=transform.Find("spBarRight");
		spRTar.localPosition=new Vector3(39.23f*sc.sp/sc.data.maxSp-17.73f,-18.24f,0);
    }
}
