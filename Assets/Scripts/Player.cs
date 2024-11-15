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
    private int shooting;
    private bool hasShield;

    public GameManager gameManager;

    public GameObject bullet;
    public GameObject enemyOne;
    public GameObject explosion;
    public GameObject thruster;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        lives = 3;
        shooting = 1;
        hasShield = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        gameManager.livesText.text = "Lives: " + lives;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (shooting)
            {
                case 1:
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 30f));
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -30f));
                    break;
            }
        }
        
    }

    public void LoseALife()
    {
        if(hasShield == false)
        {
            lives--;
        }

        gameManager.livesText.text = "Lives: " + lives;

        if (lives == 0)
        {
            gameManager.GameOver();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 6f;
        thruster.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShootingPowerDown()
    {
        yield return new WaitForSeconds(4f);
        shooting = 1;
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5f);
        hasShield = false;
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Powerup")
        {
            gameManager.PlayPowerUp();
            int powerupType = Random.Range(1, 5);
            switch (powerupType)
            {
                case 1:
                    //Speed powerup
                    speed = 9f;
                    gameManager.UpdatePowerupText("Picked up Speed");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //double shot
                    gameManager.UpdatePowerupText("Picked up Double Shot");
                    shooting = 2;
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 3:
                    //triple shot
                    gameManager.UpdatePowerupText("Picked up Triple Shot");
                    shooting = 3;
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 4:
                    //shield
                    gameManager.UpdatePowerupText("Picked up Shield");
                    hasShield = true;
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
            Destroy(whatIHit.gameObject);
        }
    }
}
