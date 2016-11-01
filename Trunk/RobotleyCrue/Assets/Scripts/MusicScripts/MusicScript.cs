using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour {

	public AudioSource audio;
	public AudioClip otherClip;

    //public bool isStartLoopMusic = false;
	
	IEnumerator Start()
	{
		audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = otherClip;
        audio.Play();
		audio.loop = true;
        //isStartLoopMusic = true;
	}

    public void changeVolume(float volume)
    {
        if (volume < 0.0f)
            volume = 0.0f;
        else if (volume > 1.0f)
            volume = 1.0f;

        audio.volume = volume;
    }

//    songStarted() {
//    previousFrameTime = getTimer();
//    lastReportedPlayheadPosition = 0;
//    mySong.play();
//}

//    void Update() {
//    songTime += getTimer() - previousFrameTime;
//    previousFrameTime = getTimer();
//    if(audio.time != lastReportedPlayheadPosition) {
//        songTime = (songTime + mySong.position)/2;
//        lastReportedPlayheadPosition = mySong.position;
//    }
//}
}
