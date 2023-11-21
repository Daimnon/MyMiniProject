using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform _mainCamTr;

    private void LateUpdate()
    {
        transform.LookAt(_mainCamTr.forward + transform.position);
    }
}
