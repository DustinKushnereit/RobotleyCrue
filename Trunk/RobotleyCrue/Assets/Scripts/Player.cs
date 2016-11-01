using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float m_health;
    float MAXHEALTH = 20;

    GlobalMusicScript music;

    float timer;
    bool canAttack;

    //Movement
    //bool canMove;
    /*public bool reachEquilibrium;
    float incrementingX;
    bool moveLeft;
    bool moveRight;
    bool moveForward;
    bool moveBackward;*/
    float moveSpeed = 5.0f;
    float incrementingZ;

    //float leftStickX;
    //float leftStickY;
    float rightStickX;
    float rightStickY;

    public bool isPlayer2;
    public GameObject player1;
    public GameObject player2;
    //bool disableForwards = true;

    //Rail System
    //float timeTillMovement = 10.0f;
    //float timeTillStop = 10.0f;
    //bool activateSystem;

    //Rumble
    private float rumbleCounter = 0.0f;
    private const float MAX_RUMBLE = 0.25f;
    private bool isRumble;
    x360_Gamepad gamePad;

    //Public Objects
    public GameObject bullet;
    public GameObject gun;
    public GameObject beatsFireBlast;
    public GameObject aoeEnergyPulse;

    //Bullets
    public GameObject aoeShotBlue;
    public GameObject aoeShotGreen;
    public GameObject aoeShotRed;
    public GameObject aoeShotYellow;

    public GameObject energyShotBlue;
    public GameObject energyShotGreen;
    public GameObject energyShotRed;
    public GameObject energyShotYellow;
    
    bool holdingDownButtonRed;
    bool holdingDownButtonYellow;
    bool holdingDownButtonGreen;
    bool holdingDownButtonBlue;

    //UI Stuff
    public Slider healthSlider;
    public Slider beatsPowerSlider;

    //Invincible frames
    public bool m_Invincible;
    private float m_InvincibleFrames;
    private const float m_MAXINCIBILITY = 0.3f;

    //Gun
    public GameObject guitarTip;
    public GameObject avatarLeftHand;
    public GameObject playerLeftArm;
    public int ammoCount;
    public int maxAmmo;
    bool fireOnce;
    bool canReload;
    public Text ammoText;
    public int bulletSpeed = 45;

    public GameObject laserSightRed;
    public GameObject laserSightBlue;
    public GameObject laserSightGreen;
    public GameObject laserSightYellow;

    //Grenade
    public GameObject flashBang;
    bool canFireGrenade;
    public Text grenadeText;
    public int grenadeCount;
    public int maxGrenadeCount;

    //End Game Bools
    public bool youLoseBool = false;
    public bool youWinBool = false;
    private float timeToReset = 20.0f;
    public Text lossText;
    public Text winText;

    //Menu
    public bool inMenu;
    public bool m16;
    public bool shotgun;
    public bool Continue;
    public GameObject singleShotGuitarP1;
    public GameObject singleShotGuitarP2;
    public GameObject AOEGuitarP1;
    public GameObject AOEGuitarP2;

    public GameObject singleShotGuitarP1Model;
    public GameObject singleShotGuitarP2Model;
    public GameObject AOEGuitarP1Model;
    public GameObject AOEGuitarP2Model;

    bool canBePressed = true;

    public GameObject theCylinder;

    public bool kinectMode;

    //Audio Sources
    public GameObject guitarAudio;
    public GameObject guitarAudio2;
    public GameObject bassAudio;
    public GameObject bassAudio2;

    void Start ()
    {
        m_health = healthSlider.value = MAXHEALTH;
        beatsPowerSlider.value = 0;

        m_InvincibleFrames = m_MAXINCIBILITY;
        m_Invincible = false;

        if(!isPlayer2)
            gamePad = GamepadManager.Instance.GetGamepad(1);
        else
            gamePad = GamepadManager.Instance.GetGamepad(2);

        canAttack = true;
        
        /*moveLeft = false;
        moveRight = false;
        moveForward = false;
        moveBackward = false;
        reachEquilibrium = false;
        incrementingX = 0.0f;*/
        incrementingZ = 0.0f;

        //timeTillMovement = 10.0f;
        //timeTillStop = 5.0f;
        //activateSystem = true;
        //canMove = false;

        if (!isPlayer2)
            music = GameObject.Find("GlobalSF").GetComponent<GlobalMusicScript>();

        ammoCount = maxAmmo;
        grenadeCount = maxGrenadeCount;
        fireOnce = true;
        canReload = true;
        canFireGrenade = true;

        if (ammoText != null)
            ammoText.text = ammoCount + "-" + maxAmmo;

        if (grenadeText != null)
            grenadeText.text = grenadeCount + "-" + maxGrenadeCount;

        inMenu = true;
        shotgun = false;
        m16 = true;
        Continue = false;

        //Bullets
        holdingDownButtonRed = true;
        holdingDownButtonYellow = false;
        holdingDownButtonGreen = false;
        holdingDownButtonBlue = false;

        laserSightRed.SetActive(true);
        laserSightBlue.SetActive(false);
        laserSightGreen.SetActive(false);
        laserSightYellow.SetActive(false);

        //Menu
        //singleShotGuitarP1 = GameObject.FindGameObjectWithTag("SingleShotGuitarPlayer1");
        //singleShotGuitarP2 = GameObject.FindGameObjectWithTag("SingleShotGuitarPlayer2");
        //AOEGuitarP1 = GameObject.FindGameObjectWithTag("AOEGuitarPlayer1");
        //AOEGuitarP2 = GameObject.FindGameObjectWithTag("AOEGuitarPlayer2");

        singleShotGuitarP1.SetActive(true);
        AOEGuitarP1.SetActive(false);

        singleShotGuitarP2.SetActive(true);
        AOEGuitarP2.SetActive(false);

        singleShotGuitarP1Model.SetActive(true);
        singleShotGuitarP2Model.SetActive(true);
        AOEGuitarP1Model.SetActive(false);
        AOEGuitarP2Model.SetActive(false);

        kinectMode = false;
    }

    void Update ()
    {
        if (!inMenu)
        {
            checkWinLoss();

            //if (canMove)
            railSystem();
            

            if (isRumble)
                vibrateController();

            if (m_Invincible)
                checkInvibilityFrames();
            
            if (kinectMode)
            {
                moveGun();
                checkInputController();
            }
            else
            {
                checkInputDebugController();
            }

            checkAmmo();

            if (ammoText != null)
                ammoText.text = ammoCount + "-" + maxAmmo;

            if (grenadeText != null)
                grenadeText.text = grenadeCount + "-" + maxGrenadeCount;
        }
        else
        {
            checkMenu();
        }
    }

    void moveGun()
    {
        if(!isPlayer2)
            guitarTip.transform.position = new Vector3(guitarTip.transform.position.x, guitarTip.transform.position.y, player1.transform.position.z + 1.5f);
        else
            guitarTip.transform.position = new Vector3(guitarTip.transform.position.x, guitarTip.transform.position.y, player2.transform.position.z + 1.5f);

        timer = Mathf.Clamp(timer + Time.deltaTime, 0.0f, 1.0f / 5.0f);

        avatarLeftHand.transform.position = guitarTip.transform.position;

        Vector3 relativePos = guitarTip.transform.position - playerLeftArm.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        float angleZ = rotation.eulerAngles.z + (90 * -guitarTip.transform.position.y);

        playerLeftArm.transform.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, angleZ);

        if (!isPlayer2)
        {
            if (playerLeftArm.transform.eulerAngles.z >= 105)
                playerLeftArm.transform.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, 105);
        }
        else
        {
            if (playerLeftArm.transform.eulerAngles.z >= 105)
                playerLeftArm.transform.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, 105);
        }
    }

    void checkInvibilityFrames()
    {
        m_InvincibleFrames -= Time.deltaTime;

        if(m_InvincibleFrames <= 0.0f)
        {
            m_InvincibleFrames = m_MAXINCIBILITY;
            m_Invincible = false;
        }
    }

    void checkInputController()
    {
        if (gamePad.IsConnected)
        {
            if (!inMenu)
            {
                rightStickX = gamePad.GetStick_R().X;

                if (rightStickX >= 0.2f && canFireGrenade && grenadeCount > 0)
                {
                    //Debug.Log("Pressed Whammy RightX");
                    canFireGrenade = false;
                    grenadeCount--;

                    /*Vector3 m_direction = (new Vector3(Mathf.Atan(0.0f) * 180 / Mathf.PI, 0, Mathf.Atan(1.0f) * 180 / Mathf.PI).normalized);
                    GameObject flashBangObject = Instantiate(flashBang, (m_direction + new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z + 1.0f)), Quaternion.LookRotation(m_direction)) as GameObject;
                    flashBangObject.GetComponent<Rigidbody>().AddForce(m_direction * 10.0f, ForceMode.VelocityChange);

                    Vector3 force = (transform.up * 3.0f) / Time.deltaTime;
                    force *= 2.5f;
                    flashBangObject.GetComponent<Rigidbody>().AddForce(force);*/

                    GameObject deathWall = Instantiate(beatsFireBlast, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 4.0f), beatsFireBlast.transform.rotation) as GameObject;

                    Destroy(deathWall, deathWall.GetComponent<ParticleSystem>().duration);
                }
                else if (rightStickX < 0.2f && !canFireGrenade)
                {
                    canFireGrenade = true;
                }
            }
        }

        if (gamePad.GetButton("B"))//Red Button
        {
            holdingDownButtonRed = true;
            holdingDownButtonYellow = false;
            holdingDownButtonGreen = false;
            holdingDownButtonBlue = false;

            laserSightRed.SetActive(true);
            laserSightBlue.SetActive(false);
            laserSightGreen.SetActive(false);
            laserSightYellow.SetActive(false);
        }
        /*else if (!gamePad.GetButton("B"))
        {
            moveForward = false;
            reachEquilibrium = true;
        }*/

        if (gamePad.GetButton("A"))//Green Button
        {
            holdingDownButtonRed = false;
            holdingDownButtonYellow = false;
            holdingDownButtonGreen = true;
            holdingDownButtonBlue = false;

            laserSightRed.SetActive(false);
            laserSightBlue.SetActive(false);
            laserSightGreen.SetActive(true);
            laserSightYellow.SetActive(false);
        }

        if (gamePad.GetButton("Y"))//Yellow Button
        {
            holdingDownButtonRed = false;
            holdingDownButtonYellow = true;
            holdingDownButtonGreen = false;
            holdingDownButtonBlue = false;

            laserSightRed.SetActive(false);
            laserSightBlue.SetActive(false);
            laserSightGreen.SetActive(false);
            laserSightYellow.SetActive(true);
        }

        if (gamePad.GetButton("X"))//Blue button
        {
            holdingDownButtonRed = false;
            holdingDownButtonYellow = false;
            holdingDownButtonGreen = false;
            holdingDownButtonBlue = true;

            laserSightRed.SetActive(false);
            laserSightBlue.SetActive(true);
            laserSightGreen.SetActive(false);
            laserSightYellow.SetActive(false);
        }

        /*if (gamePad.GetButton("LB"))//Backwards, Orange Button
        {
            moveBackward = true;
        }
        else if (!gamePad.GetButton("LB"))
        {
            moveBackward = false;
            reachEquilibrium = true;
        }*/

        if (!inMenu)
        {
            if (gamePad.GetButton("LB") && canReload)//Orange Button
            {
                canReload = false;
                reload();
            }
            else if (!gamePad.GetButton("LB") && !canReload)
            {
                canReload = true;
            }
        }
        else
        {
            if (gamePad.GetButton("LB") && canBePressed)//Orange Button
            {
                canBePressed = false;
                switchGuitar();
            }
            else if (!gamePad.GetButton("LB") && !canBePressed)
            {
                canBePressed = true;
            }
        }

        if ((gamePad.GetButton("DPad_Up") || gamePad.GetButton("DPad_Down")) && fireOnce && canAttack) //The flipper is DPad_up and DPad_Down
        {
            fireOnce = false;

            if (!inMenu)
            {
                ammoCount--;
            }

            if (m16)
            {
                if (holdingDownButtonRed)
                {
                    GameObject bulletInstance = Instantiate(energyShotRed, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonBlue)
                {
                    GameObject bulletInstance = Instantiate(energyShotBlue, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonGreen)
                {
                    GameObject bulletInstance = Instantiate(energyShotGreen, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonYellow)
                {
                    GameObject bulletInstance = Instantiate(energyShotYellow, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
            }
            else if(shotgun)
            {
                if (holdingDownButtonRed)
                {
                    GameObject bulletInstance = Instantiate(aoeShotRed, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonBlue)
                {
                    GameObject bulletInstance = Instantiate(aoeShotBlue, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonGreen)
                {
                    GameObject bulletInstance = Instantiate(aoeShotGreen, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonYellow)
                {
                    GameObject bulletInstance = Instantiate(aoeShotYellow, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
            }

            //GameObject bulletInstance = Instantiate(bullet, new Vector3(guitarTip.transform.position.x, guitarTip.transform.position.y, guitarTip.transform.position.z - 0.1f), guitarTip.transform.rotation) as GameObject;
            //bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * 38, ForceMode.VelocityChange);

            if (m16)
                Invoke("timeDelayBetweenShots", 0.3f);
            else if (shotgun)
                Invoke("timeDelayBetweenShots", 0.6f);
        }

        if (gamePad.GetButton("DPad_Up") || gamePad.GetButton("DPad_Down"))
        {
            if (m16)
            {
                if (guitarAudio.activeInHierarchy && guitarAudio2.activeInHierarchy)
                {
                    if(this.tag == "Player")
                        guitarAudio.GetComponent<MusicScript>().changeVolume(1.0f);
                    else
                        guitarAudio2.GetComponent<MusicScript>().changeVolume(1.0f);
                }
            }
            else if (shotgun)
            {
                if (bassAudio.activeInHierarchy && bassAudio2.activeInHierarchy)
                {
                    if (this.tag == "Player")
                        bassAudio.GetComponent<MusicScript>().changeVolume(1.0f);
                    else
                        bassAudio2.GetComponent<MusicScript>().changeVolume(1.0f);
                }
            }
        }
        else
        {
            if (m16)
            {
                if (guitarAudio.activeInHierarchy && guitarAudio2.activeInHierarchy)
                {
                    if (this.tag == "Player")
                        guitarAudio.GetComponent<MusicScript>().changeVolume(0.0f);
                    else
                        guitarAudio2.GetComponent<MusicScript>().changeVolume(0.0f);
                }
            }
            else if (shotgun)
            {
                if (bassAudio.activeInHierarchy && bassAudio2.activeInHierarchy)
                {
                    if (this.tag == "Player")
                        bassAudio.GetComponent<MusicScript>().changeVolume(0.0f);
                    else
                        bassAudio2.GetComponent<MusicScript>().changeVolume(0.0f);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(-20);
            player2.GetComponent<Player>().TakeDamage(-20);
        }
    }

    void checkInputDebugController() //Actual 360 controller support
    {
        if (gamePad.IsConnected)
        {
            //leftStickX = gamePad.GetStick_L().X;
            //leftStickY = gamePad.GetStick_L().Y;
            rightStickX = gamePad.GetStick_R().X;
            rightStickY = gamePad.GetStick_R().Y;

            /*if (leftStickX >= 0.2f || leftStickY >= 0.2f)
            {
                Vector3 moveDirection = new Vector3(leftStickX, 0, leftStickY);
                transform.Translate(moveDirection * 5.0f * Time.deltaTime);
            }*/

            if (rightStickX != 0.0f)
            {
                playerLeftArm.transform.Rotate(rightStickX * 3.0f, 0, 0);
            }
            if (rightStickY != 0.0f)
            {
                playerLeftArm.transform.Rotate(0, 0, -rightStickY * 3.0f);
            }

            Quaternion q = playerLeftArm.transform.rotation;
            q.eulerAngles = new Vector3(0, q.eulerAngles.y, q.eulerAngles.z);
            playerLeftArm.transform.rotation = q;
        }

        if (!inMenu)
        {
            if (gamePad.GetButtonDown("RB") && canFireGrenade && grenadeCount > 0)
            {
                canFireGrenade = false;
                grenadeCount--;

                GameObject deathWall = Instantiate(beatsFireBlast, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 4.0f), beatsFireBlast.transform.rotation) as GameObject;
                Destroy(deathWall, deathWall.GetComponent<ParticleSystem>().duration);
            }
            else if (!gamePad.GetButtonDown("RB") && !canFireGrenade)
            {
                canFireGrenade = true;
            }
        }

        if (gamePad.GetButton("B"))//Red Button
        {
            holdingDownButtonRed = true;
            holdingDownButtonYellow = false;
            holdingDownButtonGreen = false;
            holdingDownButtonBlue = false;

            laserSightRed.SetActive(true);
            laserSightBlue.SetActive(false);
            laserSightGreen.SetActive(false);
            laserSightYellow.SetActive(false);
        }

        if (gamePad.GetButton("A"))//Green Button
        {
            holdingDownButtonRed = false;
            holdingDownButtonYellow = false;
            holdingDownButtonGreen = true;
            holdingDownButtonBlue = false;

            laserSightRed.SetActive(false);
            laserSightBlue.SetActive(false);
            laserSightGreen.SetActive(true);
            laserSightYellow.SetActive(false);
        }

        if (gamePad.GetButton("Y"))//Yellow Button
        {
            holdingDownButtonRed = false;
            holdingDownButtonYellow = true;
            holdingDownButtonGreen = false;
            holdingDownButtonBlue = false;

            laserSightRed.SetActive(false);
            laserSightBlue.SetActive(false);
            laserSightGreen.SetActive(false);
            laserSightYellow.SetActive(true);
        }

        if (gamePad.GetButton("X"))//Blue button
        {
            holdingDownButtonRed = false;
            holdingDownButtonYellow = false;
            holdingDownButtonGreen = false;
            holdingDownButtonBlue = true;

            laserSightRed.SetActive(false);
            laserSightBlue.SetActive(true);
            laserSightGreen.SetActive(false);
            laserSightYellow.SetActive(false);
        }

        if (!inMenu)
        {
            if (gamePad.GetButton("LB") && canReload)//Orange Button
            {
                canReload = false;
                reload();
            }
            else if (!gamePad.GetButton("LB") && !canReload)
            {
                canReload = true;
            }
        }
        else
        {
            if (gamePad.GetButton("LB") && canBePressed)//Orange Button
            {
                canBePressed = false;
                switchGuitar();
            }
            else if (!gamePad.GetButton("LB") && !canBePressed)
            {
                canBePressed = true;
            }
        }

        if ((gamePad.GetTriggerTap_L() || gamePad.GetTriggerTap_R()) && fireOnce && canAttack)
        {
            fireOnce = false;

            if(!inMenu)
            {
                ammoCount--;
            }

            if (m16)
            {
                if (holdingDownButtonRed)
                {
                    GameObject bulletInstance = Instantiate(energyShotRed, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonBlue)
                {
                    GameObject bulletInstance = Instantiate(energyShotBlue, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonGreen)
                {
                    GameObject bulletInstance = Instantiate(energyShotGreen, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonYellow)
                {
                    GameObject bulletInstance = Instantiate(energyShotYellow, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
            }
            else if (shotgun)
            {
                if (holdingDownButtonRed)
                {
                    GameObject bulletInstance = Instantiate(aoeShotRed, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonBlue)
                {
                    GameObject bulletInstance = Instantiate(aoeShotBlue, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonGreen)
                {
                    GameObject bulletInstance = Instantiate(aoeShotGreen, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
                else if (holdingDownButtonYellow)
                {
                    GameObject bulletInstance = Instantiate(aoeShotYellow, playerLeftArm.transform.position, playerLeftArm.transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * bulletSpeed, ForceMode.VelocityChange);
                }
            }

                if (m16)
                Invoke("timeDelayBetweenShots", 0.3f);
            else if (shotgun)
                Invoke("timeDelayBetweenShots", 0.6f);
        }

        if ((gamePad.GetTrigger_L >= 0.3f || gamePad.GetTrigger_R >= 0.3f))
        {
            if (m16)
            {
                if (guitarAudio.activeInHierarchy && guitarAudio2.activeInHierarchy)
                {
                    if (this.tag == "Player")
                        guitarAudio.GetComponent<MusicScript>().changeVolume(1.0f);
                    else
                        guitarAudio2.GetComponent<MusicScript>().changeVolume(1.0f);
                }
            }
            else if (shotgun)
            {
                if (bassAudio.activeInHierarchy && bassAudio2.activeInHierarchy)
                {
                    if (this.tag == "Player")
                        bassAudio.GetComponent<MusicScript>().changeVolume(1.0f);
                    else
                        bassAudio2.GetComponent<MusicScript>().changeVolume(1.0f);
                }
            }
        }
        else
        {
            if (m16)
            {
                if (guitarAudio.activeInHierarchy && guitarAudio2.activeInHierarchy)
                {
                    if (this.tag == "Player")
                        guitarAudio.GetComponent<MusicScript>().changeVolume(0.0f);
                    else
                        guitarAudio2.GetComponent<MusicScript>().changeVolume(0.0f);
                }
            }
            else if (shotgun)
            {
                if (bassAudio.activeInHierarchy && bassAudio2.activeInHierarchy)
                {
                    if (this.tag == "Player")
                        bassAudio.GetComponent<MusicScript>().changeVolume(0.0f);
                    else
                        bassAudio2.GetComponent<MusicScript>().changeVolume(0.0f);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(-20);
            player2.GetComponent<Player>().TakeDamage(-20);
        }
    }

    void timeDelayBetweenShots()
    {
        fireOnce = true;
    }

    void railSystem()
    {
        player1.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z + incrementingZ * moveSpeed * Time.deltaTime);
        player2.transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z + incrementingZ * moveSpeed * Time.deltaTime);

        if (incrementingZ < 0.1f)
            incrementingZ += 0.1f;        
    }

    void reload()
    {
        canAttack = true;
        ammoCount = maxAmmo;
    }

    void checkAmmo()
    {
        if(ammoCount <= 0)
        {
            canAttack = false;
        }
    }

    void checkMenu()
    {
        if (kinectMode)
        {
            moveGun();
            checkInputController();
        }
        else
        {
            checkInputDebugController();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Continue = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            kinectMode = true;
            player2.GetComponent<Player>().kinectMode = true;
            Debug.Log("kinect mode");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            kinectMode = false;
            player2.GetComponent<Player>().kinectMode = false;
            Debug.Log("controller mode");
        }
    }

    void switchGuitar()
    {
        if (inMenu)
        {
            if(!isPlayer2)
            {
                if(m16)
                {
                    singleShotGuitarP1.SetActive(false);
                    AOEGuitarP1.SetActive(true);
                    singleShotGuitarP1Model.SetActive(false);
                    AOEGuitarP1Model.SetActive(true);

                    m16 = false;
                    shotgun = true;
                }
                else if(shotgun)
                {
                    singleShotGuitarP1.SetActive(true);
                    AOEGuitarP1.SetActive(false);
                    singleShotGuitarP1Model.SetActive(true);
                    AOEGuitarP1Model.SetActive(false);

                    m16 = true;
                    shotgun = false;
                }
            }
            else
            {
                if (m16)
                {
                    singleShotGuitarP2.SetActive(false);
                    AOEGuitarP2.SetActive(true);
                    singleShotGuitarP2Model.SetActive(false);
                    AOEGuitarP2Model.SetActive(true);

                    m16 = false;
                    shotgun = true;
                }
                else if (shotgun)
                {
                    singleShotGuitarP2.SetActive(true);
                    AOEGuitarP2.SetActive(false);
                    singleShotGuitarP2Model.SetActive(true);
                    AOEGuitarP2Model.SetActive(false);

                    m16 = true;
                    shotgun = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy" && !m_Invincible)
        {
            TakeDamage(1);
        }

        if (collider.tag == "EnemyBullet" && !m_Invincible)
        {
            TakeDamage(1);
        }

        if(collider.tag == "Goal")
        {
            youWinBool = true;
        }
    }

    public void TakeDamage(float amount)
    {
        m_health -= amount;
        healthSlider.value = m_health;

        if(amount >= 1)
            m_Invincible = true;

        if(m_health >= MAXHEALTH)
        {
            m_health = MAXHEALTH;
            healthSlider.value = m_health;
        }

        if (m_health <= 0)
        {
            youLoseBool = true;
        }
    }

    void checkWinLoss()
    {
        if (youLoseBool && !youWinBool)
        {
            lossText.enabled = true;
            timeToReset -= Time.deltaTime;

            if (timeToReset <= 0.0f)
            {
                timeToReset = 20.0f;
                youLoseBool = false;
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }

        if (youWinBool && !youLoseBool)
        {
            winText.enabled = true;
            timeToReset -= Time.deltaTime;

            if (timeToReset <= 0.0f)
            {
                timeToReset = 20.0f;
                youWinBool = false;
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
    }

    public void setDefaultVolume()
    {
        if(this.tag == "Player")
        {
            guitarAudio.GetComponent<MusicScript>().changeVolume(0.0f);
            bassAudio.GetComponent<MusicScript>().changeVolume(0.0f);
        }
        else
        {
            guitarAudio2.GetComponent<MusicScript>().changeVolume(0.0f);
            bassAudio2.GetComponent<MusicScript>().changeVolume(0.0f);
        }

    }

    public void rumble()
    {
        GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
        isRumble = true;
    }

    public void vibrateController()
    {
        rumbleCounter += Time.deltaTime;
        if (rumbleCounter >= MAX_RUMBLE)
            stopRumble();
    }

    public void stopRumble()
    {
        GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
        isRumble = false;
        rumbleCounter = 0.0f;
    }
}
