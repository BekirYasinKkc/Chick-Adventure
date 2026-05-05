using UnityEngine;

namespace BYK
{
    public class HolyWheatCollectibles : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float forceIncrease;
        [SerializeField] private float resetBoostDuration;
        public void Collect()
        {
            playerController.SetJumpForce(forceIncrease, resetBoostDuration);
            Destroy(gameObject);
        }
    }
}
    
