using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineSimple;

public class AirState : State
{
    private InputReader _input;
    private PlayerStats _stats;
    private Motor _motor;
    private Grounded _grounded;
    private EdgeDetect _edgeDetect;
    private Climb _climb;
    private Crouch _crouch;
    private FallDamage _falldmg;
    private Jump _jump;
    private AirJump _airjump;
    private bool toggle;

    public AirState(StateMachine stateMachine) : base(stateMachine)
    {}

    public override void OnStateEnter()
    {
        _input = stateMachine.GetComponent<InputReader>();
        _stats = stateMachine.GetComponent<StatHolder>().held;
        _motor = stateMachine.GetComponent<Motor>();
        _grounded = stateMachine.GetComponent<Grounded>();
        _edgeDetect = stateMachine.GetComponent<EdgeDetect>();
        _edgeDetect.enabled = true;
        _climb = stateMachine.GetComponent<Climb>();
        toggle = false;
        _crouch = stateMachine.GetComponent<Crouch>();
        _falldmg = stateMachine.GetComponent<FallDamage>();

        _jump = stateMachine.GetComponent<Jump>();
        _airjump = stateMachine.GetComponent<AirJump>();
        _airjump.disableOverride = false;

        #region change motor vals
        _motor.speed = _stats.airSpeed;
        _motor.accelRate = _stats.airAccelRate;
        _motor.sprintHorizontalInputReductionMult = 1f;
        #endregion
    }

    public override void OnStateExit()
    {
        _edgeDetect.enabled = false;
    }

    public override void Tick()
    {
        if (_crouch.crouching || _crouch.crouched)
            stateMachine.SetState(new AirCrouchState(stateMachine));

        if (_grounded.isGrounded)
        {
            if (_input.wasSprinting)
                stateMachine.SetState(new SprintState(stateMachine));
            else
                stateMachine.SetState(new NormalState(stateMachine));
        }

        if (_input.moveVertical > 0)
        {
            if (_edgeDetect.canClimbAndStand && !toggle)
            {
                toggle = true;
                _climb.enabled = true;
            }
        }
    }
}
