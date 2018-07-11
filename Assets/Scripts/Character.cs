﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int id;
    public string characterName;
    private GameObject characterPrefab;
    public float speed = 2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    public void setCharacter(GameObject character)
    {
        characterPrefab = character;
    }

    public GameObject getPrefab()
    {
        return characterPrefab;
    }
}