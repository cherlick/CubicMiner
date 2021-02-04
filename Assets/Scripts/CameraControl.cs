using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera _cam;

    private void Start() {
        _cam = Camera.main;
    }

    private void OnEnable() {
        StageTrigger.onStageTrigger += MoveCamera;
    }

    private void MoveCamera(float _yPosition)
    {
        transform.position = new Vector3(transform.position.x, _yPosition+3,transform.position.z);
    }
}
