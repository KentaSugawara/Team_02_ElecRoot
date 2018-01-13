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

    void Start()
    {
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
        distance = Vector3.Distance(gameObject.transform.position, player.position);
        if (!isAttack)
        {
            gameObject.transform.Translate(0, -0.5f, 0);
            agent.SetDestination(player.position);
            if (distance < remainingDistance * 2)
            {
                agent.speed = speed;
            }
            if (distance < remainingDistance)
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

        yield return /*new WaitForSeconds(2.0f)*/Wait_for_Attack();

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
