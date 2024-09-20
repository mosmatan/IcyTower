using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        createsQueues();
    }

    private void createsQueues()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = GameObject.Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            poolDict.Add(pool.tag, queue);
        }
    }

    public GameObject DrawObjectFrom(string tag)
    {
        if(!poolDict.ContainsKey(tag))
        {
            throw new ArgumentException($"Pool with tag {tag} has not found");
        }

        GameObject obj = poolDict[tag].Dequeue();
        obj.SetActive(true);

        poolDict[tag].Enqueue(obj);

        return obj;
    }

}
