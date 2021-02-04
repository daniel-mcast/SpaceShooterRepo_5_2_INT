using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false)
        {
            //It will set a new random position on the x and set the position on the y to 7.5f (Enemy Position)
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f,9.2f),7.5f,0);
            //Spawn an enemy on a random positon and store it in newEnemy
            GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity);
            //Set the newEnemy as a child to the EnemyContainer
            newEnemy.transform.SetParent(_enemyContainer.transform);
            //Wait for 5 seconds before we spawn the next enemy
            yield return new WaitForSeconds(5f);
        }
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f,9.2f),7.5f,0);
            int randomPowerup = Random.Range(0,_powerups.Length);
            Instantiate(_powerups[randomPowerup],posToSpawn,Quaternion.identity);
            //wait between 6 to 9 seconds.
            yield return new WaitForSeconds(Random.Range(6,10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
