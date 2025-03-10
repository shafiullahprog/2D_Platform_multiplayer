using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerSpawner playerSpawner;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] ProceduralLevelGenerator levelGenerator;
    private void Start()
    {
        if (playerSpawner != null)
            playerSpawner.Initialize();

        if (objectPooler != null)
            objectPooler.Initialize();

        if (levelGenerator != null/* && PhotonNetwork.CurrentRoom.PlayerCount == 1*/)
           levelGenerator.Initialize();
    }
}
