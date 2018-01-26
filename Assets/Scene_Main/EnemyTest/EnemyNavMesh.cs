using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    public float remainingDistance;
    private NavMeshAgent agent;
    private float speed;

    [SerializeField]
    private Main.Main_EnemyAttack bite;

    [SerializeField]
    private float biteRadius;
    [SerializeField]
    private float distance;
    [SerializeField]
    private bool falter;

    [SerializeField]
    private Main_Enemy _Enemy;

    [SerializeField]
    private bool _isLeft;
    public bool isLeft
    {
        get { return _isLeft; }
    }

    [SerializeField]
    private EnemyModel _EnemyModel;

    void Start()
    {
        _EnemyModel.State = EnemyModel.EnemyState.Wait;

        agent = gameObject.GetComponent<NavMeshAgent>();
        speed = agent.speed;
        agent.speed = 0.0f;

        agent.SetDestination(player.position);

        agent.updateRotation = false;
        bite.gameObject.SetActive(false);
    }

    bool isAttack = false;

    void Update()
    {
        if (_Enemy.isDead) return;

        if (!isAttack)
        {
            _EnemyModel.State = EnemyModel.EnemyState.Walk;
            gameObject.transform.Translate(0, -0.5f, 0);
            //if (player.position.x > transform.position.x) { agent.SetDestination(player.position + new Vector3(remainingDistance, 0, 0)); }
            //else { agent.SetDestination(player.position - new Vector3(remainingDistance, 0, 0)); }

            if (player.position.x > transform.position.x)
            {
                agent.SetDestination(player.position - new Vector3(2, 0, 0));
                distance = Vector3.Distance(gameObject.transform.position, player.position - new Vector3(2, 0, 0));
            }
            else
            {
                agent.SetDestination(player.position + new Vector3(2, 0, 0));
                distance = Vector3.Distance(gameObject.transform.position, player.position + new Vector3(2, 0, 0));
            }

            if (agent.velocity.x < 0.0f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (distance < remainingDistance * 3)
            {
                agent.speed = speed;
            }
            if (distance < 1)
            {
                StartCoroutine(Routine_Attack());
            }
        }
    }

    private IEnumerator Routine_Attack()
    {
        isAttack = true;
        bite.gameObject.SetActive(true);
        agent.speed = 0.0f;

        Vector3 v = (player.position - transform.position).normalized;
        bite.transform.position = transform.position + v * biteRadius;

        _EnemyModel.State = EnemyModel.EnemyState.Attack;
        yield return /*new WaitForSeconds(2.0f)*/Wait_for_Attack();
        _EnemyModel.State = EnemyModel.EnemyState.Wait;

        isAttack = false;
        bite.gameObject.SetActive(false);

        if (falter)
        {
            //怯みモーション
            //if(怯みモーションend)
            falter = false;
        }
        else
        {
            bite.Damage();
            agent.speed = speed;
        }
    }

    private IEnumerator Wait_for_Attack()
    {
        float count = 0;
        while (count < 2.0f)
        {
            if (falter)
            {
                break;
            }
            count += Time.deltaTime;
            yield return null;
        }
    }
}
