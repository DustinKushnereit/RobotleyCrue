using UnityEngine;
using System.Collections;

public class MusicParticles : MonoBehaviour
{
    public GameObject projectingMusicParticles;
    GameObject projectingMusic;
    GameObject redSphere;

    // Use this for initialization
    void Start ()
    {
        projectingMusicParticles = GameObject.FindGameObjectWithTag("RedMusicParticle");
        redSphere = GameObject.FindGameObjectWithTag("RedSphere");
        projectingMusic = (GameObject)Instantiate(projectingMusicParticles, transform.position, projectingMusicParticles.transform.rotation);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(redSphere != null && projectingMusic != null)
            projectingMusic.transform.position = redSphere.transform.position;

    }
}
