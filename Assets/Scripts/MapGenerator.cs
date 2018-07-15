using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Photon.MonoBehaviour {

    public GameObject mapSection;
    public float speed = 10f;
    public int nCurrentSections = 5;
    public List<GameObject> activeMapSections = new List<GameObject>();
    private bool initiated = false;
    private List<GameObject> mapOrder = new List<GameObject>();
    private GameObject[] players;

    // Use this for initialization
    void Awake () {
        initiateMap();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject firstSection = (GameObject)activeMapSections[0];

        bool generateNew = false;
        if(initiated && firstSection && players != null)
        {
            foreach (GameObject player in players)
            {
                if(player.transform.position.z > activeMapSections[activeMapSections.Count - 5].transform.position.z)
                {
                    generateNew = true;
                }
            }
        }
        if (generateNew)
        {
            addSection();
        }

        //if (initiated && firstSection && firstSection.transform.position.z < -5f)
        //{
        //    addSection();
        //    removeFirstSection();
        //}
        //else
        //{

        //}
    }


    void initiateMap(){
        GameObject a = (GameObject)Instantiate(mapSection, new Vector3(0, 0, 0), Quaternion.identity);
        activeMapSections.Add(a);

        for (int i=0; i<nCurrentSections; i++)
        {
            addSection();
        }

        initiated = true;
    }

    void addSection ()
    {
        //GameObject firstSection = (GameObject)activeMapSections[0];
        GameObject lastSection = activeMapSections[activeMapSections.Count-1];
        //activeMapSections.Add(Instantiate(mapSection, new Vector3(lastSection.transform.position.x, lastSection.transform.position.y, lastSection.transform.position.z+5f), Quaternion.identity));
        GameObject a = (GameObject)Instantiate(mapSection, new Vector3(lastSection.transform.position.x, lastSection.transform.position.y, lastSection.transform.position.z + 5f), Quaternion.identity);

        lastSection.GetComponent<MapSection>().setNextMapSection(a);

        activeMapSections.Add(a);
        Debug.Log(activeMapSections);
    }

    void removeFirstSection()
    {
        GameObject firstSection = activeMapSections[0];
        activeMapSections.Remove(firstSection);
        //Destroy(firstSection);
    }
    public GameObject getFirstSection()
    {
        if (initiated)
        {
            GameObject firstSection = (GameObject)activeMapSections[0];
            return firstSection;
        }
        else
        {
            Debug.Log("FAIL");
            return null;
        }

       
    }

    public void WatchPlayers(GameObject[] players)
    {
        this.players = players;
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
 
        if (stream.isWriting && PhotonNetwork.isMasterClient)
        {
            //This is our player
            stream.SendNext(mapOrder);
        }

        else if(!stream.isWriting && !PhotonNetwork.isMasterClient)
        {
            //This is someone elses player. We need to receive their position and update our version of that player
            mapOrder = (List<GameObject>)stream.ReceiveNext();
        }

    }

}
