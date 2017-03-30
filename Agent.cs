using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent{
	public GameObject student;
	public Vector3 currentPosition;
	public Vector3 target;
	public List<Node> path;
	public SilverPathfinding pathFindAlg;

	public Agent(GameObject student, SilverPathfinding pathFindAlg){
		this.student = student;
		this.currentPosition = student.transform.position;
		this.pathFindAlg = pathFindAlg;
	}

	public void SetTarget(GameObject target){
		this.target = target.transform.position;
	}

	public void SetPath(List<Node> path){
		this.path = path;
	}

	public void UpdatePosition(){
		this.currentPosition = student.transform.position;
	}

	public bool ReachTarget(){
		if (student.transform.position.x == target.x &&
			student.transform.position.y == target.y &&
			student.transform.position.z == target.z) {
			return true;
		}
		return false;
	}

}
