using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPoints : MonoBehaviour {
	public GameObject[] points;
	private List<GameObject> points_private;
	RaycastHit hit;
	
	private LineRenderer line;
	private List<Vector3> pointsList;

	// Use this for initialization
	void Start () {
		line = gameObject.AddComponent<LineRenderer>();
		line.SetWidth(0.1f,0.1f);
		line.SetColors(Color.green, Color.green);
		line.useWorldSpace = true;    
		pointsList = new List<Vector3>();
		points_private = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began){
				foreach(GameObject point in points){
					Ray ray = Camera.main.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray,out hit)){
						if (hit.transform.gameObject == point){
							Debug.Log ("Start");
							points_private = new List<GameObject>(points);
							points_private.Remove(point);

							line.SetVertexCount(0);
							pointsList.RemoveRange(0,pointsList.Count);
							break;
						}
					}
				}
			}else if(Input.GetTouch(0).phase == TouchPhase.Moved){
				if(points_private.Count > 0){
					addPoint2Line(Camera.main.ScreenToWorldPoint(touch.position));

					foreach(GameObject point in points_private){
						Ray ray = Camera.main.ScreenPointToRay(touch.position);
						if (Physics.Raycast(ray,out hit)){
							if (hit.transform.gameObject == point && points_private.Contains(point)){
								Debug.Log ("Point");
								points_private.Remove(point);
								break;
							}
						}
					}
				}
			}else if(Input.GetTouch(0).phase == TouchPhase.Ended){
				if (points_private.Count == 0) return;
				points_private.Clear();
			}
		}
	}

	private void addPoint2Line (Vector3  position)
	{
		position.z= 0 ;             
		if (!pointsList.Contains (position))              
		{
			pointsList.Add (position);
			line.SetVertexCount (pointsList.Count);
			line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);          
		}
	}
}
