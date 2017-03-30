using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReservationTable{
	//public Dictionary<int,List<Node>> reservT;
	public Dictionary<Node , List<bool>> reservT;

	Grid gridClass;
	int tableSize;

	public ReservationTable(Grid gridClass,int tableSize){
		//this.reservT = new Dictionary<int, List<Node>> ();
		this.reservT = new Dictionary<Node, List<bool>>();

		this.gridClass = gridClass;
		this.tableSize = tableSize;
	}

	public void FirstSet(){
		foreach(Node n in gridClass.grid){
			List<bool> timeTable = new List<bool> ();
			for(int i = 0; i<tableSize;i++){
				if(n.Walkable){
					timeTable.Add (false);
				}else{
					timeTable.Add (true);
				}
			}

			reservT.Add (n, timeTable);

		}
	}

	public void Reset(){
		foreach(Node n in gridClass.grid){
			for(int i = 0; i<tableSize;i++){
				if(n.Walkable){
					reservT [n] [i] = false;
				}else{
					reservT [n] [i] = true;
				}
			}
		}
	}


	public bool CheckReserve(int time, Node node){
		return reservT [node] [time];	
	}

	public void Reserve(int time, Node node){
		reservT [node] [time] = true;
	}

}


