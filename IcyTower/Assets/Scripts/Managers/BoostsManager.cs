using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spawning of boost items at random intervals in the game environment.
/// </summary>
public class BoostsManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans; // Reference to the camera's transform.
    [SerializeField] private List<GameObject> boosts; // List of boost prefabs to spawn.
    [SerializeField] private List<Transform> spawners; // List of spawn points for the boosts.
    [SerializeField] private float minBoostTime; // Minimum time interval between boost spawns.
    [SerializeField] private float maxBoostTime; // Maximum time interval between boost spawns.

    private float nextBoostTime; // Time until the next boost is spawned.
    private float timeCounter = 0f; // Counter to track elapsed time.

    private void Start()
    {
        // Set the next boost spawn time randomly within the defined range.
        nextBoostTime = Random.Range(minBoostTime, maxBoostTime);
    }

    private void Update()
    {
        // Align the position of the manager with the camera's Y position.
        transform.position = new Vector3(0, cameraTrans.transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        // Check if the time counter has reached the next boost spawn time.
        if (timeCounter >= nextBoostTime)
        {
            spawnBoost(); // Spawn a new boost.
            timeCounter = 0f; // Reset the time counter.
            nextBoostTime = Random.Range(minBoostTime, maxBoostTime);
        }

        // Increment the time counter by the elapsed time.
        timeCounter += Time.deltaTime;
    }

    private void spawnBoost()
    {
        // Select a random boost prefab and activate it.
        GameObject boost = boosts[Random.Range(0, boosts.Count)];
        boost.SetActive(true);
        // Set the boost's position to a random spawn point.
        boost.transform.position = spawners[Random.Range(0, spawners.Count)].position;
    }
}