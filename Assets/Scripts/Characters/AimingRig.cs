using UnityEngine;

namespace SciFiShooter
{
    public class AimingRig : MonoBehaviour
    {
        [SerializeField] private CharacterMovement TargetCharacter;

        [SerializeField] private UnityEngine.Animations.Rigging.Rig[] Rigs;

        [SerializeField] private float ChangeWeightLerpRate;

        private float targetWeight;

        private void Update()
        {
            for(int i = 0; i < Rigs.Length; i++)
            {
                Rigs[i].weight = Mathf.MoveTowards(Rigs[i].weight, targetWeight, Time.deltaTime * ChangeWeightLerpRate);
            }

            if(TargetCharacter.IsAiming == true)
            {
                targetWeight = 1;
            }
            else
            {
                targetWeight = 0;
            }
        }
    }
}
