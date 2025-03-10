using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviourPun
{
    [Header("Level Settings")]
    public int numberOfPlatforms = 20;
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float startMinGapY = 1.5f;
    public float startMaxGapY = 2.5f;

    [Header("Obstacle Settings")]
    public float startObstacleChance = 0.2f;
    public float maxObstacleChance = 0.5f;

    [Header("Difficulty Scaling")]
    public float maxGapIncrease = 3.5f;
    public float difficultyIncreaseHeight = 20f;

    private float yPos = 0;
    private GameObject lastPlatform;
    private GameObject endPlatform;

    private int levelSeed;

    public void Initialize()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            levelSeed = Random.Range(0, 1000);
            photonView.RPC("SyncLevelSeed", RpcTarget.OthersBuffered, levelSeed);
            GenerateLevel(levelSeed);
        }
    }

    [PunRPC]
    void SyncLevelSeed(int seed)
    {
        levelSeed = seed;
        GenerateLevel(levelSeed);
    }

    void GenerateLevel(int seed)
    {
        Random.InitState(seed);
        yPos = transform.position.y;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            float difficultyFactor = Mathf.Clamp01(yPos / difficultyIncreaseHeight);
            float currentMinGapY = Mathf.Lerp(startMinGapY, maxGapIncrease, difficultyFactor);
            float currentMaxGapY = Mathf.Lerp(startMaxGapY, maxGapIncrease + 1.0f, difficultyFactor);
            float currentObstacleChance = Mathf.Lerp(startObstacleChance, maxObstacleChance, difficultyFactor);

            Debug.Log("Difficulty Factor: " + difficultyFactor);

            float xPos = Random.Range(minX, maxX);
            float yGap = Random.Range(currentMinGapY, currentMaxGapY);
            yPos += yGap;

            GameObject platform = ObjectPooler.Instance.SpawnFromPool("Platform", new Vector2(xPos, yPos));
            SetObjectParent(platform);

            GetEndPlatform(i, platform);

            if (lastPlatform != null)
            {
                Vector2 lastPos = lastPlatform.transform.position;
                if (Mathf.Abs(xPos - lastPos.x) < 1.5f && yPos - lastPos.y < 2.0f)
                {
                    xPos += (xPos < 0) ? 1.5f : -1.5f;
                    platform.transform.position = new Vector2(xPos, yPos);
                }
            }

            lastPlatform = platform;


            if (Random.value < currentObstacleChance && i < numberOfPlatforms - 1)
            {
                GameObject obstacle = ObjectPooler.Instance.SpawnFromPool("Obstacle", new Vector2(xPos, yPos + 0.5f));
                SetObjectParent(obstacle);
            }
        }

        SpawnGoal();
    }

    private void SpawnGoal()
    {
        if (endPlatform != null)
        {
            Vector2 goalPos = endPlatform.transform.position + new Vector3(1, 2f, 0);
            GameObject goal = ObjectPooler.Instance.SpawnFromPool("Goal", goalPos);
            SetObjectParent(goal);
        }
    }

    private void SetObjectParent(GameObject obj)
    {
        obj.transform.SetParent(transform);
    }

    private void GetEndPlatform(int i, GameObject platform)
    {
        
        if (i == numberOfPlatforms - 1)
        {
            endPlatform = platform;
        }
    }
}
