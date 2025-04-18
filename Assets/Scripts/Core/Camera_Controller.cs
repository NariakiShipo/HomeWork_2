using System;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float normalSpeed = 10f;
    [SerializeField] private float UpSpeed = 25f;

    private bool IsLooking = true;
    private Transform target;

    public GameObject[] Cams;
    public GameObject[] ZoomoutCams;

    private Dictionary<KeyCode, Transform> camPositions;
    private Dictionary<KeyCode, Transform> zoomOutPositions;

    void Start()
    {
        //Automatically set up the key codes for the maincameras and zoomed out cameras
        camPositions = new Dictionary<KeyCode, Transform>();
        SettingKeycodes(camPositions, Cams, KeyCode.Alpha1);

        zoomOutPositions = new Dictionary<KeyCode, Transform>();
        SettingKeycodes(zoomOutPositions, ZoomoutCams, KeyCode.Keypad1);
    }

    void Update()
    {
        HandleCameraControls();
    }

    private void HandleCameraControls()
    {
        MoveDirection();
        RotateDirection();
        ResetPos();
        SpeedUp();
        ChangeDifferenceCamera();
        ZoomOut();
    }

    private void SettingKeycodes(Dictionary<KeyCode, Transform> dictionary,GameObject[] gameObject,KeyCode StartkeyCode)
    {
        for (int i = 0; i < gameObject.Length; i++)
        {
            dictionary.Add((KeyCode)((int)StartkeyCode + i), gameObject[i].transform);
        }
    }
    //Use the arrow keys for movement and rotation
    private void MoveDirection()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A, D
        float vertical = Input.GetAxis("Vertical");     // W, S
        float upDown = (Input.GetKey(KeyCode.R) ? 1 : 0) - (Input.GetKey(KeyCode.F) ? 1 : 0);

        Vector3 move = new Vector3(horizontal, upDown, vertical).normalized;
        transform.Translate(move * Speed * Time.deltaTime, Space.Self);
    }
    //Use Q, E for rotation and Z, C for up and down rotation
    private void RotateDirection()
    {
        float rotateHorizontal = (Input.GetKey(KeyCode.Q) ? -1 : 0) + (Input.GetKey(KeyCode.E) ? 1 : 0);
        float rotateVertical = (Input.GetKey(KeyCode.Z) ? 1 : 0) + (Input.GetKey(KeyCode.C) ? -1 : 0);

        transform.Rotate(Vector3.up * rotateHorizontal * Speed * 10 * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotateVertical * Speed * 10 * Time.deltaTime, Space.Self);
    }
    //Use X to reset the camera position and rotation
    private void ResetPos()
    {
        if (Input.GetKey(KeyCode.X))
        {
            transform.position = new Vector3(-2, 1, -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            IsLooking = false;
        }
    }
    //Use V to speed up the camera movement
    private void SpeedUp()
    {
        Speed = Input.GetKey(KeyCode.V) ? UpSpeed : normalSpeed;
    }
    //Use 1~9 for the first three cameras and T to look at the target
    private void ChangeDifferenceCamera()
    {
        foreach (var cam in camPositions)
        {
            if (Input.GetKey(cam.Key))
            {
                transform.position = cam.Value.position;
                transform.rotation = cam.Value.rotation;
                target = cam.Value;
                IsLooking = true;
            }
        }

        if (Input.GetKey(KeyCode.T) && IsLooking)
        {
            transform.LookAt(target);
        }
    }
    //Use numpad 1~9 for the zoomed out cameras
    private void ZoomOut()
    {
        foreach (var cam in zoomOutPositions)
        {
            if (Input.GetKey(cam.Key))
            {
                transform.position = cam.Value.position;
                transform.rotation = cam.Value.rotation;
            }
        }
    }
}