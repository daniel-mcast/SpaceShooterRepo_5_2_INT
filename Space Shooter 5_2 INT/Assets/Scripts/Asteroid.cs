using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the asteroid at 3m/s on the z axis
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab,transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            if(_spawnManager != null)
            {
                _spawnManager.StartSpawning();
            }
            Destroy(this.gameObject,0.25f);
        }
    }
}
