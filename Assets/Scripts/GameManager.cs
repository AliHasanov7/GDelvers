using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform defaultSpawnPoint;
    private Vector3 currentSpawnPosition; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (defaultSpawnPoint != null)
        {
            currentSpawnPosition = defaultSpawnPoint.position;
        }
        else
        {
            currentSpawnPosition = GameObject.FindWithTag("Player").transform.position;
        }
    }

    public void UpdateSpawnPoint(Vector3 newPosition)
    {
        currentSpawnPosition = newPosition;
        Debug.Log("Точка респавна обновлена!");
    }

    public void RespawnPlayer(GameObject player)
    {
        Debug.Log("Игрок возрождается...");

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        player.transform.position = currentSpawnPosition;

        if (controller != null)
        {
            controller.enabled = true;
        }

        EnemyAI enemy = FindObjectOfType<EnemyAI>();
        if (enemy != null)
        {
            enemy.isChasing = false; 
        }
    }
}