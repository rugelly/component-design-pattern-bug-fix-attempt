﻿using UnityEngine;
using System;

public class AirJump : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Grounded _grounded;
    private Jump _jump;
    private InputReader _input;
    private PlayerStats _stats;
    private float maxFuel;
    public float currentFuel;
    private float constantCost;
    private float instantCost;
     public bool disableOverride;
    public bool can;
    public bool active;

    private const int HOVER = 0, ROCKET = 1, BOOST = 2;
    private int jumpType;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _grounded = GetComponent<Grounded>();
        _jump = GetComponent<Jump>();
        _input = GetComponent<InputReader>();
        _stats = GetComponent<StatHolder>().held;

        maxFuel = _stats.maxFuel;
    }

    public GameObject jumpJuiceRef; // TODO: REMOVE THIS

    private void Update()
    {
        jumpType = (int)_stats.jumpType;

        can = _jump.disableOverride ? true : false;
        currentFuel = _grounded.isGrounded ? maxFuel : currentFuel;

        if (_grounded.isGrounded)
            disableOverride = true;

        if (!_jump.disableOverride)
            disableOverride = true;

        if (!disableOverride)
        if (can && _input.jump)
        {
            active = !active;

            if (active && currentFuel > 0)
            {
                switch (jumpType)
                {
                    case HOVER:
                        ActivatedInstantForce(0.8f, 0.9f, _stats.hoverInstantCost, 0);
                        break;
                    case ROCKET:
                        ActivatedInstantForce(0.5f, 0.4f, _stats.rocketInstantCost, 0f);
                        break;
                    case BOOST:
                        if (currentFuel >= _stats.boostInstantCost)
                            InstantJumpForce(0.8f, 0.2f, _stats.boostInstantCost, _stats.boostStrength);
                        active = false;
                        break;
                }
            }
        }

        if (currentFuel == 0)
            active = false;
        if (_grounded.isGrounded)
            active = false;

        // slowly regen fuel to hopefully save a fatal fall
        if (!active && !_grounded.isGrounded)
            currentFuel += 4 * Time.deltaTime;

        jumpJuiceRef.GetComponent<RectTransform>().localScale = new Vector3(currentFuel / 100, 1, 1);
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
    }

    private void FixedUpdate()
    {
        if (active && currentFuel > 0)
        {
            switch (jumpType)
            {
                case HOVER:
                    ActivatedConstantForce(_stats.hoverStrength, _stats.hoverConstantCost, Vector3.up);
                    break;
                case ROCKET:
                    ActivatedConstantForce(_stats.rocketStrength, _stats.rocketConstantCost, Vector3.up);
                    break;
                case BOOST:
                    break;
            }
        }
    }

    // triggers only once every time you activate the airjump
    // without additionalStrength != 0 only adds force to cancel out negative vertical velocity
    private void ActivatedInstantForce(float dampHorizPercent, float cancelVertByPercent, float fuelCostInstant, float additionalStrength)
    {
        _rigidbody.velocity = new Vector3(  _rigidbody.velocity.x * dampHorizPercent,
                                            _rigidbody.velocity.y,
                                            _rigidbody.velocity.z * dampHorizPercent);
        
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.AddForce(Vector3.up * -(_rigidbody.velocity.y * cancelVertByPercent), ForceMode.VelocityChange);
            _rigidbody.AddForce(Vector3.up * additionalStrength, ForceMode.VelocityChange);
        }

        _rigidbody.AddForce(Vector3.up * additionalStrength, ForceMode.VelocityChange);

        currentFuel -= fuelCostInstant;
    }

    // runs constantly while airjump active
    private void ActivatedConstantForce(float strength, float fuelCostPerSecond, Vector3 direction)
    {
        float effectiveStrength = strength - _rigidbody.velocity.y;
        effectiveStrength = Mathf.Max(effectiveStrength, 0);
        _rigidbody.AddForce(direction * effectiveStrength, ForceMode.Acceleration);
        currentFuel -= fuelCostPerSecond * Time.deltaTime;
    }

    // double jump needs own imp
    // cancelVert makes it harder to jump higher the faster you are falling
    private void InstantJumpForce(float dampHorizPercent, float cancelVertByPercent, float fuelCostInstant, float strength)
    {
        _rigidbody.velocity = new Vector3(  _rigidbody.velocity.x * dampHorizPercent,
                                            _rigidbody.velocity.y,
                                            _rigidbody.velocity.z * dampHorizPercent);

        if (_rigidbody.velocity.y < 0)
            _rigidbody.AddForce(Vector3.up * (_rigidbody.velocity.y * cancelVertByPercent), ForceMode.VelocityChange);

        float effectiveStrength = strength - _rigidbody.velocity.y;
        effectiveStrength = Mathf.Max(effectiveStrength, 0);
        _rigidbody.AddForce(Vector3.up * effectiveStrength, ForceMode.VelocityChange);

        currentFuel -= fuelCostInstant;
    }
}
