using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        lookAt,
        lookAtInverted,
        cameraForward,
        cameraForwardInverted
    }

    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.lookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.lookAtInverted:
                Vector3 directionFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + directionFromCamera);
                break;
            case Mode.cameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.cameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
