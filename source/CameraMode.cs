using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMode : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Vector3 CameraPosition;

    void LateUpdate()
    {
        CameraPosition.x = Player.position.x;
        CameraPosition.y = Player.position.y;
        CameraPosition.z = -15f;
        if (CameraPosition.y >= 10f)
        {                      
            CameraPosition.y = 10f;
        }
        if (CameraPosition.y <= -13f)
        {
            CameraPosition.y = -13f;
        }
        if (CameraPosition.x <= -8.5f)
        {
            CameraPosition.x = -8.5f;
        }
        if (CameraPosition.x >= 9.5f)
        {
            CameraPosition.x = 9.5f;
        }
        transform.position = CameraPosition;
    }
}
