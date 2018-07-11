using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSection_behaviour : MonoBehaviour {

    public List<GameObject> lane_1 = new List<GameObject>();
    public List<GameObject> lane_2 = new List<GameObject>();
    public List<GameObject> lane_3 = new List<GameObject>();
    public List<GameObject> lane_4 = new List<GameObject>();

    public GameObject nextMapSection;


    public List<GameObject> LaneEmpties(int laneNo)
    {
        switch(laneNo)
        {
            case 1:
                return lane_1;
                break;
            case 2:
                return lane_2;
                break;
            case 3:
                return lane_3;
                break;
            default: // lane 4 is default
                return lane_4;
                break;
        }
    }



    //public List<GameObject> Lane1Empties()
    //{
    //    return lane_1;
    //}

    //public List<GameObject> Lane2Empties()
    //{
    //    return lane_2;
    //}

    //public List<GameObject> Lane3Empties()
    //{
    //    return lane_3;
    //}

    //public List<GameObject> Lane4Empties()
    //{
    //    return lane_4;
    //}

}
