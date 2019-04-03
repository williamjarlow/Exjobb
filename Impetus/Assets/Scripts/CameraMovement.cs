using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float groundedLerpTimeX = 0.1f, groundedLerpTimeY = 0.1f, aerialLerpTimeX = 0.1f, aerialLerpTimeY = 0.1f, cameraDeadzone;
    [SerializeField]
    Vector2 xBoundary, yBoundary;

    [SerializeField]
    Vector3 offset;

    GameObject player;
    Movement playerMovement;
    Camera camera;
    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponentInChildren<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (playerMovement.onGround)
        {
            nextPosition = CalculateNextAerial();
            Vector3 pass1 = Vector3.Lerp(new Vector3(camera.transform.position.x, 0), new Vector3(nextPosition.x, 0), groundedLerpTimeX);
            Vector3 pass2 = Vector3.Lerp(new Vector3(0, camera.transform.position.y), new Vector3(0, nextPosition.y), groundedLerpTimeY);
            if (pass1.x < xBoundary.x || pass1.x > xBoundary.y)
                pass1 = camera.transform.position;
            if (pass2.y < yBoundary.x || pass2.y > yBoundary.y)
                pass2 = camera.transform.position;
            camera.transform.position = new Vector3(pass1.x, pass2.y, nextPosition.z);
        }
        else
        {
            nextPosition = CalculateNextAerial();
            Vector3 pass1 = Vector3.Lerp(new Vector3(camera.transform.position.x, 0), new Vector3(nextPosition.x, 0), aerialLerpTimeX);
            Vector3 pass2 = Vector3.Lerp(new Vector3(0, camera.transform.position.y), new Vector3(0, nextPosition.y), aerialLerpTimeY);
            if (pass1.x < xBoundary.x || pass1.x > xBoundary.y)
                pass1 = camera.transform.position;
            if (pass2.y < yBoundary.x || pass2.y > yBoundary.y)
                pass2 = camera.transform.position;
            camera.transform.position = new Vector3(pass1.x, pass2.y, nextPosition.z);
        }


        
    }

    Vector3 CalculateNextAerial()
    {
        Vector3 playerPosition = player.transform.position;
        float tempX = playerPosition.x;
        float tempY = camera.transform.position.y;
        if (Mathf.Abs(tempY - playerPosition.y) > cameraDeadzone)
        {
            if (tempY > playerPosition.y)
                tempY = (playerPosition.y + cameraDeadzone);
            else
                tempY = (playerPosition.y - cameraDeadzone);
        }

        return new Vector3(tempX, tempY, offset.z);

    }

    Vector3 CalculateNextGrounded()
    {
        Vector3 playerPosition = player.transform.position;
        return playerPosition + offset;
    }
}
