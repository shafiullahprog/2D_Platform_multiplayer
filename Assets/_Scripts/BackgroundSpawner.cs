using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public float offset = 1f;

    private void Start()
    {
        SpawnBackground();
    }

    [ContextMenu("Spawn")]
    void SpawnBackground()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float screenWidth = cam.orthographicSize * 2f * cam.aspect;
        float screenHeight = cam.orthographicSize * 2f;

        SpriteRenderer sr = backgroundPrefab.GetComponent<SpriteRenderer>();
        float spriteWidth = sr.bounds.size.x;
        float spriteHeight = sr.bounds.size.y;

        float scaleX = screenWidth / spriteWidth;
        float scaleY = screenHeight / spriteHeight;

        Vector3 scaledSize_Height = new Vector3(1, scaleY, 1f);
        Vector3 scaledSize_Width = new Vector3(scaleX,1, 1f);

        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 10));
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 10));
        Vector3 bottomEdge = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, 10));

        GameObject left = SpawnAndScaleBackground(leftEdge + Vector3.left * offset, scaledSize_Height);
        GameObject right = SpawnAndScaleBackground(rightEdge + Vector3.right * offset, scaledSize_Height);
        
        left.transform.SetParent(transform);
        right.transform.SetParent(transform);

        SpawnAndScaleBackground(bottomEdge + Vector3.down * offset, scaledSize_Width);
    }

    GameObject SpawnAndScaleBackground(Vector3 position, Vector3 scale)
    {
        GameObject bg = Instantiate(backgroundPrefab, position, Quaternion.identity);
        bg.transform.localScale = scale;
        return bg;
    }
}
