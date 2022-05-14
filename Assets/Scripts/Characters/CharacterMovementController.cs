using UnityEngine;

namespace SciFiShooter
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private CharacterMovement TargetCharacterMovement;

        [SerializeField] private ThirdPersonCamera TargetCamera;

        [SerializeField] private Vector3 AimingOffset;

        [SerializeField] private Vector3 SpintOffset;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            TargetCharacterMovement.TargetDirectionControl = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            TargetCamera.RotationControl = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if(TargetCharacterMovement.TargetDirectionControl != Vector3.zero || TargetCharacterMovement.IsAiming == true)
            {
                TargetCamera.IsRotateTarget = true;
            }
            else
            {
                TargetCamera.IsRotateTarget = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                TargetCharacterMovement.Aiming();
                TargetCamera.SetTargetOffset(AimingOffset);
            }

            if (Input.GetMouseButtonUp(1))
            {
                TargetCharacterMovement.UnAiming();
                TargetCamera.SetDefaultOffset();
            }

            if (Input.GetButtonDown("Jump"))
            {
                TargetCharacterMovement.Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                TargetCharacterMovement.Crouch();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                TargetCharacterMovement.UnCrouch();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                TargetCharacterMovement.Sprint();
                TargetCamera.SetTargetOffset(SpintOffset);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                TargetCharacterMovement.UnSprint();
                TargetCamera.SetDefaultOffset();
            }
        }
    }
}
