using System.Collections;
using System.Collections.Generic;
using UnityEditor; 
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject _spawnedEnemy;
    public GameObject Player;

    [HideInInspector]
    public float _playerXPosition;

    [Header("Stats")]
     public float maxNumberOfEnemies = 10;
     public float maxTotalNumberOfEnemies = 10;
    
    // [HideInInspector]
    public float numberOfEnemies = 0;
    public float totalNumberOfEnemies = 0;
    public bool _spawnEnemies = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _playerXPosition = Player.transform.position.x;


        if (_spawnEnemies)
        {
            SpawnEnemy();
        }
    }
    public void SpawnEnemy()
    {
        if (numberOfEnemies < maxNumberOfEnemies)
        {
            if (totalNumberOfEnemies < maxTotalNumberOfEnemies)
            {
                int num = Random.Range(1, 3);
                float spawnX;

                if (num == 1)
                    spawnX = Random.Range(-4.89f, Player.transform.position.x - 1f); // left side
                else
                    spawnX = Random.Range(Player.transform.position.x + 1f, 3.45f);   // right side

                Vector3 spawnPosition = new Vector3(spawnX, 0f, 0f);
                _spawnedEnemy = Instantiate(Enemy, spawnPosition, Quaternion.identity);
                _spawnedEnemy.active = true;

                numberOfEnemies += 1;
                totalNumberOfEnemies += 1;
            }
        }
    }
}
