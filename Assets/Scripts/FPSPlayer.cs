using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSPlayer : MonoBehaviour {

    public float movementSpeed;
    public float jumpSpeed;

    private float mouseSensitivity = 5.0f;
    private float verticalAngelLimit = 60.0f;
    private float verticalRotation = 0.0f;

    public GameObject gameCam;

    [HideInInspector]
    CharacterController cc;

    //health system
    public int health;
    //public Image[] hearts;
    //public Sprite emptyHeart;
    //public Sprite fullHeart;


    public Text headCount;
    public Text armCount;
    public Text torsoCount;
    public Text wheelCount;

    public int headNum;
    public int armNum;
    public int torsoNum;
    public int wheelNum;

    protected float verticalVelocity = 0.0f;

     void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cc = this.GetComponent<CharacterController>();
        //UpdateHealthUI(health);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            //Application.LoadLevel("main_menu");
        }

        float rotateLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotateLeftRight, 0); //yaw


        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalAngelLimit, verticalAngelLimit);

        gameCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float lateralSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;   // accelrateds by 9.81 every second

        Vector3 speed = new Vector3(lateralSpeed, verticalVelocity, forwardSpeed);  //actual movement 

        if ((Input.GetButtonDown("Jump") && cc.isGrounded))
        {   // Jump Code - allows for double jumping
            verticalVelocity = jumpSpeed;
        }

        speed = transform.rotation * speed;

        cc.Move(speed * Time.deltaTime);

        
    }


    public void TakeDamage(int damageAmmount)
    {
        health -= damageAmmount;
        //UpdateHealthUI(health);
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

    public void UpdateScoreUI()
    {
        headCount.text = "Heads: " + headNum.ToString();
        armCount.text = "Arms: " + armNum.ToString();
        torsoCount.text = "Torso: " +  torsoNum.ToString();
        wheelCount.text = "Wheel: " + wheelNum.ToString();
    }


    //private void UpdateHealthUI(int currentHealth)
    //{
    //    for (int i = 0; i < hearts.Length; i++)
    //    {
    //        if(i < currentHealth)
    //        {
    //            hearts[i].sprite = fullHeart;
    //        }
    //        else
    //        {
    //            hearts[i].sprite = emptyHeart;
    //        }
    //    }
    //}

}
