using byk;
using UnityEngine;

namespace BYK
{
    public class PlayerInteractionController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Consts.WheatTypes.GoldWheat))
            {
                other.gameObject?.GetComponent<GoldWheatCollectibles>().Collect();
            }
            if (other.CompareTag(Consts.WheatTypes.HolyWheat))
            {
                other.gameObject?.GetComponent<HolyWheatCollectibles>().Collect();

            }
            if (other.CompareTag(Consts.WheatTypes.RottenWheat))
            {
                other.gameObject?.GetComponent<RottenWheatCollectibles>().Collect();
            }
        }
    }
}
    
