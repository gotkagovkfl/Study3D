using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study3D
{
    public class Player : MonoBehaviour
    {
        public static Player player{get;private set;}

        void Awake()
        {
            player = this;
        }
}
}

