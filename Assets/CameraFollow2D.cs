using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;

    public void Initialize()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView view = obj.GetComponent<PhotonView>();
            if (view.IsMine)
            {
                cam.Follow = obj.transform;
                break;
            }
        }
    }
}
