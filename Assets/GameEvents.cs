using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    // public static GameEvents events;

    public static UnityEvent<bool> onPlayerAim;
    
    void Awake()
    {
        onPlayerAim = new();
    }

}
