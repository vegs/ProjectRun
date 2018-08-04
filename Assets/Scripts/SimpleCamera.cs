using UnityEngine;
using System.Collections;

public class SimpleCamera : MonoBehaviour {
	public GameObject body;
	//public GameObject head;

	//Smoothing and Damping
	private Vector3 velocityCamSmooth = Vector3.zero;
	[SerializeField]
	private float camSmoothDampTime = 0.1f;

	public float _positionModifier = 15f;
	public float _defaultPosMod = 15f;
	/*
	public float PositionModifier{
		get { return _positionModifier; }
		set { _positionModifier = value; }
	}
	*/

	public Vector3 _externalV3Add;

	private Vector3 _currLookDir = Vector3.zero;
	public Vector3 currLookDir{
		get { return _currLookDir; }
	}

	// Bool to determine whether the camera should move/follow or not
	public bool _autoMove = true;
	public bool _compForWalls = true;
	public bool autoMover{
		get { return _autoMove; }
		set { _autoMove = value; }
	}


	//void LateUpdate() {
	void FixedUpdate() {
		if(body != null){

			//Clamps the modifier
			_positionModifier = Mathf.Clamp(_positionModifier, 2f, 250f);

			Vector3 temp2 = (this.transform.position - body.transform.position);
		temp2 = Vector3.Normalize(temp2);
		Vector3 targetPosition = body.transform.position + _positionModifier * temp2;

			// Add a small adjustment up - looks nicer
			//targetPosition += new Vector3(0,2,0); // nope!

			// Add any external additions, and then wipe it
			targetPosition += _externalV3Add;
			_externalV3Add = Vector3.zero;
			

			targetPosition.y = Mathf.Clamp(targetPosition.y, 3, _positionModifier);


			Vector3 temp = targetPosition;

			// check if the camera passes through a wall, and compensate if that is the case
			if(_compForWalls){
			CompensateForWalls(body.transform.position , ref temp);
			}

			
			//transform.position = temp;
			//Vector3 targetPos = target.transform;
			//print ("camera: " + temp);

			if (_autoMove){
			smoothPosition(this.transform.position, temp);
			}

				transform.LookAt(body.transform);


			//transform.Translate(test.x, test.y +5, test.z -7);
			//transform.position.Equals(test);//.Set(test.x, test.y + 5, test.z - 7);

			_currLookDir = body.transform.position - this.transform.position;
			
		}
	}

	private void smoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		//making a smooth transition, of this object, between one position (fromPos) and another (toPos)
		this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		Debug.DrawLine(fromObject,toTarget,Color.red );
		
		//Compensate for walls between camera
		RaycastHit wallHit = new RaycastHit();
		if(Physics.Linecast(fromObject,toTarget, out wallHit))
		{
			Debug.DrawRay(wallHit.point,Vector3.left,Color.red);
//			toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
			toTarget = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
		}
	}

	
}
