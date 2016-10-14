using UnityEngine;
using System.Collections;

public class GlobalMusicScript : MonoBehaviour
{
	public AudioSource gunSF;
	public AudioSource swordSF;
	public AudioSource walkSF;
	public AudioSource gunHitSF;
	public AudioSource swordHitSF;
	
	// Use this for initialization
	void Start () {
		//gameObject.GetComponent<AudioSource>().enabled = true;
	}
	
	public void playGunSound(){
        gunSF.Play();
	}
	
	public void playSwordSound(){
        swordSF.Play();
	}
	
	public void playWalkSound(){
        //if(!walkSF.isPlaying)
            //walkSF.Play();
	}
	
	public void playGunHitSound(){
        gunHitSF.Play();
	}
	
	public void playSwordHitSound(){
        swordHitSF.Play();
	}
}
