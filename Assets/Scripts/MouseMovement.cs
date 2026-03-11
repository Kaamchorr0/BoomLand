using System;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float MouseSensitivity= 500f;
    float xRotation= 0f;
    float yRotation= 0f;
    public float TopClamp= -90f;
    public float BottomClamp= 90f;
    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX= Input.GetAxis("Mouse X")*MouseSensitivity*Time.deltaTime;
        float mouseY= Input.GetAxis("Mouse Y")*MouseSensitivity*Time.deltaTime;

        xRotation -= mouseY;
        xRotation= Mathf.Clamp(xRotation, TopClamp, BottomClamp);

        yRotation += mouseX;
        transform.localRotation= Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
