using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {
	public GameObject[] points;
	public GameObject player;
	bool isTouched;
	private LineRenderer line;
	private List<Vector3> pointsList;

	RaycastHit2D hit;

	// Use this for initialization
	void Start () {
		line = gameObject.AddComponent<LineRenderer>();
		line.SetWidth(0.1f,0.1f);
		line.SetColors(Color.green, Color.green);
		line.useWorldSpace = true;

		pointsList = new List<Vector3>();
		for (int i = 0; i < points.Length; i++) {

			Vector3 point = points[i].transform.position;




			line.SetVertexCount (i + 1);
			line.SetPosition (i, point);

//			if ( i + 1 < points.Length){
//				Vector3 nextPoint = points[i+1].transform.position;
//				CapsuleCollider capsule = gameObject.AddComponent<CapsuleCollider>();
//
//				capsule.radius = 0.2f;
////				capsule.center = Vector3.zero;
//				capsule.direction = 2;
//
//				capsule.transform.position = point + (nextPoint - point) / 2;
//				capsule.transform.LookAt(point);
//				capsule.height = (nextPoint - point).magnitude;
////				capsule.isTrigger = true;
//			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("------------ENTER");
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("------------EXIT");
	}

	void OnTriggerStay(Collider other) {
		Debug.Log ("------------STAY");  
	}


	
	//	void OnDrawGizmos ()
//	{
//		Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
//		
//		for (int i = 0; i +1 < points.Length; i++) {
//			Gizmos.DrawLine(points[i].transform.position, points[i+1].transform.position);
//		}
////		if(pointsList.Count == 2 )
////			Gizmos.DrawLine (points[0].transform.position, points[1].transform.position);
//	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began){
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
//				if (Physics2D.Raycast(ray,out hit)){
//					if (hit.transform.gameObject == player){
//						isTouched = true;
//					}
//				}
			}else if(Input.GetTouch(0).phase == TouchPhase.Moved){
				if(isTouched){
					Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
					position.z = 10;
					player.transform.position = position;
				}
			}else if(Input.GetTouch(0).phase == TouchPhase.Ended){
				isTouched = false;
			}
		}
	}
}
