using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PlayerBehaviour : MonoBehaviour {

    public float speed = 1f;

    void Update()
    {
        InputMovement();
    }

    void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
            this.transform.position += Vector3.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            this.transform.position -= Vector3.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            this.transform.position += Vector3.right * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            this.transform.position -= Vector3.right * speed * Time.deltaTime;
        
    }
}
