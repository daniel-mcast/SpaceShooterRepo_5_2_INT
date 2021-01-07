using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    // for private variables naming convention should be as follows: _name
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    //Total health
    private int _lives = 3;
    private SpawnManager _spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //current player position = new position(0,0,0)
        transform.position = new Vector3(0,0,0);
        //stored a copy of the spawnmanager script inside our _spawnManager
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        //if the _spawnManager is empty
        if(_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        CalculateMovement();

        //Checking if space is being pressed && Time.time > _canFire = Time.time + _fireRate
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        
    }

    void CalculateMovement()
    {
        //transform.Translate(new Vector3(1,0,0)) 1 * 5 * Time.deltatime 5m/s
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Vector3.right == Vector3(1,0,0)
        //Vector3.left == Vector3(-1,0,0)
        //Vector3(1,0,0) * 1 * 3.5 * realtime (The player will be moving to right while pressing the D key)
        //Vector3(1,0,0) * -1 * 3.5 * realtime (The player will be moving to left while pressing the A key)
        //Vector3(1,0,0) * 0 * 3.5 * realtime (The player is static as their horizontalInput has a value of zero)
        //option 1 
        // transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime); 
        //option 2
        //transform.Translate(new Vector3(horizontalInput,verticalInput,0) * speed * Time.deltaTime);
        //option 3
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //if the player position on the y axis is greater than 0
        // if(transform.position.y >= 0)
        // {
        //     transform.position = new Vector3(transform.position.x,0,0);
        // }
        //if the player position on the y axis is <= -3.8f
        // else if(transform.position.y <= -3.8f)
        // {
        //     transform.position = new Vector3(transform.position.x,-3.8f,0);
        // }
        //Mathf.Clamp(the value we wish to restrict,minimun value in can reach,maximum value it can reach)
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);

        //if the player position on the x axis is greater than 11
        //x position = -11.3
        if(transform.position.x > 11.3f)
        {
            //access the player position = Vector3(the position in where we want to teleport,current y position,0)
            transform.position = new Vector3(-11.3f,transform.position.y,0);
        }
        //if the player position on the x axis is smaller than -11.3f
        //x position = 11.3
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f,transform.position.y,0);
        }
    }

    void FireLaser()
    {
        //Debug.Log("Space was pressed");
        //every 0.5 of a second it will allow us to fire
        _canFire = Time.time + _fireRate;
        //Position of player (0,0,0) + (0,1,0) = final position of the laser (0,1,0)
        Instantiate(_laser,transform.position + new Vector3(0,1f,0),Quaternion.identity);
        
        //Time.time(Timer thats starts as soon you play) 0
        //_canFire -1f 
        //Time.time 0.5f
        //_canFire 0.5f + 0,5f
        //Time.time(0.5f) > _canFire 1f;
        //Time.time(1f)
        //_canFire = Time.time(1f) + _fireRate(0.5f)
        //Time.time(1f) > _canFire(1.5f)
    }

    public void Damage()
    {
        _lives-= 1;
        Debug.Log(_lives); 

        //if there are no lives kill the player
        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
