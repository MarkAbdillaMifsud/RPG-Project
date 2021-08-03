using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 3f;

        Mover mover;
        ActionScheduler actionScheduler;
        Transform target;
        float timeSinceLastAttack;

        private void Start() {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
            if (target != null)
            {
                MoveToTarget();   
            }
        }

        private void MoveToTarget()
        {
            mover.MoveTo(target.position);
            if (Vector3.Distance(transform.position, target.position) <= weaponRange)
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        //animation event
        void Hit()
        {

        }
    }
}