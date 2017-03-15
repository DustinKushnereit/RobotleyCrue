using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour {

	public AudioSource audio;
	public AudioClip otherClip;

    //public float timer;
    //public bool isStartLoopMusic = false;

    void Start()
	{
        //timer = 0.0f;
		//audio = GetComponent<AudioSource>();

        audio.Play();
        //print(audio.clip.length);
        float time = audio.clip.length + 2.3f;
        Invoke("ChangeSong", time);
        //yield return new WaitForSeconds(audio.clip.length);
        //isStartLoopMusic = true;
    }

    //void Update()
    //{
    //    timer += Time.deltaTime;
    //}

    void ChangeSong()
    {
        //print(timer);
        audio.clip = otherClip;
        audio.Play();
        audio.loop = true;
    }

    public void changeVolume(float volume)
    {
        if (volume < 0.0f)
            volume = 0.0f;
        else if (volume > 1.0f)
            volume = 1.0f;

        audio.volume = volume;
    }
}
