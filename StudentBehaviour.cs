using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentBehaviour : MonoBehaviour {
	public GameObject gridObject;
	public Vector3 targetPostion;
	public List<Vector3> plaquePosition;
	//public Dictionary<> rememberPosition;




	// Use this for initialization
	void Start () {	
		//set up prof memory slot and plaque position
//		profPostion = new Vector3[4];
//		plaquePosition = new List<Vector3>();
//		plaquePosition.Add ();
//		plaquePosition.Add ();
//		plaquePosition.Add ();
//		plaquePosition.Add ();
//		plaquePosition.Add ();
//		plaquePosition.Add ();
//

	}


	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			targetPostion = new Vector3 (-21,1,-1.5f);
		}
	}



}
