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

    Vector3 moveDirection;
    float m_moveSpeed = 6.0f;
    float m_rotationalSpeed = 8.0f;
    private float yLookSensitivity = 0.03f;
    private bool autoLookCenter = true;
    bool canMove;
    public bool swordSwing;
    float timer;
    bool canAttack;

    public GameObject swordAndArm;
    private Vector3 startingArmRot = new Vector3(0.0f, 90.0f, 60.0f);
    private Vector3 endingArmRot = new Vector3(0.0f, 90.0f, 130.0f);
    private bool startingAttack;
    Vector3 currentAngle;

    float leftStickX;
    float leftStickY;
    float rightStickX;
    float rightStickY;

    private float rumbleCounter = 0.0f;
    private const float MAX_RUMBLE = 0.25f;
    private bool isRumble;

    x360_Gamepad gamePad;

    GlobalMusicScript music;

    public GameObject bullet;
    public GameObject gun;
    public GameObject beatsFireBlast;

    //UI Stuff
    public Slider healthSlider;
    public Slider beatsPowerSlider;
    public GameObject beatObject;
    private bool m_BeatMovingLeft;
    public bool m_OnBeat;

    //Invincible frames
    private bool m_Invincible;
    private float m_InvincibleFrames;
    private const float m_MAXINCIBILITY = 0.3f;

    public GameObject guitarTip;
    public GameObject avatarLeftHand;
    public GameObject playerLeftArm;

    void Start ()
    {
        m_health = healthSlider.value = 20;
        beatsPowerSlider.value = 0;
        beatObject.transform.localPosition = Vector3.zero;

        m_InvincibleFrames = m_MAXINCIBILITY;
        m_Invincible = false;

        gamePad = GamepadManager.Instance.GetGamepad(1);
        moveDirection = (new Vector3(Mathf.Atan(1.0f) * 180 / Mathf.PI, 0, Mathf.Atan(1.0f) * 180 / Mathf.PI).normalized);
        canMove = true;
        swordSwing = false;
        canAttack = true;

        m_OnBeat = true;
        m_BeatMovingLeft = false;

        startingAttack = true;
        currentAngle = startingArmRot;

        music = GameObject.Find("GlobalSF").GetComponent<GlobalMusicScript>();
    }

    void Update ()
    {
        if (canMove)
            checkInputController();

        if (isRumble)
            vibrateController();

        if (m_Invincible)
            checkInvibilityFrames();

        timer = Mathf.Clamp(timer + Time.deltaTime, 0.0f, 1.0f / 5.0f);

        if (swordSwing)
        {
            Invoke("ResetArm", 0.4f);
        }

        avatarLeftHand.transform.position = guitarTip.transform.position;

        Vector3 relativePos = guitarTip.transform.position - playerLeftArm.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        float angleZ = rotation.eulerAngles.z + (90 * -guitarTip.transform.position.y);

        playerLeftArm.transform.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 100, angleZ);
    }

    void FixedUpdate()
    {
        Vector3 newLoc = beatObject.transform.localPosition;

        if(m_BeatMovingLeft)
        {
            newLoc.x += 7.4534f;
        }
        else 
        {
            newLoc.x -= 7.4534f;
        }
			
        beatObject.transform.localPosition = newLoc;

        if (newLoc.x > -43 && newLoc.x < 43)
            m_OnBeat = true; 
        else
            m_OnBeat = false;

        if (newLoc.x < -90 || newLoc.x > 90)
            m_BeatMovingLeft = !m_BeatMovingLeft;
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
        try
        {
            if (gamePad.IsConnected)
            {
                rightStickX = gamePad.GetStick_R().X;
                rightStickY = gamePad.GetStick_R().Y;
                leftStickX = gamePad.GetStick_L().X;
                leftStickY = gamePad.GetStick_L().Y;

                if (leftStickX != 0.0f || leftStickY != 0.0f)
                {
                    Vector3 moveDirection = new Vector3(leftStickX, 0, leftStickY);
                    transform.Translate(moveDirection * m_moveSpeed * Time.deltaTime);

                    music.playWalkSound();
                }

                if (rightStickX != 0.0f)
                {
                    transform.Rotate(0, rightStickX * m_rotationalSpeed, 0);
                }
                if(rightStickY != 0.0f)
                {
                    transform.Rotate(-rightStickY * (m_rotationalSpeed/2), 0, 0);
                }

                Quaternion q = transform.rotation;
                q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
                transform.rotation = q;
            }
        }
        catch
        {
            Debug.Log("Caught a crash? (gamepad probably)");
        }
        
        if(gamePad.GetTriggerTap_L() && !swordSwing)
        {
            swordSwing = true;

            if (startingAttack)
            {
                currentAngle = startingArmRot;
                startingAttack = false;
                music.playSwordSound();
            }

            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, endingArmRot.x, timer * 5.5f),
            Mathf.LerpAngle(currentAngle.y, endingArmRot.y, timer * 5.5f),
            Mathf.LerpAngle(currentAngle.z, endingArmRot.z, timer * 5.5f));

            swordAndArm.transform.localEulerAngles = currentAngle;      

        }

        if(gamePad.GetTriggerTap_R() && canAttack)
        {
            //Vector3 mDirection = (new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.y).normalized);
            GameObject bulletInstance = Instantiate(bullet, new Vector3(playerLeftArm.transform.position.x, playerLeftArm.transform.position.y, playerLeftArm.transform.position.z), playerLeftArm.transform.rotation) as GameObject;
            bulletInstance.GetComponent<Rigidbody>().AddForce(playerLeftArm.transform.up * 28, ForceMode.VelocityChange);

            /*Rigidbody bulletClone;
            bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as Rigidbody;
            bulletClone.AddForce(bullet.transform.forward * 28.0f);*/

            music.playGunSound();
            canAttack = false;
            Invoke("AttackEnable", 0.1f);
        }

        if (gamePad.GetButton("RB") && beatsPowerSlider.value >= 10) //values is from 0-10
        {
            //abilityCanBeUsed = false;
            beatsPowerSlider.value = 0;

            GameObject particleEffect = (GameObject)Instantiate(beatsFireBlast, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.Euler(beatsFireBlast.transform.eulerAngles)) as GameObject;
            Destroy(particleEffect, particleEffect.GetComponent<ParticleSystem>().duration);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    void ResetArm()
    {
        swordAndArm.transform.localEulerAngles = startingArmRot;
        swordSwing = false;
        startingAttack = true;
    }

    void AttackEnable()
    {
        canAttack = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy" && !m_Invincible)
        {
            if(!swordSwing)
            {
                TakeDamage(1);
            }
        }

        else if (collider.tag == "EnemyBullet" && !m_Invincible)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float amount)
    {
        m_health -= amount;
        healthSlider.value = m_health;

        m_Invincible = true;

        if (m_health <= 0)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
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
