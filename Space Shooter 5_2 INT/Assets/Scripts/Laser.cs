using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //to set a variable for speed of 8
    [SerializeField]
    private float _speed = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make the laser move upwards
        //translate laser up 
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if laser position is great than 8 on the y axis
        if(transform.position.y > 8)
        {
            //destroy the laser
            Destroy(gameObject);
        }
        
    }
}
