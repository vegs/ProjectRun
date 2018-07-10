using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class MapGenerator : MonoBehaviour {

    public GameObject mapSection;
    public float speed = 10f;
    public int nCurrentSections = 5;
    private List<GameObject> activeMapSections = new List<GameObject>();
    private bool initiated = false;

	// Use this for initialization
	void Start () {
        initiateMap();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject firstSection = (GameObject)activeMapSections[0];

        if (initiated && firstSection && firstSection.transform.position.z < -5f)
        {
            addSection();
            removeFirstSection();
        }
        else
        {

        }
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
        GameObject firstSection = (GameObject)activeMapSections[0];
        GameObject lastSection = activeMapSections[activeMapSections.Count-1];
        //activeMapSections.Add(Instantiate(mapSection, new Vector3(lastSection.transform.position.x, lastSection.transform.position.y, lastSection.transform.position.z+5f), Quaternion.identity));
        GameObject a = (GameObject)Instantiate(mapSection, new Vector3(lastSection.transform.position.x, lastSection.transform.position.y, lastSection.transform.position.z + 5f), Quaternion.identity);

        activeMapSections.Add(a);
        Debug.Log(activeMapSections);
    }

    void removeFirstSection()
    {
        GameObject firstSection = activeMapSections[0];
        activeMapSections.Remove(firstSection);
        Destroy(firstSection);
    }
}
