using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 100.0f;
    public Transform transGun;
    public Transform gun;
    public GameObject bulletObj;

    void Update()
    {    
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

       
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);
        

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

        if(Input.GetKey(KeyCode.T))
        {
            transGun.RotateAround(transGun.position, transGun.right, -2);
        }
        else if (Input.GetKey(KeyCode.G))
        {
            transGun.RotateAround(transGun.position, transGun.right, 2);
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(bulletObj, gun.position, gun.rotation);
        }

    }
}
