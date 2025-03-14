using UnityEngine;
using Photon.Pun;

public class GoalTrigger : MonoBehaviour
{
    PhotonView photonView;
    UIManager UIManager;
    private void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        photonView = transform.GetComponent<PhotonView>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();

            if (playerPhotonView.IsMine)
            {
                PlayerWins();
            }
        }
    }

    void PlayerWins()
    {
        Debug.Log("You Win!");
        photonView.RPC("RPC_NotifyLose", RpcTarget.Others);
        UIManager.WinScreen();
        //UIManager.Instance.WinScreen();
    }

    [PunRPC]
    void RPC_NotifyLose()
    {
        Debug.Log("You Lose!");
        UIManager.LoseScreen();
        //UIManager.Instance.LoseScreen();
    }
}
