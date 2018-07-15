using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {

    private bool moving = false;
    public int currentLane = 0;
    public int previousLane = 0;
    public GameObject currentMapSection;
    public MapSection mapSection;
    public List<GameObject> currentLaneCheckpoints = new List<GameObject>();
    private MapGenerator mapGen;

    public int id;
    public string characterName;
    private GameObject characterPrefab;
    public float speed = 10f;
    public float switchLaneSpeed = 15f;

    private GameObject nextCheckpoint; // keeps track of which empty within the map-section for player to aim for
    private GameObject previousCheckpoint; // keeps track of which empty within the map-section for player to aim for

    public Vector3 mapDirection;
    public float targetPositionX;

    double switchedLaneMillis = -1;

    

    void Start()
    {
        setCharacter(this.gameObject);

        mapGen = GameObject.Find("Scripts").GetComponent<MapGenerator>();

        setCurrentMapSection(mapGen.getFirstSection());

        //StartPlayerMove();


    }

    void Update()
    {
        if (moving)
        {
            checkNewDirection();
            moveForwards();
            moveTowardsTarget();
        }
    }

    void checkNewDirection()
    {
        setCheckpointPair();

        SetDirection();

        SetTargetPosition();        
    }

    public void SetDirection()
    {
        mapDirection = (nextCheckpoint.transform.position - previousCheckpoint.transform.position).normalized;
        this.transform.localEulerAngles = mapDirection;
    }

    public void SetTargetPosition()
    {
        Vector2 dirXZ = new Vector2(mapDirection.x, mapDirection.z);

        //targetPositionX = ((this.transform.position.z - previousCheckpoint.transform.position.z) / (Vector2.Dot(Vector2.right,dirXZ))) + previousCheckpoint.transform.position.x;
        targetPositionX = nextCheckpoint.transform.position.x;

    }

    void moveForwards()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void moveTowardsTarget()
    {
        float newPosX = this.transform.position.x;
        if (this.transform.position.x < targetPositionX)
        {
            newPosX += switchLaneSpeed * Time.deltaTime;
            newPosX = Mathf.Min(newPosX, targetPositionX);
        }
        else if (this.transform.position.x > targetPositionX)
        {
            //this.transform.position += Vector3.left * switchLaneSpeed * Time.deltaTime;
            newPosX -= switchLaneSpeed * Time.deltaTime;
            newPosX = Mathf.Max(newPosX, targetPositionX);
        }
        this.transform.position = new Vector3(newPosX, this.transform.position.y, this.transform.position.z);

    }

    private void setCheckpointPair()
    {
        for (int i=1; i<currentLaneCheckpoints.Count; i++) //the line the error is pointing to
        {
            GameObject checkpoint = currentLaneCheckpoints[i];
            if (this.transform.position.z < checkpoint.transform.position.z)
            {
                nextCheckpoint = checkpoint;
                previousCheckpoint = currentLaneCheckpoints[i-1];
                return;
            }
        }


        // player has ran past the last empty on map-section.
        setCurrentMapSection(mapSection.getNextMapSection());
    }

    public void StartPlayerMove ()
    {
        updateCurrentLaneCheckpoints();
        moving = true;

    }


    public void updateCurrentLaneCheckpoints()
    {
        currentLaneCheckpoints = mapSection.LaneCheckpoints(currentLane);
    }

    public void setCurrentMapSection(GameObject mapSectionGO)
    {
        currentMapSection = mapSectionGO;
        mapSection = currentMapSection.GetComponent<MapSection>();
        updateCurrentLaneCheckpoints();
        nextCheckpoint = currentLaneCheckpoints[1];;
        previousCheckpoint = currentLaneCheckpoints[0];

    }

    public GameObject getCurrentMapSection()
    {
        return currentMapSection;
    }

    public void switchLaneRight()
    {
        int nextLaneNo = currentLane + 1;
        if (mapSection.checkValidLane(nextLaneNo) && mapSection.numberOfLanes > nextLaneNo)
        {
            SwitchToLane(nextLaneNo, System.DateTime.Now.Millisecond);
        }
    }
    public void switchLaneLeft()
    {
        int nextLaneNo = currentLane - 1;
        if (nextLaneNo>-1)
        {
            SwitchToLane(nextLaneNo, System.DateTime.Now.Millisecond);
        }
    }

    public void revertLaneSwitch()
    {
        SwitchToLane(previousLane, System.DateTime.Now.Millisecond);
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

    void SwitchToLane(int lane, double timestamp)
    {
        if (moving)
        {
            this.photonView.RPC("setCurrentLane", PhotonTargets.All, lane, timestamp);

        }
    }

    [PunRPC]
    void setCurrentLane(int lane, double timestamp)
    {
        double now = System.DateTime.Now.Millisecond;
        if (checkIfLaneIsEmpty(lane, timestamp))
        {
            previousLane = currentLane;
            currentLane = lane;
            switchedLaneMillis = timestamp;
            updateCurrentLaneCheckpoints();
        }
    }

    bool checkIfLaneIsEmpty(int laneNo, double timestamp)
    {
        GameObject[] playerGOs = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerGO in playerGOs)
        {
            Player player = playerGO.GetComponent<Player>();
            bool isClose = (this.transform.position.z < (playerGO.transform.position.z + 2)) && (this.transform.position.z > (playerGO.transform.position.z - 2));
            if (player.getCurrentLane() == laneNo && isClose)
            {
                if(player.switchedLaneMillis < timestamp)
                {
                    return false;
                }
                else
                {
                    player.revertLaneSwitch();
                }
                
            }
        }
        return true;
    }
}
