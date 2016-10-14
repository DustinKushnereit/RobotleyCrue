using UnityEngine;
using System.Collections;

public class EnableMusicScript : MonoBehaviour {

	public GameObject gameObject;
	
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioSource>().enabled = true;
	}
}
