using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneNames
{
    public const string GameScene = "GameScene";
}

public class PhotonAutoConnect : MonoBehaviourPunCallbacks
{
    public void StartMultiplayerGame()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No available room found, creating a new one...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room! Starting game...");
        PhotonNetwork.LoadLevel(SceneNames.GameScene);
        //StartCoroutine(LoadGameSceneAsync(SceneNames.GameScene));
    }

    IEnumerator LoadGameSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}