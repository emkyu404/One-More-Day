using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBasicBehavior : MonoBehaviour 
{
	//Get the maximum position for the indicator.
	public GameObject maxValueObject;
	
	//Get the minimum position for the indicator.
	public GameObject minValueObject;

	//Select a gameObject to act as an origin point.
	public GameObject sensorBay;

	//Associate a gameObject to act as an indicator.
	public GameObject indicator;
	
	//Select a gameObject to act as a target. This can be changed to an Array if multiple objects are a target.
	public GameObject target;

	//Desired sensor range to work
	//How far/close the target can be in order to be detected.
	public float lowSignalDistance;
	public float highSignalDistance;
	

	void Update () 
	{
		float distance = CalculateDistance(sensorBay.transform.position, target.transform.position);
		float lerpDistance = Mathf.Lerp(lowSignalDistance, highSignalDistance, (distance));
		float clampPositionY = Mathf.Clamp(lerpDistance, minValueObject.transform.position.y, maxValueObject.transform.position.y);
		
		Vector3 indicatorPosition = indicator.transform.position;
		
		indicator.transform.position = UpdatedSensor(indicatorPosition, lerpDistance, clampPositionY);

		//Debug process for testing purposes.
		//print("Difference: " + distance + "\nDistance lerp: " + lerpDistance + "\nClamped Value: " + clampPositionY);
	}

	//Calculate the distance between the desired item to track and the sensor prefab. It takes two Vector3, one for each object, and returns the distance in a float form.
	public float CalculateDistance (Vector3 origin, Vector3 target)
	{
		//Generate the float value using Unity's builtin Vector3.Distance.
		float dist = Vector3.Distance(target, origin);

		//Return the value
		return dist;
	}

	//Update the sensor position based on the calculated distance and given min/max values.
	public Vector3 UpdatedSensor (Vector3 inputVector, float lerp, float clamp)
	{
		//Interpolate the indicator's position.
		if (lerp >= lowSignalDistance)
		{
			inputVector = new Vector3 (inputVector.x, maxValueObject.transform.position.y, inputVector.z);
		}
		else if (lerp <= highSignalDistance)
		{
			inputVector = new Vector3 (inputVector.x, minValueObject.transform.position.y, inputVector.z);
		}
		else
		{
			inputVector = new Vector3 (inputVector.x, clamp, inputVector.z);
		}

		//Return the value.
		return inputVector;
	}
}
