using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SciFiShooter
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;

        [Header("Movement")]
        [SerializeField] private float RifleRunSpeed;

        [SerializeField] private float RifleSprintSpeed;

        [SerializeField] private float AimingWalkSpeed;

        [SerializeField] private float AimingRunSpeed;

        [SerializeField] private float AccelerationRate;

        [SerializeField] private float CrouchSpeed;

        [SerializeField] private float jumpSpeed;

        [Header("State")]
        [SerializeField] private float crouchHeight;

        private bool isAiming;
        private bool isJump;
        private bool isCrouch;
        private bool isSprint;
        private float distanceToGround;

        [HideInInspector]
        public Vector3 TargetDirectionControl;

        public float JumpSpeed => jumpSpeed;
        public float CrouchHeight => crouchHeight;
        public bool IsAiming => isAiming;
        public bool IsJump => isJump;
        public bool IsCrouch => isCrouch;
        public bool IsSprint => isSprint;
        public float DistanceToGround => distanceToGround;

        private float BaseCharacterHeight;
        private float BaseCharacterHeightOffset;

        private Vector3 MovementDirection;
        private Vector3 DirectionControl;

        private void Start()
        {
            BaseCharacterHeight = characterController.height;
            BaseCharacterHeightOffset = characterController.center.y;
        }

        private void FixedUpdate()
        {
            UpdateDistanceToGround();
        }

        private void Update()
        {
            Move();
        }

        public float GetCurrentSpeedByState()
        {
            if (isCrouch) return CrouchSpeed;

            if(isAiming)
            {
                if (isSprint)
                    return AimingRunSpeed;
                else
                    return AimingWalkSpeed;
            }

            if(!isAiming)
            {
                if (isSprint)
                    return RifleSprintSpeed;
                else
                    return RifleRunSpeed;
            }

            return RifleRunSpeed;
        }

        private void Move()
        {
            DirectionControl = Vector3.MoveTowards(DirectionControl, TargetDirectionControl, Time.deltaTime * AccelerationRate);

            if (characterController.isGrounded == true)
            {
                MovementDirection = DirectionControl * GetCurrentSpeedByState();
                
                if (isJump == true)
                {
                    MovementDirection.y = jumpSpeed;
                    isJump = false;
                }
            }

            MovementDirection += Physics.gravity * Time.deltaTime;

            characterController.Move(MovementDirection * Time.deltaTime);
        }

        public void Jump()
        {
            if(characterController.isGrounded == false) return;

            if (isCrouch == true) return;

            if (isAiming == true) return;

            isJump = true;
        }

        public void Crouch()
        {
            if (characterController.isGrounded == false) return;

            if (isSprint == true) return;

            if (isAiming == true) return;

            isCrouch = true;
            characterController.height = crouchHeight;
            characterController.center = new Vector3(0, characterController.center.y / 2 ,0);
        }
        public void UnCrouch()
        {
            isCrouch = false;
            characterController.height = BaseCharacterHeight;
            characterController.center = new Vector3(0, BaseCharacterHeightOffset, 0);
        }

        public void Sprint()
        {
            if (characterController.isGrounded == false) return;

            if (isCrouch == true) return;

            isSprint = true;
        }

        public void UnSprint()
        {
            isSprint = false;
        }

        public void Aiming()
        {
            isCrouch = false;
            isAiming = true;    
        }

        public void UnAiming()
        {
            isAiming = false;
        }

        private void UpdateDistanceToGround()
        {
            RaycastHit hit;
            
            if(Physics.Raycast(transform.position, -Vector3.up, out hit, 1000) == true)
            {
                distanceToGround = Vector3.Distance(transform.position, hit.point);
            }
        }
    }
}
