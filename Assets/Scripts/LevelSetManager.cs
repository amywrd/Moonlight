using UnityEngine;
using System.Collections;

public class LevelSetManager : MonoBehaviour
{

    public GameObject[] levelSets;
    public float moveSpeed;

    float halfWidth;
    float doubleWidth;
    static GameObject setToSpawn;
    static bool newLevelSetNeeded = true;
    static float _moveSpeed;

    void Start()
    {
        if (moveSpeed == 0) moveSpeed = 15f;
    }

    void Update()
    {
        if (newLevelSetNeeded) GetLevelSet();
        _moveSpeed = moveSpeed;
    }

    public static void SpawnNewLevelSet(GameObject oldLevelSet)
    {
        float newLevelSetHalfWidth = setToSpawn.transform.lossyScale.x / 2;
        float oldLevelSetHalfWidth = oldLevelSet.transform.lossyScale.x / 2;
        float x = oldLevelSet.transform.position.x + (oldLevelSet.transform.lossyScale.x);
        Vector3 spawnLocation = new Vector3(x, oldLevelSet.transform.position.y, oldLevelSet.transform.position.z);

        GameObject newLevelSet = (GameObject)Instantiate(setToSpawn, spawnLocation, oldLevelSet.transform.rotation);
        newLevelSet.GetComponent<LevelSetMovement>().moveSpeed = _moveSpeed;
        // get another level set for next time
        newLevelSetNeeded = true;
    }

    void GetLevelSet()
    {
        setToSpawn = levelSets[Random.Range(0, levelSets.Length)];
        newLevelSetNeeded = false;
    }
}
