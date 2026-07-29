using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumenControl : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Slider SliderVolumen;

    private void Start()
    {
        // Cargar el valor actual del slider al iniciar
        if (SliderVolumen != null)
        {
            SliderVolumen.onValueChanged.AddListener(SetMusicVolume);
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        // Evitamos Log10(0) que da error; si el slider está en 0, silenciamos del todo (-80 dB)
        if (sliderValue <= 0.0001f)
        {
            Mixer.SetFloat("DirectorGeneral", -80f);
        }
        else
        {
            // Convierte el rango [0.0001, 1] a decibelios [-80dB, 0dB]
            float dbValue = Mathf.Log10(sliderValue) * 20f;
            Mixer.SetFloat("DirectorGeneral", dbValue);
        }
    }
}
