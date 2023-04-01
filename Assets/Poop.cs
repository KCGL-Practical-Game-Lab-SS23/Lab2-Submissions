using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.AI;

public class Poop : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private MMF_Player poopFeedback;
    [SerializeField] private float damageCooldown = 1f;
    private float _lastDamageTakenAt;

    private void Start()
    {
        agent.SetDestination(playerTransform.position);
        //transform.LookAt(playerTransform.position);
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            Debug.Log("You've been pooped");
        }
        else
        {
            agent.SetDestination(playerTransform.position);
            // transform.LookAt(playerTransform.position); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!NotInCooldown()) return;
        if (!other.CompareTag("Player")) return;
        if (other.TryGetComponent<PlayerController>(out var playerController)) playerController.OhPoopGotMe();

        poopFeedback.PlayFeedbacks();
    }

    private bool NotInCooldown()
    {
        if (Time.time - _lastDamageTakenAt < damageCooldown)
            return false;

        _lastDamageTakenAt = Time.time;
        return true;
    }
}