using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollision : MonoBehaviour
{
    BrickSystem brickSystem;
    public AudioClip hitFx;
    ParticleSystem ps;
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball" && !BallSystem.ballHit)
        {
            ps = transform.parent.GetComponent<ParticleSystem>();
            ps.Play();
            BallSystem.ballHit = true;
            AudioSystem.PlayAudioSource(hitFx, 1, 1);
            brickSystem = transform.parent.GetComponent<BrickSystem>();
            brickSystem.DamageBrick();
            BallSystem ballSystem = collision.transform.GetComponent<BallSystem>();
            ballSystem.ReverseRotation();
            ballSystem.SetDirection();
            PlayerController p = PlayerController.player.GetComponent<PlayerController>();
            p.AddScore(235);

        }
    }
    private void LateUpdate()
    {
        BallSystem.ballHit = false;
    }
}
