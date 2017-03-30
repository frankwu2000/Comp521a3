using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent{
	public Transform student;
	public List<Node> path;

	public Vector3 target;

	public Agent(Transform student){
		this.student = student;
	}

	public void SetTarget(Vector3 target){
		this.target = target;
	}

	public void SetPath(List<Node> path){
		this.path = path;
	}

	public bool ReachTarget(){
		if (student.position.x == target.x &&
			student.position.y == target.y &&
			student.position.z == target.z) {
			return true;
		}
		return false;
	}

}
