using UnityEngine;
using UnityEngine.Audio; // ← Para AudioMixer
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    private static AudioSettingsManager instance;

    void Awake()
    {
        // Verificamos si ya existe una instancia para evitar duplicados
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Esto hace que este objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruye la copia que se intenta crear en la nueva escena
            return;
        }
    }

    void Start()
    {
        // Cargar valores guardados
        float musicVol = PlayerPrefs.GetFloat("Musica", 0);
        float sfxVol = PlayerPrefs.GetFloat("SFX", 0);

        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("Musica", value);
        PlayerPrefs.SetFloat("Musica", value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX", value);
        PlayerPrefs.SetFloat("SFX", value);
    }
}
