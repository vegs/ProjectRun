using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSection : MonoBehaviour
{

    public List<List<GameObject>> lanes = new List<List<GameObject>>();
    public List<GameObject> lane_1 = new List<GameObject>();
    public List<GameObject> lane_2 = new List<GameObject>();
    public List<GameObject> lane_3 = new List<GameObject>();
    public List<GameObject> lane_4 = new List<GameObject>();
    public List<GameObject> lane_5 = new List<GameObject>();
    public List<GameObject> lane_6 = new List<GameObject>();
    public List<GameObject> lane_7 = new List<GameObject>();

    public GameObject nextMapSection;
    public int numberOfLanes = 3;

    private void Start()
    {
        lanes.Add(lane_1);
        lanes.Add(lane_2);
        lanes.Add(lane_3);
        lanes.Add(lane_4);
        lanes.Add(lane_5);
        lanes.Add(lane_6);
        lanes.Add(lane_7);
    }

    public List<GameObject> LaneWaypoints(int laneNo)
    {
        return lanes[laneNo];
    }

    public void setNextMapSection(GameObject mapSection)
    {
        nextMapSection = mapSection;
    }

    public bool checkValidLane(int laneNo)
    {
        return lanes[laneNo].Count > 1;
    }

    public GameObject getNextMapSection()
    {
        return nextMapSection;
    }
}
