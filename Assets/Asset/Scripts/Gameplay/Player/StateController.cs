using UnityEngine;

namespace BYK
{
    public class StateController : MonoBehaviour
    {
        private PlayerState currentPlayerState = PlayerState.Idle;

        private void Start()
        {
            ChangeState(PlayerState.Idle);
        }

        public void ChangeState(PlayerState newPlayerState)
        {
            if (currentPlayerState == newPlayerState)
            {
                return;
            }
            currentPlayerState = newPlayerState;
        }

        public PlayerState GetCurrentState()
        {
            return currentPlayerState;
        }
    }
}
