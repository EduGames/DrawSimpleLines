using UnityEngine;
using System.Collections;

public class dragable : MonoBehaviour {

	// Used for Dragging
	private Vector3 screenPoint;
	private Vector3 offset;

	// Game Play options
//	public bool GoAndBack;
	public bool closedShape;

	// Draw Lines
	public Material lineMaterial;
	private LineRenderer lineDraw;

	// Shape Lines
	public GameObject[] lines;
	private GameObject activeLine;
	private int activeLineIndex;
	private Transform TargetPoint;

	// Game State
	private bool newLine;
	private bool gameEnded;

	void Awake(){
		// Joint Start points with the other EndPoints.
		for (int i = 1; i < lines.Length; i++) {
			lines[i].transform.FindChild("StartPoint").position = lines[i-1].transform.FindChild("EndPoint").position;
		}
		if (closedShape && lines.Length > 2) {
			lines[lines.Length - 1].transform.FindChild("EndPoint").position = lines[0].transform.FindChild("StartPoint").position;
		}

		// Set Start states
		activeLineIndex = 0;
		activeLine = lines [activeLineIndex];
		TargetPoint = activeLine.transform.FindChild("EndPoint");

		// Set Position of Player to Start Point #1
		transform.position = lines[0].transform.FindChild("StartPoint").position;
		updateRotation (TargetPoint);

		// Set/Get GameObject Components
		lineDraw = activeLine.GetComponent<LineRenderer>();

	}

	void OnMouseDown() {
		if (gameEnded) return;
		newLine = true;
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseDrag()
	{
		if (!newLine || gameEnded)
			return;

		updatePosition ();

		if (transform.position == TargetPoint.position){
			if(activeLineIndex + 1 < lines.Length){
				initActiveLine();
			}else{
				gameEnded = true;
			}
//		} else if (GoAndBack && transform.position == activeLine.transform.FindChild("StartPoint").position && activeLineIndex > 0){
//			// May be Deleted at the end  ( I am not sure if I will support DrawBack )
//			lineDraw.SetPosition (0, transform.position);
//			lineDraw.SetPosition (1, transform.position);
//			activeLine = lines[--activeLineIndex];
//			TargetPoint = activeLine.transform.FindChild("EndPoint");
//			lineDraw = activeLine.GetComponent<LineRenderer>();
		}
		if(TargetPoint.position != transform.position)
			updateRotation (TargetPoint);
		lineDraw.SetPosition ( 1 , transform.position);
	}

	private void updateRotation(Transform position){
		var dir = position.position - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void updatePosition ()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		
		transform.position = GetClosestPointOnLineSegment ((Vector2) activeLine.transform.FindChild("StartPoint").position,
		                                                   (Vector2) activeLine.transform.FindChild("EndPoint").position,
		                                                   (Vector2) curPosition);
	}

	void initActiveLine ()
	{
		lineDraw.SetPosition (1, transform.position);
		activeLine = lines[++activeLineIndex];
		lineDraw = activeLine.GetComponent<LineRenderer>();
		lineDraw.SetPosition (0, transform.position);
		lineDraw.SetPosition (1, transform.position);
		
		TargetPoint = activeLine.transform.FindChild("EndPoint");
		newLine = false;
	}

	public static Vector2 GetClosestPointOnLineSegment(Vector2 A, Vector2 B, Vector2 P)
	{
		Vector2 AP = P - A;       //Vector from A to P   
		Vector2 AB = B - A;       //Vector from A to B  
		
		float magnitudeAB = AB.sqrMagnitude;     //Magnitude of AB vector (it's length squared)     
		float ABAPproduct = Vector2.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b     
		float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  
		
		if (distance < 0){     //Check if P projection is over vectorAB     
			return A;
		} else if (distance > 1) {
			return B;
		} else {
			return A + AB * distance;
		}
	}
}
