using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    // for private variables naming convention should be as follows: _name
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        //current player position = new position(0,0,0)
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {   //transform.Translate(new Vector3(1,0,0)) 1 * 5 * Time.deltatime 5m/s
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
        
    }
}
