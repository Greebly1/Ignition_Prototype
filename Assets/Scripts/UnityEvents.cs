using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//This script contains definitions for various unity events
[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }
[System.Serializable]
public class IntEvent : UnityEvent<int> { }
[System.Serializable]
public class FloatEvent : UnityEvent<float> { }
[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }
[System.Serializable]
public class Vec3Event : UnityEvent<Vector3> { }

//Special event that can only be invoked between delays in time
[System.Serializable] 
public class WeaponEvent<T> : UnityEvent<T> {
    public float delay = 1f;


}

