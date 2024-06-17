using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player{get;private set;}

    void Awake()
    {
        player = this;
    }
}
