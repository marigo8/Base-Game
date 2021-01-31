using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMoveBehaviour : MonoBehaviour
{
    public FloatData moveSpeed;

    private CharacterController controller;
    private Vector3 inputDirection;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();
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
        transform.rotation = Quaternion.LookRotation(direction);
        var rotation = transform.eulerAngles;
        rotation.x = 0f;
        rotation.z = 0f;

        var motion = direction * (moveSpeed.value * Time.deltaTime);
        controller.Move(motion);

    }

    public void MoveTo(Vector3 destination)
    {

    }
}
