using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float lerpTime = 0.1f, cameraDeadzone;

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
        if (!playerMovement.onGround) 
            nextPosition = CalculateNextPosition(offset);
        else
            nextPosition = CameraSnapToPlatform();


        camera.transform.position = Vector3.Lerp(camera.transform.position, nextPosition, lerpTime);
    }

    Vector3 CalculateNextPosition(Vector3 offset)
    {
        Vector3 playerPosition = player.transform.position;
        float tempX = playerPosition.x;
        float tempY = camera.transform.position.y;
        if (Mathf.Abs(tempY - playerPosition.y) > cameraDeadzone + offset.y)
        {
            if (tempY > playerPosition.y)
                tempY = (playerPosition.y + cameraDeadzone);
            else
                tempY = (playerPosition.y - cameraDeadzone);
        }

        return new Vector3(tempX, tempY, 0) + offset;

    }

    Vector3 CameraSnapToPlatform()
    {
        Vector3 playerPosition = player.transform.position;
        return playerPosition + offset;
    }
}
