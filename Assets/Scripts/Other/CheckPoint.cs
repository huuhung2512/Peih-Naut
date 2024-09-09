using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameController gameController;
    [SerializeField] Transform checkpointPos;
    [SerializeField] GameObject passCheckpoint;
    [SerializeField] GameObject activeCheckPoint;
    AudioManager audioManager;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameController.UpdateCheckPoint(checkpointPos.position);
            if (activeCheckPoint != null)
            {
                audioManager.PlaySFX(audioManager.checkpoint);
                activeCheckPoint.SetActive(true);
            }
            if (passCheckpoint != null)
            {
                passCheckpoint.SetActive(false);
            }
        }
    }
}
