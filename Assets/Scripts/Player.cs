using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public int currentLane = 2;
    public GameObject currentMapSection;
    public List<GameObject> currentLaneEmpties = new List<GameObject>();

    private int currentEmptyInt = 1; // keeps track of which empty within the map-section for player to aim for

    void Start()
    {
   

    }

    public void Spawn()
    {
        //Debug.Log("test1: " + character.name);
        PhotonNetwork.Instantiate(getPrefab().name, new Vector3(0, 0.5f, -3f), Quaternion.identity, 0);
    }

    private void Update()
    {
        if(this.transform.position.z > currentLaneEmpties[currentEmptyInt].transform.position.z)
        {
            setEmptyPair();
        }

        this.transform.position =
            currentLaneEmpties[currentEmptyInt - 1].transform.position
            + (this.transform.position - currentLaneEmpties[currentEmptyInt].transform.position).magnitude
            / (currentLaneEmpties[currentEmptyInt].transform.position - currentLaneEmpties[currentEmptyInt - 1].transform.position).magnitude
            * (currentLaneEmpties[currentEmptyInt].transform.position - currentLaneEmpties[currentEmptyInt - 1].transform.position).normalized;




    }

    private void setEmptyPair()
    {
        for(int i = 1; i < currentLaneEmpties.Count-1; i++)
        {
            if(this.transform.position.z < currentLaneEmpties[i].transform.position.z)
            {
                currentEmptyInt = i;
                return;
            }
        }

        // player has ran past the last empty on map-section.
        currentMapSection = currentMapSection.GetComponent<MapSection_behaviour>().nextMapSection;
        currentEmptyInt = 1;
    }





    public void GrabMapSectionEmpties()
    {
        currentLaneEmpties = currentMapSection.GetComponent<MapSection_behaviour>().LaneEmpties(currentLane);
    }
}
