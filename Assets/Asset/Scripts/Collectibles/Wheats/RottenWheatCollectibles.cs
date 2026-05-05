using UnityEngine;

namespace BYK
{
    public class RottenWheatCollectibles : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float movementDecreaseSpeed;
        [SerializeField] private float resetBoostDuration;
        public void Collect()
        {
            playerController.SetMovementSpeed(movementDecreaseSpeed, resetBoostDuration);
            Destroy(gameObject);
        }
    }
}
   
