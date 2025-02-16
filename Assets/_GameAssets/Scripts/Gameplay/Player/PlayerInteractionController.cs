using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Const.WheatTypes.GOLD_WHEAT)
        {
            other.gameObject?.GetComponent<GoldWheatCollectible>().Collect();
        }
        if (other.gameObject.tag == Const.WheatTypes.ROTTEN_WHEAT)
        {
            other.gameObject?.GetComponent<RottenWheatCollectible>().Collect();
        }
        if (other.gameObject.tag == Const.WheatTypes.HOLY_WHEAT)
        {
            other.gameObject?.GetComponent<HolyWheatCollectible>().Collect();
        }
    }
}
