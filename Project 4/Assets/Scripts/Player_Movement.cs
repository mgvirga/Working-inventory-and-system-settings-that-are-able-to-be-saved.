using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //speed for rotating
    public float rotateSpeed = 5.0f;

	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        if(Time.timeScale == 1)
        {
            //rotates camera with X-Axis of mouse
            float horizontal = rotateSpeed * Input.GetAxis("Mouse X");
            Vector3 lookhere = new Vector3(0, horizontal, 0);
            transform.Rotate(lookhere);


            //move with WASD keys
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward / 8;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += -transform.forward / 8;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += -transform.right / 8;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right / 8;
            }
        }
        
	}
}
