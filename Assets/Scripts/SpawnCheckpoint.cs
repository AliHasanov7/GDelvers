using UnityEngine;

public class SpawnCheckpoint : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.UpdateSpawnPoint(transform.position);
            }

            // Тут можно добавить визуальный эффект (например, смена цвета флажка или анимация)
        }
    }
}
