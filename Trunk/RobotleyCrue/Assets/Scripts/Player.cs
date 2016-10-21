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

    GlobalMusicScript music;

    float timer;
    bool canAttack;

    //Movement
    bool canMove;
    public bool reachEquilibrium;
    float moveSpeed = 5.0f;
    float incrementingX;
    float incrementingZ;
    bool moveLeft;
    bool moveRight;
    bool moveForward;
    bool moveBackward;

    float leftStickX;
    float leftStickY;
    float rightStickX;
    float rightStickY;

    public bool isPlayer2;
    public GameObject player1;
    public GameObject player2;
    bool disableForwards = true;

    //Rumble
    private float rumbleCounter = 0.0f;
    private const float MAX_RUMBLE = 0.25f;
    private bool isRumble;
    x360_Gamepad gamePad;

    //Public Objects
    public GameObject bullet;
    public GameObject gun;
    public GameObject beatsFireBlast;

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
    

    //Grenade
    public GameObject flashBang;
    bool canFireGrenade;
    public Text grenadeText;
    public int grenadeCount;
    public int maxGrenadeCount;

    //End Game Bools
    public bool youLoseBool = false;
    public bool youWinBool = false;
    private float timeToReset = 10.0f;
    public Text lossText;
    public Text winText;

    //Menu
    public bool inMenu;
    public bool m16;
    public bool shotgun;
    public bool Continue;

    bool initialDisableBool = true;

    public GameObject theCylinder;

    void Start ()
    {
        m_health = healthSlider.value = 20;
        beatsPowerSlider.value = 0;

        m_InvincibleFrames = m_MAXINCIBILITY;
        m_Invincible = false;

        if(!isPlayer2)
            gamePad = GamepadManager.Instance.GetGamepad(1);
        else
            gamePad = GamepadManager.Instance.GetGamepad(2);

        canMove = true;
        canAttack = true;
        
        moveLeft = false;
        moveRight = false;
        moveForward = false;
        moveBackward = false;
        reachEquilibrium = false;
        incrementingX = 0.0f;
        incrementingZ = 0.0f;
        initialDisableBool = true;

        if (!isPlayer2)
            music = GameObject.Find("GlobalSF").GetComponent<GlobalMusicScript>();

        ammoCount = maxAmmo;
        grenadeCount = maxGrenadeCount;
        fireOnce = true;
        canReload = true;
        canFireGrenade = true;

        if (ammoText != null)
            ammoText.text = ammoCount + "/" + maxAmmo;

        if (grenadeText != null)
            grenadeText.text = grenadeCount + "/" + maxGrenadeCount;

        inMenu = true;
        shotgun = false;
        m16 = false;
        Continue = false;
    }

    void Update ()
    {
        if (!inMenu)
        {
            if(initialDisableBool)
                initialDisable();

            checkWinLoss();

            if (canMove)
                checkInputController();

            if (isRumble)
                vibrateController();

            if (m_Invincible)
                checkInvibilityFrames();
            
            movePlayer();

            moveGun();
            checkAmmo();

            if (ammoText != null)
                ammoText.text = ammoCount + "/" + maxAmmo;

            if (grenadeText != null)
                grenadeText.text = grenadeCount + "/" + maxGrenadeCount;
        }
        else
        {
            checkMenu();
        }
    }

    void initialDisable()
    {
        if(initialDisableBool)
        {
            initialDisableBool = false;

            moveLeft = false;
            moveRight = false;
            moveForward = false;
            moveBackward = false;
            reachEquilibrium = true;
            incrementingX = 0.0f;
            incrementingZ = 0.0f;
        }
    }

    void moveGun()
    {
        timer = Mathf.Clamp(timer + Time.deltaTime, 0.0f, 1.0f / 5.0f);

        avatarLeftHand.transform.position = guitarTip.transform.position;

        Vector3 relativePos = guitarTip.transform.position - playerLeftArm.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        float angleZ = rotation.eulerAngles.z + (90 * -guitarTip.transform.position.y);

        playerLeftArm.transform.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, angleZ);

        if (!isPlayer2)
        {
            if (playerLeftArm.transform.eulerAngles.z <= 65)
                playerLeftArm.transform.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, 65);
        }
        else
        {
            if (playerLeftArm.transform.eulerAngles.z <= 65)
                playerLeftArm.transform.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, 65);
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
            rightStickX = gamePad.GetStick_R().X;

            if (rightStickX >= 0.2f && canFireGrenade && grenadeCount > 0)
            {
                //Debug.Log("Pressed Whammy RightX");
                canFireGrenade = false;
                grenadeCount--;

                Vector3 m_direction = (new Vector3(Mathf.Atan(0.0f) * 180 / Mathf.PI, 0, Mathf.Atan(1.0f) * 180 / Mathf.PI).normalized);
                GameObject flashBangObject = Instantiate(flashBang, (m_direction + new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z + 1.0f)), Quaternion.LookRotation(m_direction)) as GameObject;
                flashBangObject.GetComponent<Rigidbody>().AddForce(m_direction * 10.0f, ForceMode.VelocityChange);

                Vector3 force = (transform.up * 3.0f) / Time.deltaTime;
                force *= 2.5f;
                flashBangObject.GetComponent<Rigidbody>().AddForce(force);

                Destroy(flashBangObject, flashBangObject.GetComponent<ParticleSystem>().duration);
            }
            else if (rightStickX < 0.2f && !canFireGrenade)
            {
                canFireGrenade = true;
            }
        }

        if (gamePad.GetButton("B") && !disableForwards)//Forwards, Red Button
        {
            moveForward = true;
            //Vector3 moveDirection = new Vector3(0, 0, 1);
            //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
        else if (!gamePad.GetButton("B"))
        {
            moveForward = false;
            reachEquilibrium = true;
        }

        if (gamePad.GetButton("Y"))//Left, Yellow Button
        {
            moveLeft = true;
        }
        else if (!gamePad.GetButton("Y"))
        {
            moveLeft = false;
            reachEquilibrium = true;
        }

        if (gamePad.GetButton("X"))//Right, Blue button
        {
            moveRight = true;
        }
        else if (!gamePad.GetButton("X"))
        {
            moveRight = false;
            reachEquilibrium = true;
        }

        if (gamePad.GetButton("LB"))//Backwards, Orange Button
        {
            moveBackward = true;
        }
        else if (!gamePad.GetButton("LB"))
        {
            moveBackward = false;
            reachEquilibrium = true;
        }

        if (gamePad.GetButton("A") && canReload)
        {
            canReload = false;
            reload();
        }
        else if(!gamePad.GetButton("A") && !canReload)
        {
            canReload = true;
        }

        if (gamePad.GetButton("Start"))
        {
            if (disableForwards)
            {
                disableForwards = false;
                Debug.Log("false");
            }
            else if (!disableForwards)
            {
                disableForwards = true;
                Debug.Log("true");
            }
        }

        if ((gamePad.GetButton("DPad_Up") || gamePad.GetButton("DPad_Down")) && fireOnce && canAttack) //The flipper is DPad_up and DPad_Down
        {
            fireOnce = false;
            ammoCount--;
            GameObject bulletInstance = Instantiate(bullet, new Vector3(playerLeftArm.transform.position.x - 0.1f, playerLeftArm.transform.position.y - 0.1f, playerLeftArm.transform.position.z - 0.1f), playerLeftArm.transform.rotation) as GameObject;
            bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * 38, ForceMode.VelocityChange);

            //GameObject bulletInstance = Instantiate(bullet, new Vector3(guitarTip.transform.position.x, guitarTip.transform.position.y, guitarTip.transform.position.z - 0.1f), guitarTip.transform.rotation) as GameObject;
            //bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * 38, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    void movePlayer()
    {
        player1.transform.position += new Vector3(incrementingX, 0.0f, incrementingZ) * moveSpeed * Time.deltaTime;
        player2.transform.position += new Vector3(incrementingX, 0.0f, incrementingZ) * moveSpeed * Time.deltaTime;

        if (moveRight)
        {
            if (incrementingX < 1.0f)
                incrementingX += 0.1f;
        }

        if (moveLeft)
        {
            if (incrementingX > -1.0f)
                incrementingX -= 0.1f;
        }

        if (moveForward && !disableForwards)
        {
            if (incrementingZ < 1.0f)
                incrementingZ += 0.1f;
        }

        if (moveBackward)
        {
            if (incrementingZ > -1.0f)
                incrementingZ -= 0.1f;
        }

        if (reachEquilibrium)
        {
            if (incrementingX < 0.01f)
                incrementingX += 0.01f;

            if (incrementingX > 0.0f)
                incrementingX -= 0.01f;

            if (incrementingZ < 0.01f)
                incrementingZ += 0.01f;

            if (incrementingZ > 0.0f)
                incrementingZ -= 0.01f;

            if (incrementingX == 0.0f || incrementingZ == 0.0f)
                reachEquilibrium = false;
        }
    }

    void reload()
    {
        canAttack = true;
        ammoCount = maxAmmo;
    }

    void checkAmmo()
    {
        if ((!gamePad.GetButton("DPad_Up") && !gamePad.GetButton("DPad_Down")) && !fireOnce)
        {
            fireOnce = true;
        }

        if(ammoCount <= 0)
        {
            canAttack = false;
        }
    }

    void checkMenu()
    {
        moveGun();

        moveLeft = false;
        moveRight = false;
        moveForward = false;
        moveBackward = false;
        reachEquilibrium = true;
        incrementingX = 0.0f;
        incrementingZ = 0.0f;

        if ((gamePad.GetButton("DPad_Up") || gamePad.GetButton("DPad_Down")) && fireOnce) //The flipper is DPad_up and DPad_Down
        {
            fireOnce = false;
            GameObject bulletInstance = Instantiate(bullet, new Vector3(playerLeftArm.transform.position.x - 0.1f, playerLeftArm.transform.position.y - 0.1f, playerLeftArm.transform.position.z - 0.1f), playerLeftArm.transform.rotation) as GameObject;
            bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * 38, ForceMode.VelocityChange);

            //GameObject bulletInstance = Instantiate(bullet, new Vector3(guitarTip.transform.position.x, guitarTip.transform.position.y, guitarTip.transform.position.z - 0.1f), guitarTip.transform.rotation) as GameObject;
            //bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * 38, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Continue = true;
        }

        checkAmmo();
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

        m_Invincible = true;

        if (m_health <= 0)
        {
            youLoseBool = true;
        }
    }

    void checkWinLoss()
    {
        if (youLoseBool)
        {
            lossText.enabled = true;
            timeToReset -= Time.deltaTime;

            if (timeToReset <= 0.0f)
            {
                timeToReset = 10.0f;
                youLoseBool = false;
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }

        if (youWinBool)
        {
            winText.enabled = true;
            timeToReset -= Time.deltaTime;

            if (timeToReset <= 0.0f)
            {
                timeToReset = 10.0f;
                youWinBool = false;
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
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
