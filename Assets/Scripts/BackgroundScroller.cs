using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
    bool hasSpawned = false;
    private float halfWidth;
    private float doubleWidth;
    public GameObject planeToSpawn;
    public float moveSpeed;

    void Start()
    {
        // Work out half and double the width of this GameObject
        halfWidth = (transform.lossyScale.x / 2);
        doubleWidth = (transform.lossyScale.x * 2);
    }

    void Update()
    {
        // Move towards the left!
        transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;

        // Destroy object when its off camera
        if (transform.position.x + doubleWidth < -GetScreenWidth())
            Destroy(gameObject);

        // only need to spawn another object once!
        if (hasSpawned) return;

        if (transform.position.x + halfWidth - 5f < GetScreenWidth())
        {
            float newPlaneHalfWidth = planeToSpawn.transform.lossyScale.x / 2;

            // Spawn it at the right edge of this object
            Vector3 spawnLocation = new Vector3(transform.position.x + (halfWidth + newPlaneHalfWidth), transform.position.y, transform.position.z);

            Instantiate(planeToSpawn, spawnLocation, transform.rotation);
            hasSpawned = true;
        }


    }

    public float GetScreenWidth()
    {
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperCorner);
        return targetWidth.x;
    }
}
