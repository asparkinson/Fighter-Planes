using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyOne;
    public GameObject cloud;
    public GameObject coin;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
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
}
