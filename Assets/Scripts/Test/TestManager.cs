using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject cube;
    
    private LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;  // 레이저 시작 너비
        lineRenderer.endWidth = 0.1f;    // 레이저 끝 너비
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
            // Vector3 mousePos = Input.mousePosition;

            // Ray ray = Camera.main.ScreenPointToRay(mousePos);
            // // Transform t1 = Instantiate(cube, mouseDir, Quaternion.identity).transform;
    
            // if(Physics.Raycast(ray,out RaycastHit hit))
            // {
            //     Vector3 mouseWorldPos = hit.point; // 타겟을 레이캐스트가 충돌된 곳으로 옮긴다.
            //     // mouseWorldPos.y = 1f;

            //     //Transform t2 = Instantiate(cube, mouseWorldPos, Quaternion.identity).transform;

                
            //     lineRenderer.startColor = Color.blue;
            //     lineRenderer.endColor = Color.blue;
                
            //     lineRenderer.SetPosition(0, ray.origin);
            //     lineRenderer.SetPosition(1, mouseWorldPos);

            // }
            // else
            // {
            //     lineRenderer.startColor = Color.white;
            //     lineRenderer.endColor = Color.white;
                
            //     lineRenderer.SetPosition(0, ray.origin);
            //     lineRenderer.SetPosition(1, ray.origin + ray.direction * 100f);  // 100 유닛 정도로 길게 그립니다.
            // }
                
        // }

         
    }



}
