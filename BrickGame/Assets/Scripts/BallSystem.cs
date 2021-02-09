using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem : MonoBehaviour
{
    public AudioClip hitFx;
    OptSystem optSystem = new OptSystem();
    [HideInInspector]
    public bool reverseDir = false;
    public float ballSpeed;
    public Vector3 dir;
    float maxY = 22;
    float minY = -10;    
    float maxX = 14.5f;
    float minX = -14.5f;
    public float curAngle = 0;
    public static bool ballHit = false;
    // Update is called once per frame
    private void Start()
    {
        dir = Vector3.up;
    }
    void Update()
    {
      
        if (transform.position.y < minY)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            transform.position = optSystem.Vector3(transform.position.x, minY, 0);
            ReverseRotation();
            SetDirection();
            //ball dies, if player is down to one ball player loses life.
            //transform.gameObject.SetActive(false);
        }
        else if (transform.position.y >= maxY)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            transform.position = optSystem.Vector3(transform.position.x, maxY, 0);
            ReverseRotation();
            SetDirection();
        }

        if (transform.position.x <= minX)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            transform.position = optSystem.Vector3(minX, transform.position.y, 0);
            ReverseRotation();
        }
        else if (transform.position.x >= maxX)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            ReverseRotation();
            transform.position = optSystem.Vector3(maxX, transform.position.y, 0);
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
        Vector3 vector = optSystem.Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(vector);

    }
}
