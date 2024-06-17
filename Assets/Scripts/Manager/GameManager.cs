using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    void Awake()
    {
        gm = this;
    }
    
    //================================================

    public GameObject Help_instantiate(GameObject prefab,Transform parent)
    {
        if (!gm)
        {
            return null;            
        }
        
        return Instantiate(prefab,parent);
    }

    public GameObject Help_instantiate(GameObject prefab,Vector3 pos, Quaternion rot)
    {
        if (!gm)
        {
            return null;            
        }
        
        return Instantiate(prefab,pos,rot);
    }

    public void Help_destroy(GameObject gameObject, float delay=0f)
    {
        Destroy(gameObject,delay);
    }

}
