using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerSpawner playerSpawner;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] ProceduralLevelGenerator levelGenerator;
    [SerializeField] CameraFollow2D cameraFollow2D;
    private void Start()
    {
        if (playerSpawner != null)
            playerSpawner.Initialize();

        if (objectPooler != null)
            objectPooler.Initialize();

        if (levelGenerator != null/* && PhotonNetwork.CurrentRoom.PlayerCount == 1*/)
            levelGenerator.Initialize();

        if(cameraFollow2D != null)
            cameraFollow2D.Initialize();
    }

    private void OnEnable()
    {
        UIManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        UIManager.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        cameraFollow2D.player.GetComponent<PlayerMovement>().enabled = false;
        cameraFollow2D.enabled = false;
    }
}
