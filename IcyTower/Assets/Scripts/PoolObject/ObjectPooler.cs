using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages object pooling for efficient reuse of game objects.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag; // Identifier for the pool.
        public GameObject prefab;
        public int size; // Number of objects in the pool.
    }

    [SerializeField] private List<Pool> pools = new List<Pool>(); 
    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>(); // Dictionary to hold the object queues.

    private void Start()
    {
        createsQueues(); // Initialize object queues.
    }

    private void createsQueues()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform); 
                obj.SetActive(false); // Deactivate them initially.
                queue.Enqueue(obj); // Add to the queue.
            }

            poolDict.Add(pool.tag, queue); // Add the queue to the dictionary.
        }
    }

    public GameObject DrawObjectFrom(string tag)
    {
        if (!poolDict.ContainsKey(tag))
        {
            throw new ArgumentException($"Pool with tag {tag} not found"); // Throw an error if the tag doesn't exist.
        }

        GameObject obj = poolDict[tag].Dequeue(); 
        obj.SetActive(true); // Activate the object.

        poolDict[tag].Enqueue(obj); // Re-add it to the queue for future use.

        return obj; // Return the activated object.
    }
}