using UnityEngine;

namespace SciFiShooter
{
    public class CharacterAnimationState : MonoBehaviour
    {
        [SerializeField] private CharacterController TargetCharacterController;

        [SerializeField] private CharacterMovement TargetCharacterMovement;

        [SerializeField] private Animator TargetAnimator;

        private void LateUpdate()
        {
            Vector3 movementSpeed = transform.InverseTransformDirection(TargetCharacterController.velocity);

            TargetAnimator.SetFloat("Normalize Movement X", movementSpeed.x / TargetCharacterMovement.GetCurrentSpeedByState());
            TargetAnimator.SetFloat("Normalize Movement Z", movementSpeed.z / TargetCharacterMovement.GetCurrentSpeedByState());

            TargetAnimator.SetBool("Is Crouch", TargetCharacterMovement.IsCrouch);  
            TargetAnimator.SetBool("Is Sprint", TargetCharacterMovement.IsSprint);
            TargetAnimator.SetBool("Is Aiming", TargetCharacterMovement.IsAiming);
            TargetAnimator.SetBool("Is Ground", TargetCharacterMovement.IsGrounded);

            if (TargetCharacterMovement.IsGrounded == false)
            {
                TargetAnimator.SetFloat("Jump", movementSpeed.y);
            }

            Vector3 groundSpeed = TargetCharacterController.velocity;
            groundSpeed.y = 0;
            TargetAnimator.SetFloat("Ground Speed", groundSpeed.magnitude);

            TargetAnimator.SetFloat("Distance To Ground", TargetCharacterMovement.DistanceToGround);
        }
    }
}
