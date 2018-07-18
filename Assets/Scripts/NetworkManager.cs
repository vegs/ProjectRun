using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour
{
    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = true;
    public Player player;
    private Player myPlayer;
    public string Version = "0.0.1";
    public int numberOfPlayers = 1;
    bool gameStarted = false;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;


    public virtual void Start()
    {
        PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
    }

    public virtual void Update()
    {
        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version);
        }

        if (!gameStarted)
        {
            checkStart();
        }

  
    }

    void checkStart()
    {
        if (PhotonNetwork.inRoom && PhotonNetwork.playerList.Length == numberOfPlayers)
        {
            
            if(GameObject.FindGameObjectsWithTag("Player").Length == numberOfPlayers) {
                StartGame();
            
            }
        }
    }


    // below, we implement some callbacks of PUN
    // you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage


    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        //player.Spawn();
        GameObject playerInstance = PhotonNetwork.Instantiate(player.gameObject.name, new Vector3(0, 0.5f, -3f), Quaternion.identity, 0) as GameObject;
        //GameObject playerInstance2 = PhotonNetwork.Instantiate(player.gameObject.name, new Vector3(0, 0.5f, -3f), Quaternion.identity, 0) as GameObject;
        myPlayer = playerInstance.GetComponent<Player>();
        //MapGenerator mapGen = GetComponent<MapGenerator>();

        //playerInstanceComp.setCurrentMapSection(mapGen.getFirstSection());

        //playerInstanceComp.StartPlayerMove();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
        playerInstance.GetComponentInChildren<Camera>().enabled = true;

        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
    }

    private void StartGame()
    {
        gameStarted = true;
        myPlayer.StartPlayerMove();
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<MapGenerator>().WatchPlayers(GameObject.FindGameObjectsWithTag("Player"));
    }
}
