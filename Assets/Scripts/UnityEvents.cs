using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//This script contains definitions for various unity events
public class GameObjectEvent : UnityEvent<GameObject> { }

public class IntEvent : UnityEvent<int> { }

public class FloatEvent : UnityEvent<float> { }

public class BoolEvent : UnityEvent<bool> { }


