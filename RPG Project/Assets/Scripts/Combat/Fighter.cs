using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float weaponRange = 2f;

        Mover mover;
        ActionScheduler actionScheduler;
        Transform target;

        private void Start() {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update() {
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
                GetComponent<Animator>().SetTrigger("attack");
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