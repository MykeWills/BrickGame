using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem : MonoBehaviour
{
    public AudioClip hitFx;
    public AudioClip brickHitFx;
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
    //Quaternion fixedRotation;
    Transform ballParent;
    //Collider m_Collider;
    //Vector3 m_Center;
    //Vector3 m_Size, m_Min, m_Max;
    // Update is called once per frame
    private void Start()
    {
        ballParent = transform;
        ////Fetch the Collider from the GameObject
        //m_Collider = GetComponent<Collider>();
        ////Fetch the center of the Collider volume
        //m_Center = m_Collider.bounds.center;
        ////Fetch the size of the Collider volume
        //m_Size = m_Collider.bounds.size;
        ////Fetch the minimum and maximum bounds of the Collider volume
        //m_Min = m_Collider.bounds.min;
        //m_Max = m_Collider.bounds.max;
        //Output this data into the console
        //OutputData();
        //fixedRotation = transform.rotation;
        dir = Vector3.up;
    }
    void Update()
    {
        //transform.rotation = fixedRotation;
        if (PlayerController.isStarted)
        {
            if (reverseDir) dir = Vector3.down;
            else dir = Vector3.up;
            ballParent.Translate(dir * Time.deltaTime * ballSpeed);
        }
        if (ballParent.position.y < minY)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            ballParent.position = optSystem.Vector3(ballParent.position.x, minY, 0);
            ReverseRotation();
            SetDirection();
            //ball dies, if player is down to one ball player loses life.
            //transform.gameObject.SetActive(false);
        }
        else if (ballParent.position.y >= maxY)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            ballParent.position = optSystem.Vector3(ballParent.position.x, maxY, 0);
            ReverseRotation();
            SetDirection();
        }

        if (ballParent.position.x <= minX)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            ballParent.position = optSystem.Vector3(minX, ballParent.position.y, 0);
            ReverseRotation();
        }
        else if (ballParent.position.x >= maxX)
        {
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            ReverseRotation();
            ballParent.position = optSystem.Vector3(maxX, ballParent.position.y, 0);
        }
      
    }

    public void ReverseRotation()
    {
        float angle = ballParent.eulerAngles.z;
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
        ballParent.rotation = Quaternion.Euler(vector);

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Brick")
    //    {
    //        BrickSystem brickSystem = collision.transform.parent.GetComponent<BrickSystem>();
    //        ContactPoint contact = collision.GetContact(0);
    //        Vector3 point = contact.point;

    //        //left right up down
    //        if (point.x == m_Min.x  && point.y > m_Min.y && point.y < m_Max.y) ReverseRotation();
    //        if (point.x == m_Max.x && point.y > m_Min.y && point.y < m_Max.y) ReverseRotation();
    //        if (point.y == m_Max.y && point.x > m_Min.x && point.x < m_Max.x) { ReverseRotation(); SetDirection(); }
    //        if (point.y == m_Min.y && point.x > m_Min.x && point.x < m_Max.x) { ReverseRotation(); SetDirection(); }
    //        // topL botL topR botR with Slack
    //        if (point.y > (m_Max.y - 0.1f) && point.x < (m_Min.x + 0.1f)) ReverseRotation();
    //        if (point.y < (m_Min.y + 0.1f) && point.x < (m_Min.x + 0.1f)) ReverseRotation();
    //        if (point.y > (m_Max.y - 0.1f) && point.x > (m_Max.x - 0.1f)) ReverseRotation();
    //        if (point.y < (m_Min.y + 0.1f) && point.x > (m_Max.x - 0.1f)) ReverseRotation();
    //        // topL botL topR botR with no Slack
    //        if (point.y == m_Min.y && point.x == m_Min.x) SetDirection();
    //        if (point.y == m_Min.y && point.x == m_Max.x) SetDirection();
    //        if (point.y == m_Max.y && point.x == m_Max.x) SetDirection();
    //        if (point.y == m_Max.y && point.x == m_Min.x) SetDirection();
    //        ParticleSystem ps = collision.gameObject.transform.parent.GetComponent<ParticleSystem>();
    //        ps.Play();
    //        AudioSystem.PlayAudioSource(brickHitFx, 1, 1);
    //        brickSystem.DamageBrick();
    //    }
    //}
}
