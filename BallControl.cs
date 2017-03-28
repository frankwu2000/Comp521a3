using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
	public Grid grid2d;
	public Rigidbody Rb;
	public Vector2 TargetGrid;
	public Vector3 StartPos;
	public int gridx;
	public int gridy;


	void Start () {
		Rb = gameObject.GetComponent<Rigidbody> ();
		gridx = 10;
		gridy = 10;

	}
	void Awake(){
		grid2d = GameObject.Find ("Grid").GetComponent<Grid>();
	}
	void Update () {


		if (Input.GetKeyDown(KeyCode.D)) {
			gridx++;
			StartPos = grid2d.grid[gridx, gridy].WorldPosition;
			transform.position = StartPos;
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			gridy++;
			StartPos = grid2d.grid[gridx, gridy].WorldPosition;
			transform.position = StartPos;
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			gridx--;
			StartPos = grid2d.grid[gridx, gridy].WorldPosition;
			transform.position = StartPos;
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			gridy--;
			StartPos = grid2d.grid[gridx, gridy].WorldPosition;
			transform.position = StartPos;
		}

	}

	void MoveUp(){
		
	}

	void MoveDown(){
		
	}

	void MoveLeft(){
	}

	void MoveRight(){
	}
}


