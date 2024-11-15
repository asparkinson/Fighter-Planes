using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyOne;
    public GameObject cloud;
    public GameObject coin;
    public GameObject powerUp;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public AudioClip coinSound;

    public int cloudSpeed;

    private bool isPlayerAlive;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI RestartText;
    public TextMeshProUGUI PowerupText;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1f, 3f);
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        InvokeRepeating("CreateCoin", 1f, 20f);
        StartCoroutine(CreatePowerUp());
        isPlayerAlive = true;
        cloudSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    IEnumerator CreatePowerUp()
    {
        Instantiate(powerUp, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        StartCoroutine(CreatePowerUp());
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++) 
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }

    void CreateCoin()
    {
        Instantiate(coin, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
    }

    public void GameOver()
    {
        isPlayerAlive = false;
        CancelInvoke();
        GameOverText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void UpdatePowerupText(string whichPowerup)
    {
        PowerupText.text = whichPowerup;
    }

    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position);
    }

    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDownSound, Camera.main.transform.position);
    }

    public void PlayCoinSound()
    {
        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
    }
}
