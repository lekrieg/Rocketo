// ////////////////////////
// File: EventManager.cs
// Created at: 08 22, 2023
// Description:
// 
// Modified by: Daniel Henrique
// 08 24, 2023
// ////////////////////////

using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnChangeLaunchTypeHandler();

    public delegate void OnLandingImpulseHandler();

    public delegate void OnLaunchedHandler();

    public static EventManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public event OnLaunchedHandler OnLaunched;

    public void Launch()
    {
        OnLaunched?.Invoke();
    }

    public event OnLandingImpulseHandler OnLandingImpulse;

    public void LandingImpulse()
    {
        OnLandingImpulse?.Invoke();
    }

    public event OnChangeLaunchTypeHandler OnChangeLaunchType;

    public void ChangeLaunchType()
    {
        OnChangeLaunchType?.Invoke();
    }
}