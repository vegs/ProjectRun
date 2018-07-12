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
    public float switchLaneSpeed = 5f;

    private GameObject nextWaypoint; // keeps track of which empty within the map-section for player to aim for
    private GameObject previousWaypoint; // keeps track of which empty within the map-section for player to aim for

    public Vector3 mapDirection;
    public float targetPositionX;

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

        SetDirection();

        SetTargetPosition();

        moveTowardsTarget();
    }

    public void SetDirection()
    {
        mapDirection = (nextWaypoint.transform.position - previousWaypoint.transform.position).normalized;
    }

    public void SetTargetPosition()
    {
        Vector2 dirXZ = new Vector2(mapDirection.x, mapDirection.z);

        //targetPositionX = ((this.transform.position.z - previousWaypoint.transform.position.z) / (Vector2.Dot(Vector2.right,dirXZ))) + previousWaypoint.transform.position.x;
        targetPositionX = nextWaypoint.transform.position.x;

    }

    public void moveTowardsTarget()
    {
        //if (this.transform.position.x < targetPositionX)
        //{
        //    this.transform.position += Vector3.right * switchLaneSpeed * Time.deltaTime;
        //}
        //else if (this.transform.position.x > targetPositionX)
        //{
        //    this.transform.position += Vector3.left * switchLaneSpeed * Time.deltaTime;
        //}
        this.transform.position = new Vector3(targetPositionX, this.transform.position.y, this.transform.position.z);

    }

    private void setEmptyPair()
    {
        for (int i=1; i<currentLaneWaypoints.Count; i++) //the line the error is pointing to
        {
            GameObject waypoint = currentLaneWaypoints[i];
            if (this.transform.position.z < waypoint.transform.position.z)
            {
                nextWaypoint = waypoint;
                previousWaypoint = currentLaneWaypoints[i-1];
                return;
            }
        }


        // player has ran past the last empty on map-section.
        setCurrentMapSection(currentMapSection.GetComponent<MapSection_behaviour>().getNextMapSection());
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

    public void switchLaneRight()
    {
        if (currentMapSection.GetComponent<MapSection_behaviour>().numberOfLanes > currentLane)
        {
            setCurrentLane(currentLane + 1);
        }
    }
    public void switchLaneLeft()
    {
        if (currentMapSection.GetComponent<MapSection_behaviour>().numberOfLanes > currentLane)
        {
            setCurrentLane(currentLane - 1);
        }
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
