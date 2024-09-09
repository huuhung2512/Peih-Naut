using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] AnimationEvent transitionAnim;
    public string sceneName = "";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start() 
    {
       transitionAnim.endTransitionAction += LoadLevel;
    }
    public void SetLevel(string level)
    {
        sceneName = level;
        transitionAnim.PlayAnimTransition();
    }
    private void LoadLevel()
    {
        if(!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
