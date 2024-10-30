using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject enemy2;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, new Vector3(0, -3.5f, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateEnemy2", 1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-9f, 9f), 9f, 0), Quaternion.identity);
    }

    void CreateEnemy2()
    {
        Instantiate(enemy2, new Vector3(Random.Range(-9f, 9f), 9f, 0), Quaternion.identity);
    }
}
