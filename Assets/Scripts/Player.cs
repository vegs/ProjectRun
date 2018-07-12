using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool moving = false;
    private int currentLane = 2;
    public GameObject currentMapSection;
    public List<GameObject> currentLaneWaypoints = new List<GameObject>();
    private MapGenerator mapGen;

    public int id;
    public string characterName;
    private GameObject characterPrefab;
    public float speed = 2f;

    private GameObject nextWaypoint; // keeps track of which empty within the map-section for player to aim for
    private GameObject previousWaypoint; // keeps track of which empty within the map-section for player to aim for

    void Start()
    {
        setCharacter(this.gameObject);

        mapGen = GameObject.Find("Scripts").GetComponent<MapGenerator>();

        setCurrentMapSection(mapGen.getFirstSection());

        StartPlayerMove();
    }

    void Update()
    {
        if (moving)
        {
            checkNewDirection();
        }




    }

    void checkNewDirection()
    {
        setEmptyPair();

        //if (this.transform.position.z > currentLaneEmpties[currentEmptyInt].transform.position.z)
        //{
        //    setEmptyPair();

        //    this.transform.position =
        //    currentLaneEmpties[currentEmptyInt - 1].transform.position
        //    + (this.transform.position - currentLaneEmpties[currentEmptyInt].transform.position).magnitude
        //    / (currentLaneEmpties[currentEmptyInt].transform.position - currentLaneEmpties[currentEmptyInt - 1].transform.position).magnitude
        //    * (currentLaneEmpties[currentEmptyInt].transform.position - currentLaneEmpties[currentEmptyInt - 1].transform.position).normalized;
        //}
    }

    private void setEmptyPair()
    {
        for (int i=1; i<currentLaneWaypoints.Count; i++) //the line the error is pointing to
        {
            GameObject waypoint = currentLaneWaypoints[i];
            if (GetComponent<Transform>().position.z < waypoint.GetComponent<Transform>().position.z)
            {
                nextWaypoint = waypoint;
                previousWaypoint = currentLaneWaypoints[i-1];
                return;
            }
        }

        // player has ran past the last empty on map-section.
        //setCurrentMapSection(currentMapSection.GetComponent<MapSection_behaviour>().getNextMapSection());
    }

    public void StartPlayerMove ()
    {
        updateCurrentLaneWaypoints();
        moving = true;

    }


    public void updateCurrentLaneWaypoints()
    {
        currentLaneWaypoints = currentMapSection.GetComponent<MapSection_behaviour>().LaneEmpties(currentLane);
    }

    public void setCurrentMapSection(GameObject mapSection)
    {
        currentMapSection = mapSection;
        updateCurrentLaneWaypoints();
        nextWaypoint = currentLaneWaypoints[1];;
        previousWaypoint = currentLaneWaypoints[0];

    }

    public GameObject getCurrentMapSection()
    {
        return currentMapSection;
    }

    public void setCurrentLane(int lane)
    {
        currentLane = lane;
        updateCurrentLaneWaypoints();

    }

    public int getCurrentLane()
    {
        return currentLane;
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
