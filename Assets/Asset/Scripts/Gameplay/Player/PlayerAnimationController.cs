using System;
using UnityEngine;

namespace BYK
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;

        private PlayerController playerController;

        private StateController stateController;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            stateController = GetComponent<StateController>();
        }

        private void Start()
        {
            playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
        }

        private void Update()
        {
            SetPlayerAnimations();
        }

        private void PlayerController_OnPlayerJumped()
        {
            playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
            Invoke(nameof(ResetJumping), 0.5f);
        }

        private void ResetJumping()
        {
            playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
        }

        private void SetPlayerAnimations()
        {
            var currentState = stateController.GetCurrentState();

            switch (currentState)
            {
                case PlayerState.Idle:
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                    break;

                case PlayerState.Move:
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                    break;

                case PlayerState.SlideIdle:
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                    break;

                case PlayerState.Slide:
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                    playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                    break;
            }

        }
    }
}