using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //translate the enemy down
        //it needs to move at a speed of 4m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6f)
        {
            //teleport enemy back to the top
            //7.5f on y axis.
            float randomX = Random.Range(-9.2f,9.2f);
            transform.position = new Vector3(randomX,7.5f,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        //Debug.Log("Hit: " + other.transform.name);
        if(other.tag == "Laser")
        {
            //Destroy the laser
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            //Destory the enemy
            Destroy(this.gameObject);    
        }
        if(other.tag == "Player")
        {
            //Damage the player
            Player player = other.GetComponent<Player>();
            //if the player is not empty
            if(player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}
