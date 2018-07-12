using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : Character
{

    // Use this for initialization
    void Start()
    {
        this.characterName = "Cube";

        
        //setCharacter(this.gameObject);



        //setCharacter(GameObject.FindGameObjectWithTag(this.characterName));
    }

    // Update is called once per frame
    void Update()
    {

        //this.transform.position -= Vector3.forward * speed * Time.deltaTime;
    }
}