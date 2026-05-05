using BYK;
using UnityEngine;

namespace byk
{
    public class GoldWheatCollectibles : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float movementIncreaseSpeed;
        [SerializeField] private float resetBoostDuration;
        public void Collect()
        {
            playerController.SetMovementSpeed(movementIncreaseSpeed, resetBoostDuration);
            Destroy(gameObject);
        }
    }
}
    
