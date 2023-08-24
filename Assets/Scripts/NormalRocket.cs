// ////////////////////////
// File: NormalRocket.cs
// Created at: 08 22, 2023
// Description:
// 
// Modified by: Daniel Henrique
// 08 24, 2023
// ////////////////////////

using System;
using System.Collections;
using UnityEngine;

public class NormalRocket : BaseRocket
{
    [SerializeField] private GameObject _firstStagePiece;

    private GameObject _decoupleSmoke;

    private GameObject _engineSmoke;

    private void Start()
    {
        EventManager.Instance.OnLaunched += Launch;
        EventManager.Instance.OnLandingImpulse += LandingImpulse;

        _rigidbody.isKinematic = true;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnLaunched -= Launch;
        EventManager.Instance.OnLandingImpulse -= LandingImpulse;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _parachuteActivated = false;
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 0.05f;
        }
    }

    protected override void Launch()
    {
        UIManager.Instance.DeactivateLaunchPanel();
        _rocketCamera.Priority = 3;

        _rigidbody.isKinematic = false;

        var smokePoint = transform.Find("EngineSmokePoint");
        _engineSmoke = Instantiate(_engineSmokePrefab, smokePoint);

        _audioSource.clip = _thrustSound;
        _audioSource.Play();
        StartCoroutine(LaunchRocket());
    }

    private void LandingImpulse()
    {
        _rigidbody.AddForce(_force * 4 * Vector3.down, ForceMode.Impulse);
    }

    private IEnumerator LaunchRocket()
    {
        while (_timeElapsed < _fuelTime)
        {
            _rigidbody.AddForce(_force * transform.up, ForceMode.Acceleration);

            _timeElapsed += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        _audioSource.Stop();
        if (_canDetach)
        {
            _audioSource.clip = _decoupleSound;
            _audioSource.Play();

            StartCoroutine(DetachFirstPiece());
        }
    }

    private IEnumerator DetachFirstPiece()
    {
        UIManager.Instance.ActivateEndPanel();

        _engineSmoke.SetActive(false);

        var smokePoint = transform.Find("DecoupleSmokePoint");
        _decoupleSmoke =
            Instantiate(_decoupleSmokePrefab, smokePoint.position, _decoupleSmokePrefab.transform.rotation);
        transform.SetParent(null);
        _firstStagePiece.transform.gameObject.AddComponent<Rigidbody>();

        StartCoroutine(LifeTimeDelay(1f, _decoupleSmoke));

        _rigidbody.AddForce(_force * transform.up, ForceMode.Impulse);

        while (true)
        {
            if (_rigidbody.velocity.y < 0)
            {
                _audioSource.Stop();
                Parachute();
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator LifeTimeDelay(float t, GameObject go)
    {
        yield return new WaitForSeconds(t);
        go.SetActive(false);
    }
}