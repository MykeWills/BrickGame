using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem : MonoBehaviour
{
    bool reverseDir = false;
    public float ballSpeed;
    Vector3 dir;
    float maxY = 22;
    float minY = -6;    
    float maxX = 14.5f;
    float minX = -14.5f;
    float curAngle = 0;
    // Update is called once per frame
    private void Start()
    {
        dir = Vector3.up;
    }
    void Update()
    {
      
        if (transform.position.y < minY)
        {
            transform.position = OptimizeSystem.ChangeVector3(transform.position.x, minY, 0);
            ReverseRotation();
            SetDirection();
            //ball dies, if player is down to one ball player loses life.
            //transform.gameObject.SetActive(false);
        }
        else if (transform.position.y >= maxY)
        {
            transform.position = OptimizeSystem.ChangeVector3(transform.position.x, maxY, 0);
            ReverseRotation();
            SetDirection();
        }

        if (transform.position.x <= minX)
        {
            transform.position = OptimizeSystem.ChangeVector3(minX, transform.position.y, 0);
            ReverseRotation();
        }
        else if (transform.position.x >= maxX)
        {
            ReverseRotation();
            transform.position = OptimizeSystem.ChangeVector3(maxX, transform.position.y, 0);
        }
        if (PlayerController.isStarted)
        {
            if (reverseDir) dir = Vector3.down;
            else dir = Vector3.up;
            transform.Translate(dir * Time.deltaTime * ballSpeed);
        }

    }
    public void ReverseRotation()
    {
        float angle = transform.eulerAngles.z;
        curAngle = -angle;
        SetBallAngle(curAngle);
    }
    public void SetDirection()
    {
        reverseDir = !reverseDir;
    }
    public void SetBallAngle(float angle)
    {
        Vector3 vector = OptimizeSystem.ChangeVector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(vector);

    }
}
