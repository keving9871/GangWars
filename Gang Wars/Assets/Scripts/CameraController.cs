using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //This camera
    public Camera mainCamera;

    //Player
    public Transform player;

    //Distance we want between the player and camera
    public float distance = 5.0f;

    //x and y position of camera
    float x = 0.0f;
    float y = 0.0f;

    //x and y side speed, how fast your camera moves in x way and in y way
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    //Minium and maximum distance between player and camera
    public float distanceMin = 0.5f;
    public float distanceMax = 15f;

    //Stores the camera's current distance from player
    private float curDist = 0;

    private void Start()
    {
        //Make cursor invisible
        Cursor.visible = false;

        //Make variable from our euler angles
        Vector3 angles = transform.eulerAngles;

        //and store y and x angles to different values
        x = angles.y;
        y = angles.x;

        //Set this camera to main camera
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        //Gets mouse movement x and y and multiplies them with speeds and moves camera with them
        x += Input.GetAxis("Horizontal") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Horizontal") * ySpeed * 0.02f;

        //Set rotation
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        //Changes distance between max and min distancy by mouse scroll
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        //Negative distance of camera
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);

        //Cameras postion
        Vector3 position = rotation * negDistance + player.position;

        //rotation and position of our camera to different variables
        transform.rotation = rotation;
        transform.position = position;

        //Cameras x rotation
        float cameraX = transform.rotation.x;

        //Store raycast hit
        RaycastHit hit;
        //If camera detects something behind or under it move camera to hitpoint so it doesn't go throught wall/floor
        if (Physics.Raycast(player.position, (transform.position - player.position).normalized, out hit, (distance <= 0 ? -distance : distance)))
        {
            transform.position = hit.point;
        }
    }
}