using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyAnimations enemyAnimations;
    [SerializeField] private int stopDistance = 6;
    [SerializeField] private int visionDistance = 15;
    [SerializeField] private LayerMask visionLayerMask;
    private Collider2D targetColider;
    private Transform target;
    private Coroutine chasePlayerCor;
    private Coroutine visionCor;
    private Transform myTrasform;


    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        myTrasform = transform;

        visionCor = StartCoroutine(EnemyVisionCor());

    }
    //зрение врага
    private IEnumerator EnemyVisionCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            targetColider = Physics2D.OverlapCircle(myTrasform.position, visionDistance, visionLayerMask);

            if (targetColider != null && target == null)
            {
                target = targetColider.transform;
                chasePlayerCor = StartCoroutine(ChasePlayerCor());
            }
        }
    }

    //погоня за игроком
    private IEnumerator ChasePlayerCor()
    {
        if (visionCor != null)
        {
            StopCoroutine(visionCor);
        }

        while (true)
        {
            agent.SetDestination(target.position);

            yield return new WaitForSeconds(0.1f);

            if (agent.remainingDistance > stopDistance)
            {
                agent.SetDestination(target.position);
                enemyAnimations.Run();
            }
            else
            {
                agent.SetDestination(myTrasform.position);
                enemyAnimations.Attack();

            }
        }
    }

    //остановить погоню
    public void StopChasing()
    {

        if (chasePlayerCor != null)
        {
            StopCoroutine(chasePlayerCor);
        }

        agent.SetDestination(myTrasform.position);
    }


    public Transform GetTarget()
    {
        return target;
    }

}
