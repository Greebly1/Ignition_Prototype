using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoundObjectPool : MonoBehaviour, SoundPlayer
{
    public GameObject SoundPrefab = null;

    public void PlaySound()
    {
        if (GameObjectPool.allPools.ContainsKey(SoundPrefab))
        {
            Debug.Log("baurger");
            GameObjectPool.allPools[SoundPrefab].PoolInstantiate(transform.position, transform.rotation);
        } else
        {
            Instantiate(SoundPrefab, transform.position, transform.rotation);
        }
    }
}
