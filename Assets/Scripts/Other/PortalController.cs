using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    public Animation anim;
    Rigidbody2D playerRb;
    
    AudioManager audioManager;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
            }
        }
    }
    IEnumerator PortalIn(){
        audioManager.PlaySFX(audioManager.portalIn);
        playerRb.simulated = false;
        anim.Play("Portal In");
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        player.transform.position = destination.transform.position;
        playerRb.velocity = Vector2.zero;
        anim.Play("Portal Out");
        yield return new WaitForSeconds(0.5f);
        audioManager.PlaySFX(audioManager.portalOut);
        yield return new WaitForSeconds(0.5f);
        playerRb.simulated = true;
    }


    IEnumerator MoveInPortal(){
        float timer = 0;
        while (timer < 0.5f){
            player.transform.position = Vector2.MoveTowards(player.transform.position,transform.position, 3*Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
