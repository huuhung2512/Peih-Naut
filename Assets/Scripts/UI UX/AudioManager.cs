
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------Audio Source--------------")]
    [SerializeField] AudioSource musicScoure;
    [SerializeField] AudioSource SFXScoure;
    [Header("--------------Audio Clip--------------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip wallTouch;
    public AudioClip portalIn;
    public AudioClip portalOut;
    public AudioClip finish;
    private void Start() {
        musicScoure.clip = background;
        musicScoure.Play();
    }
    public void PlaySFX(AudioClip clip){
        SFXScoure.PlayOneShot(clip);
    }

}
