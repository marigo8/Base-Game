using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMoveBehaviour : MonoBehaviour
{
    public FloatData moveSpeed, jumpStrength;

    private CharacterController controller;
    private Vector3 inputDirection, motion;
    private float yVelocity;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Debug.Log(motion);
        controller.Move(motion * Time.deltaTime);
        if(controller.isGrounded){
            motion.y = 0;
        }
    }

    public void InputMove()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        inputDirection.Set(hInput, 0, vInput);

        inputDirection = Vector3.ClampMagnitude(inputDirection, 1);

        Move(inputDirection);
    }

    public void Move(Vector3 direction)
    {
        motion.x = 0;
        motion.z = 0;
        motion += direction * (moveSpeed.value);

        if(direction.sqrMagnitude > 0.1f){
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
        Debug.Log("Boing!");
        motion.y = jumpStrength.value;
    }

    public void Fall()
    {
        motion.y += Physics.gravity.y * Time.deltaTime;
    }
}
