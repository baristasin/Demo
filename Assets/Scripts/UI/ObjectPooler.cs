using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary; // Farklı türlerden veri yapılarını tutan tür Dictionary kullanıldı.
                                                                 // Queue, veri yapılarındaki "stack" mantığıyla benzer çalışır. Queue'ye giren objeler 
                                                                 // [0]. eleman olur.

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools) // En başta pool türündeki size değişkenine verilen değer kadar, poolDictionari'ye obje atanır.
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position,Quaternion rotation) // Dequeue edilen objenin yerine, aynı türden olan ve en başta yaratılmış olan
                                                                                      // objeyi Enqueue eder.
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool'da bu key'e sahip bir obje yok");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }


}
