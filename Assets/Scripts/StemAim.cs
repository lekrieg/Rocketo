// ////////////////////////
// File: StemAim.cs
// Created at: 08 24, 2023
// Description:
// 
// Modified by: Daniel Henrique
// 08 24, 2023
// ////////////////////////

using UnityEngine;

public class StemAim : MonoBehaviour
{
    [SerializeField] private Transform _rocketTransform;

    private bool isBalistic;

    private void Start()
    {
        EventManager.Instance.OnChangeLaunchType += ChangeLaunchType;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnChangeLaunchType -= ChangeLaunchType;
    }

    private void ChangeLaunchType()
    {
        isBalistic = !isBalistic;

        var zValue = isBalistic ? -35f : 0f;

        _rocketTransform.rotation = Quaternion.Euler(0f, 0f, zValue);
    }
}