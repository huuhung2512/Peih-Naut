using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [Header("Movement Particle")]
    [SerializeField] ParticleSystem movementParticles;
    [Range(0, 10)]
    [SerializeField] int occurAffterVelocity;
    [Range(0, 0.2f)]
    [SerializeField] float dustFomationPeroid;
    [SerializeField] Rigidbody2D playerRb;
    float counter;
    public bool isOnGround;
    [Header("")]
    [SerializeField] ParticleSystem fallParticles;
    [SerializeField] ParticleSystem touchParticles;
    [SerializeField] ParticleSystem dieParticles;
    void Start()
    {
        touchParticles.transform.parent = null;
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAffterVelocity)
        {
            if (counter > dustFomationPeroid)
            {
                movementParticles.Play();
                counter = 0;
            }
        }
    }
    public void PlayTouchParticles(Vector2 pos)
    {
        touchParticles.transform.position = pos;
        touchParticles.Play();
    }
    public void die(){
        dieParticles.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticles.Play();
            isOnGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    internal void Stop()
    {
        dieParticles.Clear();
    }
}
