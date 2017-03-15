using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectsScript : MonoBehaviour {

    public List<AudioSource> messUpClips;

	void Start () {

	}
	
	// Update is called once per frame
    //void Update () {
	
    //}

    public void randomMessUpSF()
    {
        int rand = Random.Range(0, messUpClips.Count);

        messUpClips[rand].Play();
    }
}
