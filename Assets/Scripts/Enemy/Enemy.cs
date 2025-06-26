using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy
{

    [SerializeField] private NavMeshAgent agent;  
    [SerializeField] private int stopDistance = 6;
    [SerializeField] private int visionDistance = 15;
    [SerializeField] private LayerMask visionLayerMask;
    private IEnemyAnimations enemyAnimations;
    private Collider2D targetColider;
    private Transform target;
    private Coroutine chasePlayerCor;
    private Coroutine visionCor;
    private Transform myTrasform;


    private void Awake()
    {
        enemyAnimations = GetComponent<IEnemyAnimations>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        myTrasform = transform;
    }

    private void Start()
    {
        StartVision();
    }

    //включить поиск игрока у врага
    public void StartVision()
    {
        visionCor = StartCoroutine(EnemyVisionCor());
    }
    //отключить поиск игрока
    public void StopVision()
    {
        if (visionCor != null)      
        StopCoroutine(visionCor);        
    }

    //погоня за игроком
    public void StartChasePlayer()
    {
        chasePlayerCor = StartCoroutine(ChasePlayerCor());
    }

    //остановить погоню
    public void StopChasing()
    {

        if (chasePlayerCor != null)
        StopCoroutine(chasePlayerCor);

        agent.SetDestination(myTrasform.position);
    }

    //возвращает трансформ цели
    public Transform GetTarget()
    {
        return target;
    }

   
    private IEnumerator EnemyVisionCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            targetColider = Physics2D.OverlapCircle(myTrasform.position, visionDistance, visionLayerMask);

            if (targetColider != null && target == null)
            {
                target = targetColider.transform;
                StartChasePlayer();
            }
        }
    }

   
    private IEnumerator ChasePlayerCor()
    {
        StopVision();

        while (true)
        {
            agent.SetDestination(target.position);

            if (Vector2.Distance(myTrasform.position, target.transform.position) > stopDistance)
            {
                agent.SetDestination(target.position);
                enemyAnimations.Run();
            }
            else
            {
                agent.SetDestination(myTrasform.position);
                enemyAnimations.Attack();

            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
