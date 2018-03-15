using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wheel {
	public WheelCollider wheelCollider;
	public Transform visual;
	public bool F;
}

public class CarController : MonoBehaviour {
	
	public List<GameObject> FWheelsIn;
	public List<GameObject> RWheelsIn;

	public List<Wheel> wheels;
	
	public float tourqe;
	public float steering;

	public void Start () {
		foreach (GameObject wheel in FWheelsIn) {
			Wheel result = new Wheel();
			result.wheelCollider = wheel.GetComponent<WheelCollider>();
			result.visual = wheel.transform.GetChild(0);
			result.F = true;
			wheels.Add(result);
		}
		foreach (GameObject wheel in RWheelsIn) {
			Wheel result = new Wheel();
			result.wheelCollider = wheel.GetComponent<WheelCollider>();
			result.visual = wheel.transform.GetChild(0);
			wheels.Add(result);
		}
	}

	public void Update () {
		foreach (Wheel wheel in wheels) {
			float currentTourqe = tourqe*Input.GetAxis("Vertical");
			float currentSteering = steering*Input.GetAxis("Horizontal");
			if (currentTourqe != 0) {
				wheel.wheelCollider.brakeTorque = 0;
				wheel.wheelCollider.motorTorque = currentTourqe;
			} else {
				wheel.wheelCollider.brakeTorque = Mathf.Abs(tourqe);
				wheel.wheelCollider.motorTorque = 0;
			}
			if (wheel.F) {
				wheel.wheelCollider.steerAngle = currentSteering;
				wheel.visual.localRotation = Quaternion.Euler(wheel.visual.localRotation.x, wheel.visual.localRotation.y, wheel.visual.localRotation.z);
			}
			wheel.visual.Rotate(0, currentTourqe*Time.deltaTime, 0);
		}
	}
}
