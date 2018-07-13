using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PlayerBehaviour : MonoBehaviour {

    public float speed = 1f;
    private Player player;
    private PhotonView photonView;

    void Start()
    {
        player = this.GetComponent<Player>();
        photonView = this.GetComponent<PhotonView>();
    }
    void Update()
    {
        if(photonView.isMine){
            InputMovement();
        }
    }

    void InputMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.switchLaneLeft();
        }

        //if (Input.GetKey(KeyCode.E))
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.switchLaneRight();
        }

        //if (Input.GetKey(KeyCode.W))
        //    this.transform.position += Vector3.forward * speed * Time.deltaTime;

        //if (Input.GetKey(KeyCode.S))
        //    this.transform.position -= Vector3.forward * speed * Time.deltaTime;

        //if (Input.GetKey(KeyCode.D))
        //    this.transform.position += Vector3.right * speed * Time.deltaTime;

        //if (Input.GetKey(KeyCode.A))
        //    this.transform.position -= Vector3.right * speed * Time.deltaTime;
    }
}
