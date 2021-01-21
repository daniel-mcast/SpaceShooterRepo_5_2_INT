using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    //ID for powerups
    //0 = Triple Shot
    //1 = Speed
    //2 = Shield
    [SerializeField]
    private int _powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        //if the powerup position on the y axis is smaller than -6f destroy the powerup
        if(transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                switch(_powerupID)
                {
                    case 0:
                        player.ActiveTripleShot();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        Debug.Log("Speed Boost Activated");
                        break;
                    case 2:
                        player.ActivateShield();
                        Debug.Log("Shield is Activated");
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
