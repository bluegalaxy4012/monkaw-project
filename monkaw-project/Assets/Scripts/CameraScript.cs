using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;
    Camera cam;
    float mouseX;
    float mouseY;

    float multiplier = 2.5f;
    float xRotation;
    float yRotation;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine)
            return;
     
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
     
        
    }
    private void Update()
    {
        if (!pv.IsMine)
            return;
        MyInput();
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }
void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}