using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class Obstacles : MonoBehaviour
{
    [SerializeField]private PhotonView photonView;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            photonView = player.GetComponent<PhotonView>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResetPlayerPosition(collision.gameObject);
        }
    }

    private void ResetPlayerPosition(GameObject player)
    {
        PlayerPlatformTracker platformTracker = player.GetComponent<PlayerPlatformTracker>();

        if (platformTracker != null)
        {
            Vector3 lastSafePosition = platformTracker.GetLastSafePosition();
            
            StartCoroutine(SmoothReset(player, lastSafePosition));
        }
    }


    private IEnumerator SmoothReset(GameObject player, Vector3 targetPosition)
    {
        ParticleSystem particle =  player.GetComponentInChildren<ParticleSystem>();
        if (particle != null)
        {
            particle.Play();
        }

        yield return new WaitForSeconds(0.5f);
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.angularVelocity = 0;
            playerRb.simulated = false;
        }

        float elapsedTime = 0f;
        Vector3 startPosition = player.transform.position;

        while (elapsedTime < 1f)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * 2;
            yield return null;
        }

        player.transform.position = targetPosition; 
        if (playerRb != null)
        {
            playerRb.simulated = true;
        }
    }
}
