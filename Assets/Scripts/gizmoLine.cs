using UnityEngine;
using System.Collections;

public class gizmoLine : MonoBehaviour {

	public Transform start;
	public Transform end;

	void OnDrawGizmos ()
	{
		Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
		Gizmos.DrawLine(start.position, end.position);
	}

	void Awake(){
		resetLines ();
	}

	public void resetLines(){
		LineRenderer lineDraw = GetComponent<LineRenderer> ();
		lineDraw.SetPosition (0, start.position);
		lineDraw.SetPosition (1, start.position);
	}
}
