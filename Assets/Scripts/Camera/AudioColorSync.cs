using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(AudioListener))]
public class AudioColorSync : MonoBehaviour
{
    private Camera _camera;
    private AudioListener _listener;

    public void Start()
    {
        _camera = GetComponent<Camera>();
        _listener = GetComponent<AudioListener>();
    }

    public void Update()
    {
        float[] spectrum = new float[64];
        float[] samples = new float[256];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        AudioListener.GetOutputData(samples, 0);
        float h = 0;
        float s = 1f;
        float v = Mathf.Lerp(0.8f, 1f, samples[255]);
        float maxSpectrum = 0;
        for (int i = 0; i < 64; i++)
        {
            float spec = spectrum[i];
            if (spec > maxSpectrum)
            {
                maxSpectrum = spec;
                h = spec;
            }
        }
        float pastH;
        float pastV;
        Color.RGBToHSV(_camera.backgroundColor, out pastH, out _, out pastV);
        h = Mathf.Lerp(pastH, h, Time.deltaTime * 0.1f);
        //v = Mathf.Lerp(pastV, v, Time.deltaTime);
        _camera.backgroundColor = Color.HSVToRGB(h, s, v);
    }
}