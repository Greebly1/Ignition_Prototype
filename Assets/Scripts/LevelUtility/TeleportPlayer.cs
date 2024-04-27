using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject emptyTeleporterTarget; //empty object to teleport to

    public void Teleport()
    {//probably the player
       
        GameManager.Instance.CurrentLevel.Player.transform.position = emptyTeleporterTarget.transform.position;
    }
}
