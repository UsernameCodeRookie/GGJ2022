using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay{
public class PlayerScript:MonoBehaviour{
    public PlayerDataSO data;
	public float spd,sp,mp;
	public int hp;
	
    public void Init(){
		spd = data.initSpeed;
		sp = 0;
		mp = 0;
		hp = data.maxHp;
    }
	
    public void Upd(){
		mp+=data.mpRecoverRate*Time.deltaTime;
		if(mp>data.maxMp)mp=data.maxMp;
    }

    public void GetFruit(){
		mp+=1f;
		if(mp>data.maxMp)mp=data.maxMp;
		sp=data.maxSp;
    }
	
    public void Die(){
		hp--;
		if(hp<=0){
			Debug.LogError("Die");
			Destroy(gameObject);
		}
    }
}}
