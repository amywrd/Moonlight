using UnityEngine;
using System.Collections;

public class LevelSetMovement : MonoBehaviour
{

    public float moveSpeed;

    float doubleWidth;
    float halfWidth;
    bool hasSentSpawnNotification = false;

    void Start()
    {
        if (moveSpeed == 0) moveSpeed = 7f;

        // Work out half and double the width of this GameObject
        halfWidth = (transform.lossyScale.x / 2);
        doubleWidth = (transform.lossyScale.x * 2);
    }

    void Update()
    {
        // move to the left
        transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;

        // destroy object if it is off the camera
        if (transform.position.x + doubleWidth < -GetScreenWidth() - 10f) Destroy(gameObject);

        if (transform.position.x + halfWidth < GetScreenWidth() && !hasSentSpawnNotification)
        {
            LevelSetManager.SpawnNewLevelSet(gameObject);
            hasSentSpawnNotification = true;
        }
    }

    public float GetScreenWidth()
    {
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperCorner);
        return targetWidth.x;
    }
}
