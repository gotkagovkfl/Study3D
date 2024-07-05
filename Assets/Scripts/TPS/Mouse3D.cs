using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse3D : MonoBehaviour
{
    public static Mouse3D mouse {get; private set;}
    
    [SerializeField] LayerMask mouseColliderLayerMask = new();
    
    void Awake()
    {
        mouse = this;
    }

    // Update is called once per frame
    void Update()
    {  
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
       {
            transform.position = raycastHit.point;
       } 
    }

    //
    public static Vector3 GetMouseWorldPosition()
    {
        if (!mouse)
        {
            Debug.LogError("마우스3D 오브젝트가 존재하지 않음!");
        }
        return mouse.GetMouseWorldPosition_instance();
    }

    Vector3 GetMouseWorldPosition_instance()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width *0.5f, Screen.height * 0.5f);
        
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        } 
    }
}
