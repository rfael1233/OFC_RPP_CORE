using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public int parallaxMultiplier;
    public float lerpSpeed;

    private Camera _mainCamera;

    private Vector3 _lastCameraPosition;

    private float _newVelocity;

    private void Start()
    {
        _mainCamera = Camera.main;
        _lastCameraPosition = _mainCamera.transform.position;
        if (parallaxMultiplier >= 0)
        {
            GetComponent<SpriteRenderer>().sortingOrder = - 800 + (100 - parallaxMultiplier);
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 100-parallaxMultiplier;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float velocity = (_mainCamera.transform.position.x - _lastCameraPosition.x);

        //_newVelocity = Mathf.Lerp(_newVelocity, velocity, Time.fixedDeltaTime * lerpSpeed);
        
        _lastCameraPosition = _mainCamera.transform.position;
        
        transform.position += Vector3.right * velocity * ((float)parallaxMultiplier)/100f;
        
    }
}
