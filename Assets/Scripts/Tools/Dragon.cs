using UnityEngine;

public class Dragon : MonoBehaviour
{
    [Header("Dragon")]
    [SerializeField] private PlatformController dragon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ResetSpike(dragon);
        }
    }

    void ResetSpike(PlatformController spike)
    {
        spike.setFromWayPointIndex(0);
        spike.setPercentWayPoints(0);
    }
}
