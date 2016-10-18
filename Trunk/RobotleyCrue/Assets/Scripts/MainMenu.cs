using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject waveManager;
    //public GameObject kinectManager;
    public GameObject mainCamera;
    public GameObject playerHealthBar;
    public GameObject canvasMenu;
    public GameObject menuGameObjects;
    public Text p1M16;
    public Text p2M16;
    public Text p1Shotgun;
    public Text p2Shotgun;
    public GameObject m16GO;
    public GameObject shotgunGO;
    public GameObject continueGO;
    public GameObject metalFloor;
    public GameObject pillar9;
    public GameObject pillar5;

    GameObject player;
    
    bool player1CanContinue;

    void Start ()
    {
        player1CanContinue = false;
    }
	
	void Update ()
    {
        checkDistance();
        checkContinue();
	}

    void checkDistance()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<Player>().m16)
        {
            player1CanContinue = true;
            player.GetComponent<Player>().shotgun = false;

            if (p1M16 != null)
                p1M16.enabled = true;

            if (p1Shotgun != null)
                p1Shotgun.enabled = false;
        }

        if (player.GetComponent<Player>().shotgun)
        {
            player1CanContinue = true;
            player.GetComponent<Player>().m16 = false;

            if (p1Shotgun != null)
                p1Shotgun.enabled = true;

            if (p1M16 != null)
                p1M16.enabled = false;
        }
    }

    void checkContinue()
    {
        if(player1CanContinue && player.GetComponent<Player>().Continue)
        {
            player.GetComponent<Player>().inMenu = false;
            
            metalFloor.SetActive(true);
            pillar9.SetActive(true);
            pillar5.SetActive(true);
            waveManager.SetActive(true);
            //kinectManager.SetActive(true);
            mainCamera.SetActive(true);
            mainCamera.GetComponent<CameraFollowPlayer>().inMenu = false;
            playerHealthBar.SetActive(true);
            canvasMenu.SetActive(false);
            menuGameObjects.SetActive(false);
        }
    }
}
