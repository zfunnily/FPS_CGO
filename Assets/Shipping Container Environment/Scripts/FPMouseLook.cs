using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMouseLook : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private Transform characterTransform;
    private Vector3 cameraRotation;

    public float MouseSensitivity;
    public Vector2 MaxMinAngle;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var tmp_MouseX = Input.GetAxis("Mouse X");
        var tmp_MouseY = Input.GetAxis("Mouse Y");

        cameraRotation.x -= tmp_MouseY * MouseSensitivity;//MouseSensitivity=18
        cameraRotation.y += tmp_MouseX * MouseSensitivity;//MouseSensitivity=18

        cameraRotation.x = Mathf.Clamp(cameraRotation.x, MaxMinAngle.x, MaxMinAngle.y);
        cameraTransform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);

        characterTransform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }
}
