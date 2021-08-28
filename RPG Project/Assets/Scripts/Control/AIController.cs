using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control {
    
    public class AIController : MonoBehaviour 
    {
        [SerializeField] float chaseDistance = 5.0f;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;

        private void Start() {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player) && !health.IsDead())
            {
                fighter.Attack(player);
            }
            else
            {
                mover.StartMoveAction(guardPosition);
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }
        
        private void OnDrawGizmosSelected() 
        {
            Gizmos.DrawWireSphere(transform.position, chaseDistance);  
        }
    }
}