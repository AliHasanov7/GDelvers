using UnityEngine;
using UnityEngine.AI; 

[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(NavMeshAgent))] 
public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform; 

    public bool isChasing = false; 

    private BaseMovement movement;
    private NavMeshAgent agent; 

    void Start()
    {
        movement = GetComponent<BaseMovement>();
        agent = GetComponent<NavMeshAgent>(); 

        agent.updatePosition = false; 
        agent.updateRotation = false; 

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }

    void Update()
    {
        if (!isChasing || playerTransform == null)
        {
            if (agent.hasPath) agent.ResetPath();
            movement.MoveTo(Vector3.zero);
            return;
        }

        agent.nextPosition = transform.position;

        agent.SetDestination(playerTransform.position);

        Vector3 moveDirection = agent.desiredVelocity;
        moveDirection.y = 0f;

        if (moveDirection.magnitude > 0.1f)
        {
            movement.MoveTo(moveDirection.normalized);
        }
        else
        {
            movement.MoveTo(Vector3.zero);
        }
    }

    void LateUpdate()
    {
        if (isChasing && agent != null)
        {
            agent.nextPosition = transform.position;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isChasing && hit.gameObject.CompareTag("Player"))
        {
            KillPlayer(hit.gameObject);
        }
    }

    private void KillPlayer(GameObject player)
    {
        Debug.Log("Игрок убит врагом!");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RespawnPlayer(player);
        }
    }
}