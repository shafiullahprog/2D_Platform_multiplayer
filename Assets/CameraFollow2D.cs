using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] GameObject player;
    public void Initialize()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView view = obj.GetComponent<PhotonView>();
            if (view.IsMine)
            {
                player = obj;
                break;
            }
        }
    }

    private void LateUpdate()
    {
        var pos = transform.position + (transform.position - player.transform.position);
        pos.x = transform.position.x;
        pos.y = transform.position.y;
        pos.z = -10;
        transform.position = pos;
    }
}
