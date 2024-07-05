using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

namespace Study3D
{
public class TPSController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;

    [SerializeField] StarterAssetsInputs starterAssetsInputs;

    [SerializeField] float sensitivity_onNormal;
    [SerializeField] float sensitivity_onAiming;


    // private const float _threshold = 0.01f;


    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (starterAssetsInputs.aim)
        {
            Aim();
        }
        else
        {
            UnAim();
        }
    }


    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;



    public void Aim()
    {
        aimVirtualCamera.gameObject.SetActive(true);
        
    }

    public void UnAim()
    {
        aimVirtualCamera.gameObject.SetActive(false);
    }

}

}
