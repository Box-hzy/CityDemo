using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManagement : MonoBehaviour
{
    public static WeatherManagement Instance;

    public enum weatherType
    {
        Sunny,
        Rainy,
        Foggy
    }

    public weatherType currentWeather;

    public ParticleSystem rain;
    public ParticleSystem fog;
    public Material sky;
    public Color sunnySkyColor;
    Color currentSkyColor;
    public Color rainySkyColor;
    public Color foggySkyColor;
    public Color cloudColor;
    public Vector2 durationRange = new Vector2(60, 180);
    [SerializeField] private float timer;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        timer = RandomTimer();
        /* sunnySkyColor = new Color(0, 0.45f, 0.7f, 0);
         rainySkyColor = new Color(0, 0.22f, 0.34f, 0);*/
        currentSkyColor = sky.GetColor("_Color1");
        changeWeather(RandomState());
    }


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            changeWeather(RandomState());
        }
    }



    weatherType RandomState()
    {
        int randomIndex = Random.Range(0, 3);
        weatherType weatherType = (weatherType)Random.Range(0, 3);
        return weatherType;
    }

    public void changeWeather(weatherType thisWeather)
    {

        OnExitState();
        currentWeather = thisWeather;
        OnEnterState();
    }


    IEnumerator LerpSkyColor(Color color)
    {
        while (currentSkyColor != color)
        {
            currentSkyColor = Color.Lerp(currentSkyColor, color, 0.1f);
            sky.SetColor("_Color1", currentSkyColor);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnExitState()
    {
        switch (currentWeather)
        {
            case weatherType.Sunny:
                break;
            case weatherType.Rainy:
                rain.Stop();
                break;
            case weatherType.Foggy:
                fog.Stop();
                break;
            default:
                break;
        }
    }

    float RandomTimer()
    {
        return Random.Range(durationRange.x, durationRange.y);
    }

    void OnEnterState()
    {
        timer = RandomTimer();

        switch (currentWeather)
        {
            case weatherType.Sunny:
                StartCoroutine(LerpSkyColor(sunnySkyColor));
                break;
            case weatherType.Rainy:
                rain.Play();
                StartCoroutine(LerpSkyColor(rainySkyColor));
                break;
            case weatherType.Foggy:
                fog.Play();
                StartCoroutine(LerpSkyColor(foggySkyColor));
                break;
            default:
                break;
        }
    }

    public weatherType getWeather()
    {
        return currentWeather;
    }
}
