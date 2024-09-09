using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Quaternion startRotation;
    Rigidbody2D playerRb;
    [SerializeField] ParticlesController particlesController;
    [SerializeField] MovementController movementController;
    
    CameraController cameraController;
    bool isDie;
    [SerializeField] int deadCount;
    [SerializeField] TextMeshProUGUI numberDead;

    void Awake()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        playerRb = GetComponent<Rigidbody2D>();
        deadCount = 0;
        numberDead.text = deadCount.ToString();
    }
    void Start()
    {
        checkPointPos = transform.position;
        startRotation = transform.rotation;
        isDie = false;
    }
    void Update(){
        numberDead.text = "Deaths: " + deadCount;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle") && !isDie)
        {
            Die();
            deadCount++;
        }
    }

    public void UpdateCheckPoint(Vector2 pos){
        checkPointPos = pos;
    }
    void Die()
    {
        isDie = true;
        cameraController.anim.Play("White Screen");
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
