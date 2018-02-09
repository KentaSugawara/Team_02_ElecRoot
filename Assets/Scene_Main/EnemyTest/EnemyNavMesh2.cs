using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh2 : MonoBehaviour
{
    public Transform player;
    public float remainingDistance;
    private NavMeshAgent agent;
    private float speed;

    [SerializeField]
    private GameObject _Prefab_Needle;

    [SerializeField]
    private float ShotRadius;
    [SerializeField]
    private float distance;
    [SerializeField]
    private bool falter;

    [SerializeField]
    private Vector3 _Shot_Offset;

    [SerializeField]
    private float _Shot_Speed;

    [SerializeField]
    private float _WaitForAttackSeconds;

    [SerializeField]
    private Main_Enemy _Enemy;

    [SerializeField]
    private bool _isLeft;
    public bool isLeft
    {
        get { return _isLeft; }
    }

    [SerializeField]
    private EnemyModel_2 _EnemyModel2;

    void Start()
    {
        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Wait;

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
            _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Walk;
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
            if (distance < 4 && distance > 2)
            {
                AttackRoutine = Routine_Attack();
                StartCoroutine(AttackRoutine);
            }
        }
    }

    private bool _isFind;
    private IEnumerator Routine_Find()
    {
        _isFind = true;
        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Attack;
        yield return new WaitForSeconds(1.0f);
        _isFind = false;
        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Wait;
        agent.speed = speed;
    }

    private IEnumerator AttackRoutine;
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
        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Wait;
        yield return new WaitForSeconds(_WaitForAttackSeconds);


        //var obj = Instantiate(_Prefab_Needle);
        //var bullet = obj.GetComponent<Main.Main_EnemyBullet>();

        //if (transform.localScale.x < 0.0f)
        //{
        //    bullet.transform.position = _Shot_Offset + transform.position + Vector3.right * ShotRadius;
        //    bullet.StartMove(Vector3.right * _Shot_Speed);
        //}
        //else
        //{
        //    bullet.transform.position = _Shot_Offset + transform.position + Vector3.left * ShotRadius;
        //    bullet.StartMove(Vector3.left * _Shot_Speed);
        //}

        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Attack;
        //yield return /*new WaitForSeconds(2.0f)*/Wait_for_Attack();
        yield return new WaitForSeconds(4.0f);
        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Wait;

        isAttack = false;
        AttackRoutine = null;

        agent.speed = speed;
    }

    public void Damage()
    {
        StartCoroutine(Routine_Damage());
    }

    private IEnumerator Routine_Damage()
    {
        if (AttackRoutine != null) StopCoroutine(AttackRoutine);
        AttackRoutine = null;
        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Stop;
        _EnemyModel2.SetColor(Color.red, 0.2f);
        yield return new WaitForSeconds(0.5f);
        _EnemyModel2.State2 = EnemyModel_2.Enemy2State.Wait;
        isAttack = false;
    }

    private IEnumerator Wait_for_Attack()
    {
        for (float t = 0.0f; t < _WaitForAttackSeconds; t += Time.deltaTime)
        {
            yield return null;
        }
    }
}
