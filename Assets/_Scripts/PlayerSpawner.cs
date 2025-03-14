using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    private Vector2 spawnPosVal;
    public void Initialize()
    {
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        spawnPosVal = new Vector2(Random.Range(-2, 2), -5f);
        Vector2 spawnPos = spawnPosVal;
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }
}
