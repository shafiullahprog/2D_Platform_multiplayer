using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] ProceduralLevelGenerator levelGenerator;
    [SerializeField] PlayerMovement playerMovement;

    private void Start()
    {
        if(playerMovement!=null)
            playerMovement.Initialize();
        
        if (objectPooler != null)
            objectPooler.Initialize();

        if (levelGenerator != null)
            levelGenerator.Initialize();
    }
}
