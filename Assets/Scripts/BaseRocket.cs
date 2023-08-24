// ////////////////////////
// File: BaseRocket.cs
// Created at: 08 22, 2023
// Description:
// 
// Modified by: Daniel Henrique
// 08 24, 2023
// ////////////////////////

using Cinemachine;
using UnityEngine;

public abstract class BaseRocket : MonoBehaviour
{
    // parachute mechanic from: https://www.youtube.com/watch?v=YxKS3lgHYSM

    [SerializeField] protected CinemachineVirtualCamera _rocketCamera;

    [Header("Sound stuff")] [SerializeField]
    protected AudioSource _audioSource;

    [SerializeField] protected AudioClip _thrustSound;
    [SerializeField] protected AudioClip _decoupleSound;
    [SerializeField] protected AudioClip _parachuteSound;

    [Header("Parachute stuff")] [SerializeField]
    protected Transform _parachuteTransform;

    [SerializeField] protected Animator _animator;
    [SerializeField] protected float normalDrag = 1f;
    [SerializeField] protected float tangentDrag = .1f;

    [Header("Rocket physics stuff")] [SerializeField]
    protected Rigidbody _rigidbody;

    [SerializeField] protected float _force;
    [SerializeField] protected float _fuelTime;
    [SerializeField] protected bool _canDetach;

    [SerializeField] protected GameObject _engineSmokePrefab;
    [SerializeField] protected GameObject _decoupleSmokePrefab;
    protected bool _parachuteActivated = false;

    protected float _timeElapsed = 0;

    private void FixedUpdate()
    {
        if (_parachuteActivated)
        {
            var wingPosition = transform.position;
            var velocity = _rigidbody.GetPointVelocity(wingPosition);
            var normal = transform.up;
            var dot = -Vector3.Dot(velocity, normal);

            var normalForce = normal * (dot * normalDrag);
            var tangentForce = -Vector3.ProjectOnPlane(velocity, normal) * tangentDrag;

            var force = normalForce + tangentForce;

            _rigidbody.AddForceAtPosition(force, wingPosition, ForceMode.Force);
        }
    }

    protected abstract void Launch();

    protected virtual void Parachute()
    {
        _parachuteTransform.gameObject.SetActive(true);
        _animator.SetTrigger("Activate");
        _audioSource.clip = _parachuteSound;
        _audioSource.Play();

        _parachuteActivated = true;
    }
}