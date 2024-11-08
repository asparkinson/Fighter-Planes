using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    // Access: public or private
    // Type: int, float
    // Name: speed, playerSpeed
    // Optional: give initial value
    private float speed;
    private float horizontalInput;
    private float verticalInput;
    private float horizontalScreenSize = 11.5f;
    private float verticalScreenSize = 7.5f;
    public int lives;

    public GameObject bullet;
    public GameObject enemyOne;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        lives = 3;
        
        GameObject.Find("Game Manager").GetComponent<GameManager>().livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            // Player is outside the screen
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y > verticalScreenSize || transform.position.y <= -verticalScreenSize)
        {
            // Player is outside the screen
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        // On pressed SPACE create bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        lives --;
        GameObject.Find("Game Manager").GetComponent<GameManager>().livesText.text = "Lives: " + lives;

        if (lives == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
