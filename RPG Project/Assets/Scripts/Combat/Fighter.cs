using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 3f;
        [SerializeField] float weaponDamage = 5f;

        Mover mover;
        ActionScheduler actionScheduler;
        Health target;

        float timeSinceLastAttack;

        private void Start() {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            else
            {
                MoveToTarget();   
            }
        }

        private void MoveToTarget()
        {
            mover.MoveTo(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) <= weaponRange)
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) 
            {
                return false;
            }
            
            Health target = combatTarget.GetComponent<Health>();
            return target != null && !target.IsDead();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform.position);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        //animation event
        void Hit()
        {
            target.TakeDamage(weaponDamage);
        }
    }
}