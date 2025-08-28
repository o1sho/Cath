using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Light2D light2d;

    public static WeatherManager Instance { get; private set; }


    private void Awake() {
        if (Instance == null) Instance = this;    
    }

    public void SetStorm(bool v) {
        if (v) light2d.intensity = 0.2f;
    }
}
