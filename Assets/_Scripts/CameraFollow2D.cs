using Photon.Pun;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0;
    private float fixedX;
    private float fixedZ;

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

        fixedX = transform.position.x;
        fixedZ = -1;
    }

    void LateUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > 2)
        {
            Vector3 targetPosition = new Vector3(fixedX, player.transform.position.y + 2f, fixedZ);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}


