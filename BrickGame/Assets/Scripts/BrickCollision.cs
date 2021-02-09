using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollision : MonoBehaviour
{
    BrickSystem brickSystem;
    public AudioClip hitFx;
    ParticleSystem ps;
    Collider m_Collider;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;
    Vector3 point;
    Vector3 ballPos;
    private void Start()
    {
        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider>();
        //Fetch the center of the Collider volume
        m_Center = m_Collider.bounds.center;
        //Fetch the size of the Collider volume
        m_Size = m_Collider.bounds.size;
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;
        //Output this data into the console
        //OutputData();
    }
    void OutputData()
    {
        //Output to the console the center and size of the Collider volume
        Debug.Log("Collider Center : " + m_Center);
        Debug.Log("Collider Size : " + m_Size);
        Debug.Log("Collider bound Minimum : " + m_Min);
        Debug.Log("Collider bound Maximum : " + m_Max);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            BallSystem ballSystem = collision.transform.GetComponent<BallSystem>();
           
            for (int c = 0; c < collision.contacts.Length; c++)
            {
                ContactPoint contact = collision.GetContact(c);
                point = contact.point;
                ballPos = point;
                //left right up down
                if (collision.transform.position.y < m_Max.y)
                    collision.transform.position = ballPos;
                else if (collision.transform.position.y > m_Min.y)
                    collision.transform.position = ballPos;
                else if (collision.transform.position.x > m_Min.x)
                    collision.transform.position = ballPos;
                else if (collision.transform.position.x > m_Min.x)
                    collision.transform.position = ballPos;
                if (point.x < (m_Min.x + 0.1f) && point.y > m_Min.y && point.y < m_Max.y) ballSystem.ReverseRotation();
                else if (point.x > (m_Max.x - 0.1f) && point.y > m_Min.y && point.y < m_Max.y) ballSystem.ReverseRotation();
                else if (point.y > (m_Max.y - 0.1f) && point.x > m_Min.x && point.x < m_Max.x) { ballSystem.ReverseRotation(); ballSystem.SetDirection(); }
                else if (point.y < (m_Min.y + 0.1f) && point.x > m_Min.x && point.x < m_Max.x) { ballSystem.ReverseRotation(); ballSystem.SetDirection(); }
                // topL botL topR botR with Slack
                else if (point.y > (m_Max.y - 0.1f) && point.x < (m_Min.x + 0.1f)) ballSystem.ReverseRotation();
                else if (point.y < (m_Min.y + 0.1f) && point.x < (m_Min.x + 0.1f)) ballSystem.ReverseRotation();
                else if (point.y > (m_Max.y - 0.1f) && point.x > (m_Max.x - 0.1f)) ballSystem.ReverseRotation();
                else if (point.y < (m_Min.y + 0.1f) && point.x > (m_Max.x - 0.1f)) ballSystem.ReverseRotation();
                // topL botL topR botR with no Slack
                else if (point.y == m_Min.y && point.x == m_Min.x) ballSystem.ReverseRotation();
                else if (point.y == m_Min.y && point.x == m_Max.x) ballSystem.ReverseRotation();
                else if (point.y == m_Max.y && point.x == m_Max.x) ballSystem.ReverseRotation();
                else if (point.y == m_Max.y && point.x == m_Min.x) ballSystem.ReverseRotation();
             

            }
            ps = transform.parent.GetComponent<ParticleSystem>();
            ps.Play();
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            brickSystem = transform.parent.GetComponent<BrickSystem>();
            brickSystem.DamageBrick();
        }
    }


}
