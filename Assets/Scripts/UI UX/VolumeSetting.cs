
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Image musicImg;
    [SerializeField] private Image sfxImg;

    [SerializeField] private Sprite onSpr;
    [SerializeField] private Sprite offSpr;


    bool isMusicStatus = true;
    bool isSfxStatus = true;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        isMusicStatus = volume > 0.001f;
        musicImg.sprite = isMusicStatus?onSpr:offSpr;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        //lưu giá trị volume của người dùng
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        isSfxStatus = volume > 0.001f;
        sfxImg.sprite = isSfxStatus?onSpr:offSpr;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        //lưu giá trị volume của người dùng
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }

    public void ButtonMusic(){
        isMusicStatus = !isMusicStatus;
        musicSlider.value = isMusicStatus ? 1f : 0f;
        musicImg.sprite = isMusicStatus?onSpr:offSpr;
    }
    public void ButtonSFX(){
        isSfxStatus =! isSfxStatus;
        SFXSlider.value = isSfxStatus ? 1f : 0f;
        sfxImg.sprite = isSfxStatus?onSpr:offSpr;
    }
}
