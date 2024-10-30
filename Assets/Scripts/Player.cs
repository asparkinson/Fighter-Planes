using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Access: public or private
    // Type: int, float
    // Name: speed, playerSpeed
    // Optional: give initial value
    private float speed = 5.0f;
    public int lives = 3;
    private float horizontalInput;
    private float verticalInput;

    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
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

        if (transform.position.x > 11.5f || transform.position.x <= -11.5f)
        {
            // Player is outside the screen
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y >= 0f)
        {
            // Player is touching the screen edges
            Vector3 newPosition = new Vector3(transform.position.x, 0f, 0);
            transform.position = newPosition;
        }

        if (transform.position.y <= -4f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, -4f, 0);
            transform.position = newPosition;
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
}
