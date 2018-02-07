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
    private GameObject _Prefab_bite;

    [SerializeField]
    private float biteRadius;
    [SerializeField]
    private float distance;
    [SerializeField]
    private bool falter;

    [SerializeField]
    private Vector3 _Bite_Offset;

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
    }

    bool isAttack = false;

    [SerializeField]
    private float _FindRange;
    private bool _FindPlayer = false;
    void Update()
    {
        if (!_FindPlayer)
        {
            if (Vector3.SqrMagnitude(Vector3.Scale(new Vector3(1, 0, 1), player.position) - Vector3.Scale(new Vector3(1, 0, 1), transform.position)) > _FindRange * _FindRange)
            {
                return;
            }
            _FindPlayer = true;
            StartCoroutine(Routine_Find());
        }

        if (_isFind) return;

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
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                agent.SetDestination(player.position + new Vector3(2, 0, 0));
                distance = Vector3.Distance(gameObject.transform.position, player.position + new Vector3(2, 0, 0));
                transform.localScale = new Vector3(1, 1, 1);
            }

            //if (agent.velocity.x < 0.0f)
            //{
            //    transform.localScale = new Vector3(1, 1, 1);
            //}
            //else if (agent.velocity.x > 0.0f)
            //{
            //    transform.localScale = new Vector3(-1, 1, 1);
            //}


            //if (distance < remainingDistance * 3)
            //{
            //    agent.speed = speed;
            //}
            if (distance < 1)
            {
                StartCoroutine(Routine_Attack());
            }
        }
    }

    private bool _isFind;
    private IEnumerator Routine_Find()
    {
        _isFind = true;
        _EnemyModel.State = EnemyModel.EnemyState.Attack;
        yield return new WaitForSeconds(1.0f);
        _isFind = false;
        _EnemyModel.State = EnemyModel.EnemyState.Wait;
        agent.speed = speed;
    }

    private IEnumerator Routine_Attack()
    {
        isAttack = true;
        agent.speed = 0.0f;

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        _EnemyModel.State = EnemyModel.EnemyState.Wait;
        yield return new WaitForSeconds(1.0f);


        var bite = Instantiate(_Prefab_bite);
        var component = bite.GetComponent<Main.Main_EnemyAttack>();
        component.Enemy = _Enemy;

        if (transform.localScale.x < 0.0f)
        {
            bite.transform.position = _Bite_Offset + transform.position + Vector3.right * biteRadius;
        }
        else
        {
            bite.transform.position = _Bite_Offset + transform.position + Vector3.left * biteRadius;
        }

        _EnemyModel.State = EnemyModel.EnemyState.Attack;
        yield return /*new WaitForSeconds(2.0f)*/Wait_for_Attack();
        _EnemyModel.State = EnemyModel.EnemyState.Wait;

        isAttack = false;

        if (falter)
        {
            //怯みモーション
            //if(怯みモーションend)
            falter = false;
        }
        else
        {
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
