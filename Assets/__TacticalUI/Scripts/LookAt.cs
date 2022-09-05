using System.Collections;
using UnityEngine;

public class LookAt : MonoBehaviour 
{
	public GameObject objective;
	
	void Update () 
	{
		Vector3 modTransform = new Vector3(objective.transform.position.x, 0, objective.transform.position.z);
		this.transform.LookAt(modTransform);
	}

}
