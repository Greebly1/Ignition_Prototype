using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefabTemplate;
    List<GameObject> pool = new List<GameObject>();
    public int maxObjects = 10;

    public static Dictionary<GameObject, GameObjectPool> allPools = new Dictionary<GameObject, GameObjectPool>();
    public static List<GameObjectPool> pools = new List<GameObjectPool>();

    bool isLoadingObjects = false;
    Coroutine loadingCoroutine = null;

    #region Pool functions
    public GameObject firstAvailableObject {
        get
        {
            try
            {
                return pool.First(obj => !obj.activeInHierarchy);
            } catch
            {
                Debug.LogWarning(this.name.ToString() + " gameobject pool does not have enough instances to fulfill requirements");
            }
            return pool[0];
            
        }
    } 
    bool poolEmpty
    {
        get
        {
            foreach (GameObject obj in pool)
            {
                if (obj.activeInHierarchy) { return false; }
            }
            return true;
        }
    }
    public GameObject PoolInstantiate(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (poolEmpty) { /* TODO: make functionality for increasing max capacity*/ }

        GameObject chosenObject = firstAvailableObject;
        Debug.Log(chosenObject);
        chosenObject.SetActive(true);
        chosenObject.transform.position = spawnPosition;
        chosenObject.transform.rotation = spawnRotation;
        return chosenObject;
    }
    #endregion


    #region Monobehavior Callbacks
    private void Awake()
    {
        //TODO: add a check that deletes this gameobject if the prefab template itself contains an object pool
        if (allPools.TryAdd(prefabTemplate, this))
        {
            pools.Add(this);
        } else
        {
            Debug.LogError("An object pool tried to duplicate an existing object pool");
        }
    }

    private void Start()
    {
        if (isLoadingObjects == false) { loadingCoroutine = StartCoroutine("FillPool"); }
    }

    private void OnDestroy()
    {
        allPools.Remove(prefabTemplate);
    }
    #endregion

    //Instantiates 1 object each frame until the pool is filled
    IEnumerator FillPool()
    {
        isLoadingObjects = true;

        while (pool.Count < maxObjects)
        {
            pool.Add(
                Instantiate(prefabTemplate, this.transform)
                );
            pool.Last().SetActive(false);

            yield return null;
        }

        isLoadingObjects = false;
    }


    /// <summary>
    /// This does not work
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>meant to return if a given object is part of an object pool</returns>
    public static bool IsInPool(GameObject obj)
    {
        foreach (GameObjectPool currentObjectPool in pools)
        {
            foreach(GameObject currentPoolObject in currentObjectPool.pool)
            {
                if(currentPoolObject == obj)
                {
                    return true; //early out
                }
            }
        }

        return false;
    }
}
