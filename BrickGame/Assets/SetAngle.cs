using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAngle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ball" && PlayerController.isStarted)
        {
            ContactPoint contact = collision.GetContact(0);
            MeshCollider meshCollider = transform.GetComponent<MeshCollider>();
            Vector3 point = contact.point;
            Vector3 collider = meshCollider.bounds.center;
            float dist = (collider.x - point.x);
            float angle = (dist / 0.25f) * 9.0f;
            BallSystem ballSystem = collision.transform.GetComponent<BallSystem>();
            ballSystem.SetBallAngle(angle);
            ballSystem.SetDirection();
        }
    }
}
