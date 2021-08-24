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

        public bool CanAttack(GameObject combatTarget)
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
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //animation event
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);
        }
    }
}