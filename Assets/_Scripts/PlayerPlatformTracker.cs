using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformTracker : MonoBehaviour
{
    //[SerializeField] private GameObject lastSafePlatform;
    [SerializeField] private Vector3 lastSafePosition;
    private bool isOnPlatform = false;
    public bool IsOnPlatform => isOnPlatform;
    private void Start()
    {
        lastSafePosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //lastSafePosition = collision.transform.position;
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //lastSafePlatform = collision.gameObject;
            //lastSafePosition = collision.transform.position;
            isOnPlatform = false;
        }
    }

    public Vector3 GetLastSafePosition()
    {
        return lastSafePosition;
    }
}
