using UnityEngine;

public class DeactivateTrigger : MonoBehaviour
{
    public EnemyAI enemyToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyToActivate != null)
            {
                AudioManager.Instance.StopMusic();
                enemyToActivate.isChasing = false;
            }
        }
    }
}