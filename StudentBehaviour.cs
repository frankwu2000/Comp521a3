using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentBehaviour : MonoBehaviour {
	
	public GameObject gridObject;
	public Vector3 targetPostion;

	public string assignedProf;
	public Queue<KeyValuePair<string, Vector3>> memory;
	public List<Professor> profs;
	public List<Plaque> plaqs;

	public Color calm;
	public Color annoyed;
	public Color frustrated;
	private Material materialColored;


	float timer = 0f ;
	float waitTime = 2f;


	public bool moodMode;
	float timerMood = 0f;
	float waitTimeMood1 = 0.1f;
	float waitTimeMood2 = 3f;
	float oldDistance;
	float currentDistance;
	int mood = 1;

	// Use this for initialization
	void Start () {	
		moodMode = true;
		//initialize memory
		memory = new Queue<KeyValuePair<string, Vector3>>();
		//initialize all professors and plaque
		profs = new List<Professor>();
		plaqs = new List<Plaque> ();
		//six professors
		profs.Add(new Professor("prof1", new Vector3(-21,0,-2)));
		profs.Add(new Professor("prof2", new Vector3(-10,0,11)));
		profs.Add(new Professor("prof3", new Vector3(8,0,11)));
		profs.Add(new Professor("prof4", new Vector3(21,0,2)));
		profs.Add(new Professor("prof5", new Vector3(10,0,-11)));
		profs.Add(new Professor("prof6", new Vector3(-8,0,-11)));
		//six plaques

		plaqs.Add(new Plaque (profs[0],new Vector3(-18,0,0)));
		plaqs.Add(new Plaque (profs[1],new Vector3(-9,0,8)));
		plaqs.Add(new Plaque (profs[2],new Vector3(9,0,8)));
		plaqs.Add(new Plaque (profs[3],new Vector3(18,0,0)));
		plaqs.Add(new Plaque (profs[4],new Vector3(9,0,-8)));
		plaqs.Add(new Plaque (profs[5],new Vector3(-9,0,-8)));

		//random assigned prof from start
		assignedProf = profs[Random.Range(0,6)].getName();
		foreach (Professor p1 in profs){
			if(assignedProf.CompareTo(p1.getName())==0){
				targetPostion = p1.getPos();
			}
		}



	}

	void FixedUpdate(){
		
//		Debug.Log (timerMood);
		if (timerMood > 5) {
			timerMood = 0;
		}
		timerMood += Time.fixedDeltaTime;
		//check distance
		currentDistance = DistanceToAssigned();
		if (timerMood == 0) {
			oldDistance = DistanceToAssigned ();

		}

		if (timerMood > waitTimeMood1) {
			if (oldDistance < currentDistance) {
				mood = 2;
			} else {
				mood = 1;
			}
		} else if (timerMood > waitTimeMood2) {
			if (oldDistance < currentDistance) {
				mood = 3;
			} 

		}

	}

	// Update is called once per frame
	void Update () {
		
		changeColor (mood);

		//implement hard-coded behaviour tree
		if(ReachTarget(targetPostion)){

			//if reach a professor
			if(ReachTarget(GetPosByName(assignedProf))){
				//50% chance go idle
				if(Random.Range(0,2) == 0){
					//go idle
					targetPostion = ReturnRandomPosition();

				}else{
					//talk and being assigned to a new professor
					Professor newAssignedProf = profs[Random.Range(0,6)];
					assignedProf = newAssignedProf.getName ();
					//if remember assigned prof office
					if (IsRememberProf (newAssignedProf.getName ())) {
						targetPostion = newAssignedProf.getPos ();
					} else {
						//if dont remember assigned prof office
						targetPostion = plaqs[Random.Range(0,6)].getPos();
					}
				}

			//if reach a plaque
			}else if(IsReachingAnyPlaque()){
				Plaque reachedPlaque = GetPlaqueByPos ();
				string tempProName = reachedPlaque.getProfName ();
				//if the professor on plaque not in memory, add 
				if(!IsRememberProf(tempProName)){
					memory.Enqueue(new KeyValuePair<string,Vector3>(reachedPlaque.getProfName(),reachedPlaque.getProfPos()));
				}
				//if memory > 4, dequeue
				if (memory.Count > 4) {
					memory.Dequeue ();
				}
				if (tempProName.CompareTo (assignedProf) == 0) {
					targetPostion = reachedPlaque.getProfPos ();
				} else {
					targetPostion = plaqs[Random.Range(0,6)].getPos();
				}

			}
			//reach neither plaque nor prof, random position
			else{
				//wait for 0.5 - 3 s
				float waitTime = Random.Range(0.5f,3f);
//				timer += Time.time;
//				if(timer> waitTime){
//					timer = 0f;
//
//				}
				//assigned a new professor
				Professor newAssignedProf = profs[Random.Range(0,6)];
				assignedProf = newAssignedProf.getName ();
				//if remember assigned prof office
				if (IsRememberProf (newAssignedProf.getName ())) {
					targetPostion = newAssignedProf.getPos ();
				} else {
					//if dont remember assigned prof office
					targetPostion = plaqs[Random.Range(0,6)].getPos();
				}

			}

		}

	}

	void changeColor(int mood){
		//if calm ,  1
		//if annoyed,  2
		//if frustrated,  3
		//implement color change
		if(moodMode){
			if (mood == 1) {
				//calm	
				materialColored = new Material(Shader.Find("Diffuse"));
				materialColored.color = calm;
				this.GetComponent<Renderer>().material = materialColored;
			}else if(mood == 2){
				//annoyed
				materialColored = new Material(Shader.Find("Diffuse"));
				materialColored.color = annoyed;
				this.GetComponent<Renderer>().material = materialColored;
			}else if(mood == 3){
				//frustrated
				materialColored = new Material(Shader.Find("Diffuse"));
				materialColored.color = frustrated;
				this.GetComponent<Renderer>().material = materialColored;
			}
		}
	}

	float DistanceToAssigned(){
		Vector3 direction =GetPosByName(assignedProf) -transform.position;
		return direction.magnitude;

	}


	Vector3 ReturnRandomPosition(){
		float randomX = Random.Range (-18f,18f);
		float randomZ = Random.Range(-8f,8f);
		return new Vector3 (randomX,0,randomZ);
	}

	Vector3 GetPosByName(string profName){
		Vector3 pos = new Vector3();
		foreach (Plaque pl in plaqs){
			if(profName.CompareTo(pl.getProfName())==0){
				pos = pl.getProfPos();
			}
		}
		return pos;
	}

	bool IsReachingAnyPlaque(){
		foreach(Plaque pl in plaqs){
			Vector3 distance = pl.getPos() - transform.position;
			if (distance.magnitude<2) {
				return true;
			}
		}
		return false;
	}

	Plaque GetPlaqueByPos(){
		foreach(Plaque pl in plaqs){
			Vector3 distance = pl.getPos() - transform.position;
			if (distance.magnitude<2) {
				return pl;
			}
		}
		Debug.Log ("should not reach here, GetPlaqueByPos");
		Professor temp =  profs[Random.Range (0, 6)];
		return new Plaque(temp, temp.getPos());
	}


		
	bool IsRememberProf(string profName){
		foreach(KeyValuePair<string,Vector3> k in memory){
			if(k.Key.CompareTo( profName)==0){
				return true;
			}
		}
		return false;
	}

	Vector3 GetProfPosFromMem(string profName){
		foreach(KeyValuePair<string,Vector3> k in memory){
			if(k.Key.CompareTo( profName)==0){
				return k.Value;
			}
		}
		return Vector3.zero;
	}


	bool ReachTarget(Vector3 target){
		Vector3 distance = target - transform.position;
		if (distance.magnitude<2) {
			return true;
		}
		return false;
	}

	string GetNewAssignedProf(string old_prof){
		return profs[Random.Range(0,6)].getName();
	}


//	bool ReachAssignedProf(){
//		Vector3 goodPos = new Vector3();
//		foreach (Professor prof in profs){
//			if(assignedProf.CompareTo(prof.getName()) == 0){
//				goodPos = prof.getPos();
//			}
//		}
//
//		return ReachTarget (goodPos);
//	}

}
