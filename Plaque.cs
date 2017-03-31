using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plaque {
	public Professor prof;
	public Vector3 plaqueLocation;

	public Plaque(Professor prof,Vector3 pLocation){
		this.prof = prof;
		this.plaqueLocation = pLocation;
	}

	public string getProfName(){
		return prof.getName ();
	}

	public Vector3 getProfPos(){
		return prof.getPos ();
	}

	public Vector3 getPos(){
		return plaqueLocation;
	}

}

