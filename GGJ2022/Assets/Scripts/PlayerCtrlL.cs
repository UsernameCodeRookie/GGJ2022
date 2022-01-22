using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlL : MonoBehaviour
{
	public float spd=2.3f;
	
	private Vector3 dir;
	
    void Start(){
        dir=new Vector3(0,-1,0);
    }
	
    void Update(){
		Vector3 op=new Vector3(Input.GetAxis("Horizontal L"),Input.GetAxis("Vertical L"),0);
		if(op!=Vector3.zero)dir=op.normalized;
		this.transform.Translate(dir*spd*Time.deltaTime);
    }
}