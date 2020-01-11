using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private InputReader _input;
    private Grounded _grounded;
    private PlayerStats _stats;
    private float strength {get{return _stats.jumpStrength;}}
    private Vector3 direction {get{return _stats.jumpDirection;}}
    private int forgivenessTimer;
     public bool disableOverride; // bypass weird behaviour when enabling/disabling component

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<InputReader>();
        _grounded = GetComponent<Grounded>();
        _stats = GetComponent<StatHolder>().held;

        disableOverride = false;
    }

    private void Update()
    {
        float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * strength);
        jumpSpeed = Mathf.Max(jumpSpeed - _rigidbody.velocity.y, 0);

        if (!disableOverride)
        {
            if (_input.jump)
            {
                _rigidbody.AddForce(direction * jumpSpeed, ForceMode.VelocityChange);
                disableOverride = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_grounded.isGrounded)
        {
            forgivenessTimer = 0;
        }    
        else
        {
            forgivenessTimer++;
            forgivenessTimer = Mathf.Clamp(forgivenessTimer, 0, 10);
            if (forgivenessTimer > 7f)
                disableOverride = true;
        }
    }
}
