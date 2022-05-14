using UnityEngine;

namespace SciFiShooter
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private CharacterMovement TargetCharacterController;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            TargetCharacterController.TargetDirectionControl = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (Input.GetMouseButtonDown(1))
            {
                TargetCharacterController.Aiming();
            }

            if (Input.GetMouseButtonUp(1))
            {
                TargetCharacterController.UnAiming();
            }

            if (Input.GetButtonDown("Jump"))
            {
                TargetCharacterController.Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                TargetCharacterController.Crouch();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                TargetCharacterController.UnCrouch();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                TargetCharacterController.Sprint();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                TargetCharacterController.UnSprint();
            }
        }
    }
}
