using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLigthController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField]private float TimeOfDay = 0f;
    [SerializeField]private float DayDuration = 30f;
    [SerializeField]private AnimationCurve GlobalLigthIntensity;

    private Light2D GlobalLight;

    public void Start()
    {
        GlobalLight = GetComponent<Light2D>();
    }

    public void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;
        if(TimeOfDay >= 1) TimeOfDay -= 1;

        GlobalLight.intensity = GlobalLigthIntensity.Evaluate(TimeOfDay);
    }
}
