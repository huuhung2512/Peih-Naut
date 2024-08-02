using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Quaternion startRotation;
    Rigidbody2D playerRb;
    [SerializeField] ParticlesController particlesController;
    [SerializeField] MovementController movementController;
    bool isDie;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        checkPointPos = transform.position;
        startRotation = transform.rotation;
        isDie = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle") && !isDie)
        {
            Die();
            Debug.Log("DIEEEEEEEEEE");
        }
    }

    public void UpdateCheckPoint(Vector2 pos){
        checkPointPos = pos;
    }
    void Die()
    {
        isDie = true;
        particlesController.die();
        StartCoroutine(Respawn(0.5f));
    }
    
    IEnumerator Respawn(float duration){
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0,0);
        playerRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(duration);
        particlesController.Stop();
        movementController.resetRelativeVector();
        transform.position = checkPointPos;
        transform.rotation = startRotation;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
        isDie = false;
    }
}
