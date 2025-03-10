using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;


    private void Start()
    {
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-2f, 2f), -3f);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }
}
