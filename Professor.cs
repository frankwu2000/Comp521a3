using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professor {
	string profName;
	Vector3 profPosition;

	public Professor(string profName,Vector3 profPosition){
		this.profName = profName;
		this.profPosition = profPosition;
	}

	public string getName(){
		return profName;
	}

	public Vector3 getPos(){
		return profPosition;
	}
}
