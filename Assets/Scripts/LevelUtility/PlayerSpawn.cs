using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// spawns the player at its position when it becomes active
/// </summary>
public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;

    private void Awake()
    {
        Instantiate(PlayerPrefab, this.transform.position, this.transform.rotation);
    }
}
