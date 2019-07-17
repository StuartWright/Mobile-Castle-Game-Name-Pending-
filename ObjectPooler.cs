using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }
    //public static ObjectPooler Instance;
    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

	void Start ()
    {
        //Instance = this;
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in Pools)
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                //obj.SetActive(false);
                ObjectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.Tag, ObjectPool);
        }
	}
	
	public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        GameObject ObjectToSpawn = PoolDictionary[tag].Dequeue();

        ObjectToSpawn.SetActive(true);
        ObjectToSpawn.transform.position = position;

        PoolDictionary[tag].Enqueue(ObjectToSpawn);

        return ObjectToSpawn;
    }
}
