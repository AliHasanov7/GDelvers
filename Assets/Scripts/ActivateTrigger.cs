using UnityEngine;

public class ActivateTrigger : MonoBehaviour
{
    public EnemyAI enemyToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyToActivate != null)
            {
                
                AudioManager.Instance.playMusic("Stress");
                enemyToActivate.isChasing = true;
            }
        }
    }
}