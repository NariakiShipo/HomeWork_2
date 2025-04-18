using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_controller : MonoBehaviour
{
    public Light redLight;
    public Light greenLight;
    public Light blueLight;

    public float rotationSpeed = 50f;       // rotation speed of the lights
    public Transform rotationCenter;        // rotation center point
    public float rotationRadius = 5f;       // rotation radius of the lights

    private float redAngle = 0f;            // red light initial angle offset
    private float greenAngle = 120f;        // green light initial angle offset
    private float blueAngle = 240f;         // blue light initial angle offset

    // Update is called once per frame
    void Update()
    {
        // Control the rotation of the lights
        RotateLights();

        // Control the lights with keyboard input
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            redLight.enabled = !redLight.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            greenLight.enabled = !greenLight.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            blueLight.enabled = !blueLight.enabled;
        }
    }

    private void RotateLights()
    {
        // calculate the position of the red light
        redAngle += rotationSpeed * Time.deltaTime;
        float redX = rotationCenter.position.x + Mathf.Cos(redAngle * Mathf.Deg2Rad) * rotationRadius;
        float redZ = rotationCenter.position.z + Mathf.Sin(redAngle * Mathf.Deg2Rad) * rotationRadius;
        redLight.transform.position = new Vector3(redX, redLight.transform.position.y, redZ);

        // calculate the position of the green light
        greenAngle += rotationSpeed * Time.deltaTime;
        float greenX = rotationCenter.position.x + Mathf.Cos(greenAngle * Mathf.Deg2Rad) * rotationRadius;
        float greenZ = rotationCenter.position.z + Mathf.Sin(greenAngle * Mathf.Deg2Rad) * rotationRadius;
        greenLight.transform.position = new Vector3(greenX, greenLight.transform.position.y, greenZ);

        // calculate the position of the blue light
        blueAngle += rotationSpeed * Time.deltaTime;
        float blueX = rotationCenter.position.x + Mathf.Cos(blueAngle * Mathf.Deg2Rad) * rotationRadius;
        float blueZ = rotationCenter.position.z + Mathf.Sin(blueAngle * Mathf.Deg2Rad) * rotationRadius;
        blueLight.transform.position = new Vector3(blueX, blueLight.transform.position.y, blueZ);
    }
}