using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levels : MonoBehaviour {
	public Level[] levelsData;
	public GameObject panel;
	public GameObject btn;

	// Use this for initialization
	void Start () {
		foreach (Level level in levelsData) {
			GameObject lvlBtn = Instantiate(btn);
			lvlBtn.transform.SetParent(panel.transform);
			lvlBtn.GetComponent<Image>().color = level.bgColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
