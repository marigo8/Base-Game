using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMoveBehaviour : MonoBehaviour
{
    public FloatData runSpeed, sprintSpeed, walkSpeed, jumpStrength;
    public IntData maxJumps;
    public bool inputRun = true, inputSprint = true, inputWalk = true, inputJump = true, gravity = true;

    private CharacterController controller;
    private Vector3 inputDirection, motion;
    private float currentSpeed;
    private int jumps;
    private bool grounded;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (inputRun)
        {
            InputMove();
        }
        if (inputJump)
        {
            InputJump();
        }
        if (gravity)
        {
            Fall();
        }

        controller.Move(motion * Time.deltaTime);
        if(controller.isGrounded){
            motion.y = 0;
            jumps = 0;
            grounded = true;
        }
        else if(grounded)
        {
            grounded = false;
            jumps++;
        }
    }

    public void InputMove()
    {
        InputMoveSpeed();
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        inputDirection.Set(hInput, 0, vInput);

        inputDirection = Vector3.ClampMagnitude(inputDirection, 1);

        Move(inputDirection);
    }

    public void InputMoveSpeed()
    {
        currentSpeed = runSpeed.value;

        if (inputWalk && Input.GetButton("Walk"))
        {
            currentSpeed = walkSpeed.value;
        }
        if (inputSprint && Input.GetButton("Sprint"))
        {
            currentSpeed = sprintSpeed.value;
        }
    }

    public void Move(Vector3 direction)
    {
        motion.x = 0;
        motion.z = 0;
        motion += direction * currentSpeed;

        if(direction.sqrMagnitude > 0.01f){
            transform.rotation = Quaternion.LookRotation(direction);
            var rotation = transform.eulerAngles;
            rotation.x = 0f;
            rotation.z = 0f;
        }
    }

    public void InputJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (jumps >= maxJumps.value) return;
        if(!grounded) jumps++;

        motion.y = jumpStrength.value;
    }

    public void Fall()
    {
        motion.y += Physics.gravity.y * Time.deltaTime;
    }
}
