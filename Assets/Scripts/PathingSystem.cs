using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingSystem : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] public GameObject[] pathingPoints;
    [SerializeField] private int enemiesToSpawn;
    [SerializeField] private float spawnTimer;

    private bool isAllowedToSpawn = false;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAllowedToSpawn && !(count == enemiesToSpawn))
        {
            StartCoroutine(Spawn());
        }
        else
        {
            isAllowedToSpawn = false;
        }
    }


    private IEnumerator Spawn()
    {
        GameObject enemySpawned = Instantiate(enemy, pathingPoints[0].transform.position, enemy.transform.rotation);
        enemySpawned.transform.parent = this.transform;
        count++;

        isAllowedToSpawn = false;
        yield return new WaitForSeconds(spawnTimer);
        isAllowedToSpawn = true;
    }

    public void SetIsAllowedToSpawn(bool t)
    {
        isAllowedToSpawn = t;
    }
}
