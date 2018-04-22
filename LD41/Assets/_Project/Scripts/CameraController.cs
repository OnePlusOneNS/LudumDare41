using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    [SerializeField]
    private float _mouseSensitivity = 10f;

    float _rotationY = 0f;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate ()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _mouseSensitivity;

            _rotationY = Mathf.Clamp (_rotationY + Input.GetAxis("Mouse Y") * _mouseSensitivity, -70, 70);

            transform.localEulerAngles = new Vector3(-_rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * _mouseSensitivity, 0);
        }
        else
        {
            _rotationY = Mathf.Clamp (_rotationY + Input.GetAxis("Mouse Y") * _mouseSensitivity, -360, 360);

            transform.localEulerAngles = new Vector3(-_rotationY, transform.localEulerAngles.y, 0);
        }
    }
}
