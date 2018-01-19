using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public enum CharaState
    {
        Wait,
        Walk,
        Run,
        HandAttack,
        BarAttack,
        Shot,
        Damage,
        Down
    }

    public class Main_PlayerCharacter : MonoBehaviour
    {
        [SerializeField]
        private Main_Player_Action _Action;

        [SerializeField]
        private Rigidbody _Rigidbody;

        private CharaState _State = CharaState.Wait;
        public CharaState State
        {
            get { return _State; }
        }

        private bool _isLeft;
        public bool isLeft
        {
            get { return _isLeft; }
        }

        public void ChangeState(CharaState next)
        {
            _State = next;
        }

        public Main_Player_Action Action
        {
            get { return _Action; }
        }

        [SerializeField]
        private Main_LockOnObject _LockOnTarget;
        public Main_LockOnObject LockOnTarget
        {
            get { return _LockOnTarget; }
        }

        [SerializeField]
        private float _WalkMoveSpeed;

        [SerializeField]
        private float _RunMoveSpeed;

        [SerializeField]
        private GameObject LockOnImage;

        [SerializeField]
        private int _HP;
        public int HP
        {
            get { return _HP; }
        }
        public void Damage()
        {
            --_HP;
            if (_HP <= 0)
            {
                _Action.Down();
                Main_GameManager.GameOver();
                _HP = 0;
            }
            else
            {
                _Action.Damage();
            }
            UpdateLifeViewer();
        }

        [SerializeField]
        private int _NumOfBar;
        public int NumOfBar
        {
            get { return _NumOfBar; }
        }

        [SerializeField]
        private float _FindRadius;

        [SerializeField]
        private Main_Player_Attack _HandAttack;

        [SerializeField]
        private Main_Player_Attack _BarAttack;

        public void Init(int HP, int NumOfBar)
        {
            _HP = HP;
            _NumOfBar = NumOfBar;

            //UIのターゲットが自身なら表示を更新
            UpdateBarViewer();
        }

        private void Start()
        {
            StartCoroutine(Routine_Main());
        }

        private IEnumerator Routine_Main()
        {
            while(true)
            {
                if (_LockOnTarget != null)
                {
                    LockOnImage.transform.position = _LockOnTarget.transform.position;
                }
                yield return null;
            }
        }

        private Vector3 CharacterDir = Vector3.right;

        RaycastHit hit;
        int Mask_Floor = 1 << 19;
        public void Move(Vector2 MoveVector, float Magnitude)
        {
            if (_State == CharaState.HandAttack && _State == CharaState.BarAttack && _State == CharaState.Damage && _State == CharaState.Down)
            {
                return;
            }

            //CharacterDir を更新
            {
                Vector2 v2 = MoveVector.normalized;
                CharacterDir = new Vector3(v2.x, 0.0f, v2.y);
            }
            
            Vector3 v = new Vector3(MoveVector.x, 0.0f, MoveVector.y);
            //移動
            _Rigidbody.velocity = Vector3.zero;
            if (Magnitude > 0.5f)
            {
                ChangeState(CharaState.Run);
                _Rigidbody.MovePosition(transform.position + v * _RunMoveSpeed * Time.deltaTime);
            }
            else
            {
                ChangeState(CharaState.Walk);
                _Rigidbody.MovePosition(transform.position + v * _WalkMoveSpeed * Time.deltaTime);
            }
            
            //Vector3 velocity = v * _MoveSpeed * Time.deltaTime;
            //Ray ray = new Ray(transform.position, v.normalized);
            //if (Physics.Raycast(ray, out hit, velocity.magnitude, Mask_Floor))
            //{
            //    var point = hit.point;
            //    point.y = transform.position.y;
            //    transform.position = point;
            //}
            //else
            //{
            //    transform.position += velocity;
            //}
            //transform.position += v * _MoveSpeed * Time.deltaTime;
            //_Rigidbody.position = transform.position + v * _MoveSpeed * Time.deltaTime;
            //_Rigidbody.velocity = v * _MoveSpeed * Time.deltaTime;

            //Debug.Log(transform.position);
            //if (_State != CharaState.HandAttack && _State != CharaState.BarAttack)
            //{
            //    ChangeState(CharaState.Walk);
            //}

            if (v.x < 0)
            {
                _isLeft = true;
            }
            else
            {
                _isLeft = false;
            }

            if (_LockOnTarget != null)
                LockOnImage.transform.position = _LockOnTarget.transform.position;
        }

        public void MoveWait()
        {
            _Rigidbody.velocity = Vector3.zero;
            //
            if (_State != CharaState.HandAttack && _State != CharaState.BarAttack && _State != CharaState.Damage && _State != CharaState.Down)
            {
                ChangeState(CharaState.Wait);
            }
        }

        public void setLockOn(Main_LockOnObject LockOnTarget)
        {
            _LockOnTarget = LockOnTarget;
            LockOnImage.SetActive(true);
        }

        public void stopLockOn()
        {
            _LockOnTarget = null;
            LockOnImage.SetActive(false);
        }

        public Vector3 getPosition()
        {
            return transform.position;
        }

        public Vector3 getFirePortPosition()
        {
            return transform.position;
        }

        public Vector3 calcShotVector()
        {
            if (_LockOnTarget != null)
            {
                Vector3 v = (_LockOnTarget.transform.position - transform.position).normalized;
                v.y = 0.0f;
                return v;
            }
            else
            {
                return CharacterDir;
            }
        }

        /// <summary>
        /// UIのターゲットが自身なら鉄棒の表示を更新
        /// </summary>
        private void UpdateLifeViewer()
        {
            if (Main_GameManager.UIManager.PlayerCharacter == this)
                Main_GameManager.UIManager.LifeViewer.UpdateLife();
        }

        /// <summary>
        /// UIのターゲットが自身ならライフの表示を更新
        /// </summary>
        private void UpdateBarViewer()
        {
            //UIのターゲットが自身なら表示を更新
            if (Main_GameManager.UIManager.PlayerCharacter == this)
                Main_GameManager.UIManager.BarViewer.UpdateBarView();
        }

        /// <summary>
        /// 棒をひとつ消費する
        /// 無かった場合false
        /// </summary>
        /// <returns></returns>
        public bool UseBar()
        {
            //無かった場合
            if (_NumOfBar <= 0) return false;

            --_NumOfBar;
            UpdateBarViewer();
            return true;
        }

        /// <summary>
        /// 鉄棒を補充する
        /// </summary>
        public bool ReplenishmentBar()
        {
            //最大数所持していないなら
            if (_NumOfBar < Main_GameManager.MainSettings.Chara_MaxNumOfBar)
            {
                _NumOfBar = Main_GameManager.MainSettings.Chara_MaxNumOfBar;
                UpdateBarViewer();
                return true;
            }

            return false;
        }

        private int Mask_LockOn = 1 << 9; // LockOnObject
        private IEnumerator FindTargetRoutine;
        /// <summary>
        /// 
        /// </summary>
        public void StartFindLockOnTarget()
        {
            if (FindTargetRoutine != null) StopCoroutine(FindTargetRoutine);
            FindTargetRoutine = Routine_FindTarget();
            StartCoroutine(FindTargetRoutine);
        }

        public void StopFindLockOnTarget()
        {
            if (FindTargetRoutine != null) StopCoroutine(FindTargetRoutine);
            stopLockOn();
        }

        private IEnumerator Routine_FindTarget()
        {
            while(true)
            {
                var colliders = Physics.OverlapSphere(transform.position, _FindRadius, Mask_LockOn);
                if (colliders.Length > 0)
                {
                    Debug.Log(colliders.Length);
                    var target = colliders[0].GetComponent<Main_LockOnObject>();
                    setLockOn(target);
                }
                else
                {
                    stopLockOn();
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
