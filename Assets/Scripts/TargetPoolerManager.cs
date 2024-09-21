using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoolerManager : MonoBehaviour
{
    [System.Serializable]
    public class TargetPools
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    
    public static TargetPoolerManager Instance { get; private set; }


    [SerializeField] private List<TargetPools> targetPools;
    [SerializeField] private Dictionary<string, Queue<GameObject>> targetPoolDictionary;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        targetPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        InitializePool();

    }

    public void InitializePool()
    {
        foreach (TargetPools targetPool in targetPools)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();
            for (int i = 0; i < targetPool.size; i++)
            {
                GameObject obj = Instantiate(targetPool.prefab);
                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }

            // targetPoolDictionary[targetPool.tag] = poolQueue;
            targetPoolDictionary.Add(targetPool.tag, poolQueue);
        }
    }

    public GameObject GetPooledTarget(string targetTag, Vector2 position)
    {
        if (!targetPoolDictionary.ContainsKey(targetTag))
        {
            Debug.LogWarning("Game Object with tag " + targetTag + " does not exist!!");
            return null;
        }
        if (targetPoolDictionary[targetTag] == null || targetPoolDictionary[targetTag].Count == 0)
        {
            InitializePool();
        }
        GameObject targetToSpawn = targetPoolDictionary[targetTag].Dequeue();

        targetToSpawn.SetActive(true);
        targetToSpawn.transform.position = position;

        IPooledTarget pooledTarget = targetToSpawn.GetComponent<IPooledTarget>();
        if (pooledTarget != null)
        {
            pooledTarget.OnSpawnTarget();
        }

        return targetToSpawn;
    }

    public void ReturnPooledTarget(GameObject target)
    {
        target.SetActive(false);
        if (target == null)
        {
            return;
        }
        targetPoolDictionary[target.tag].Enqueue(target);
    }

}
