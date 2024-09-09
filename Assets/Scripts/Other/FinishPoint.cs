
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
  AudioManager audioManager;
  
  private void Awake() {
    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      audioManager.PlaySFX(audioManager.finish);
      UnlockNewLevel();
      //chuyển sang level tiếp theo
      SceneController.instance.SetLevel("Level " + (SceneManager.GetActiveScene().buildIndex + 1));
    }
  }

  void UnlockNewLevel()
  {
    if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
    {
      PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
      PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
      PlayerPrefs.Save();
    }
  }
}
