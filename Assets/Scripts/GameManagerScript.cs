using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject zombiesSpawnPoints;
    public GameObject zombiePrefab;

    void Start()
    {
        InvokeRepeating("SpawnZombie", 0f, Random.Range(4f, 8f));
    }

    void Update()
    {
        
    }

    private void SpawnZombie()
    {
        int randomSpawn = Random.Range(0, zombiesSpawnPoints.transform.childCount);
        GameObject zombie = Instantiate(zombiePrefab, zombiesSpawnPoints.transform.GetChild(randomSpawn).transform.position, Quaternion.Euler(0, 0, 0));
        zombie.SetActive(true);
    }
}
