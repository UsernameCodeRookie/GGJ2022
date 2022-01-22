using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class PlayerCtrlL:MonoBehaviour{
	private PlayerScript sc;
	private Vector3 dir;
	
    void Start(){
        dir=new Vector3(0,-1,0);
		sc=gameObject.GetComponent<PlayerScript>();
		sc.Init();
    }
	
    void Update(){
		Vector3 op=new Vector3(Input.GetAxis("Horizontal L"),Input.GetAxis("Vertical L"),0);
		if(op!=Vector3.zero)dir=op.normalized;
		this.transform.Translate(dir*sc.speed*Time.deltaTime);
		sc.Upd();
    }
}
