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
    public GameObject player2HealthBar;
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

    GameObject player1;
    GameObject player2;

    bool player1CanContinue;
    bool player2CanContinue;

    void Start ()
    {
        player1CanContinue = false;
        player2CanContinue = false;
    }
	
	void Update ()
    {
        checkPlayer1();
        checkPlayer2();
        checkContinue();
	}

    void checkPlayer1()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");

        if (player1.GetComponent<Player>().m16)
        {
            player1CanContinue = true;
            player1.GetComponent<Player>().shotgun = false;

            if (p1M16 != null)
                p1M16.enabled = true;

            if (p1Shotgun != null)
                p1Shotgun.enabled = false;
        }

        if (player1.GetComponent<Player>().shotgun)
        {
            player1CanContinue = true;
            player1.GetComponent<Player>().m16 = false;

            if (p1Shotgun != null)
                p1Shotgun.enabled = true;

            if (p1M16 != null)
                p1M16.enabled = false;
        }
    }

    void checkPlayer2()
    {
        player2 = GameObject.FindGameObjectWithTag("Player2");

        if (player2.GetComponent<Player>().m16)
        {
            player2CanContinue = true;
            player2.GetComponent<Player>().shotgun = false;

            if (p2M16 != null)
                p2M16.enabled = true;

            if (p2Shotgun != null)
                p2Shotgun.enabled = false;
        }

        if (player2.GetComponent<Player>().shotgun)
        {
            player2CanContinue = true;
            player2.GetComponent<Player>().m16 = false;

            if (p2Shotgun != null)
                p2Shotgun.enabled = true;

            if (p2M16 != null)
                p2M16.enabled = false;
        }
    }

    void checkContinue()
    {
        if(player1.GetComponent<Player>().Continue)// && player2CanContinue && player2.GetComponent<Player>().Continue)
        {
            player1.GetComponent<Player>().inMenu = false;
            player2.GetComponent<Player>().inMenu = false;

            metalFloor.SetActive(true);
            pillar9.SetActive(true);
            pillar5.SetActive(true);
            waveManager.SetActive(true);
            //kinectManager.SetActive(true);
            mainCamera.SetActive(true);
            mainCamera.GetComponent<CameraFollowPlayer>().inMenu = false;
            playerHealthBar.SetActive(true);
            player2HealthBar.SetActive(true);
            canvasMenu.SetActive(false);
            menuGameObjects.SetActive(false);
        }
    }
}
