using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent agent;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Начинает погоню
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }

        // Останавливает погоню
        if (other.CompareTag("Escaped"))
        {
            isChasing = false;
            agent.ResetPath();
            Debug.Log("Escaped");
        }
    }
}