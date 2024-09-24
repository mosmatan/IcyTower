using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class BoostsManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private List<GameObject> boosts;
    [SerializeField] private List<Transform> spawners;
    [SerializeField] private float minBoostTime;
    [SerializeField] private float maxBoostTime;
    
    
    private float nextBoostTime;
    private float timeCounter = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        nextBoostTime = Random.Range(minBoostTime, maxBoostTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, cameraTrans.transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        if (timeCounter >= nextBoostTime)
        {
            spawnBoost();
            timeCounter = 0f;
            nextBoostTime = Random.Range(minBoostTime, maxBoostTime);
        }

        timeCounter += Time.deltaTime;
    }

    private void spawnBoost()
    {
        GameObject boost = boosts[Random.Range(0, boosts.Count)];
        boost.SetActive(true);
        boost.transform.position = spawners[Random.Range(0, spawners.Count)].position;
    }
}
