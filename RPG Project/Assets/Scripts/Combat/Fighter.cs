using UnityEngine;
using RPG.Movement;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour {

        [SerializeField] float weaponRange = 2f;
        Mover mover;
        Transform target;

        private void Start() {
            mover = GetComponent<Mover>();
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
                mover.Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }
    }
}