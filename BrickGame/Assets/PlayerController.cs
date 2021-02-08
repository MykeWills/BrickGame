using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool isStarted = false;
    public float acceleration;
    public float moveSpeed = 5;
    public bool isBoosting = true;
    public Transform ballPool;
    float maxX = 13.3f;
    float minX = -13.3f;    
    float maxY = -3.5f;
    float minY = -4.5f;
    Vector3 returnPos = new Vector3(0, -4, 0);
    Vector2 velocity;
    private void Start()
    {
        OptimizeSystem.GameMouseActive(false, CursorLockMode.Locked);
        SetupPaddle();
    }

    void Update()
    {
        Move();
        if (OptimizeSystem.playerController.GetButton("Boost")) isBoosting = true;
        else if (OptimizeSystem.playerController.GetButtonUp("Boost")) isBoosting = false;
        if (OptimizeSystem.playerController.GetButtonDown("Start") && !isStarted) StartPaddle();
        if (OptimizeSystem.playerController.GetButtonDown("A") && isStarted) SetupPaddle();

    }
    void Move()
    {
        float inputX = OptimizeSystem.playerController.GetAxisRaw("Horizontal");
        float inputY = OptimizeSystem.playerController.GetAxisRaw("Vertical");
        transform.Translate(Time.deltaTime * velocity);
        if(inputX != 0)
        {
            if (isBoosting)
                velocity.x = Mathf.MoveTowards(velocity.x, (moveSpeed * 1.5f) * inputX, acceleration);
            else
                velocity.x = Mathf.MoveTowards(velocity.x, moveSpeed * inputX, acceleration);
        }
        else if(inputX == 0)
            velocity.x = 0;

        if (inputY != 0)
        {
            if (isBoosting)
                velocity.y = Mathf.MoveTowards(velocity.y, (moveSpeed * 1.5f) * inputY, acceleration);
            else
                velocity.y = Mathf.MoveTowards(velocity.y, moveSpeed * inputY, acceleration);
        }
        else if (inputY == 0)
            velocity.y = 0;

        if (transform.localPosition.y < minY)
            transform.localPosition = OptimizeSystem.ChangeVector3(transform.localPosition.x, minY, transform.localPosition.z);
        else if (transform.localPosition.y > maxY)
            transform.localPosition = OptimizeSystem.ChangeVector3(transform.localPosition.x, maxY, transform.localPosition.z);
        if (transform.localPosition.x < minX)
            transform.localPosition = OptimizeSystem.ChangeVector3(minX, transform.localPosition.y, transform.localPosition.z);
        else if (transform.localPosition.x > maxX)
            transform.localPosition = OptimizeSystem.ChangeVector3(maxX, transform.localPosition.y, transform.localPosition.z);





    }
    public void StartPaddle()
    {
        transform.GetChild(1).transform.SetParent(ballPool);
        isStarted = true;
        
    }
    public void SetupPaddle()
    {
        isStarted = false;
        transform.localPosition = returnPos;
        if (ballPool.transform.childCount > 0)
        {
            ballPool.transform.GetChild(0).transform.SetParent(transform);
            transform.GetChild(1).transform.localPosition = OptimizeSystem.ChangeVector3(0, 0.5f, 0);
            transform.GetChild(1).transform.localRotation = Quaternion.identity;
        }
    }

}
