using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control {
    
    public class AIController : MonoBehaviour 
    {
        [SerializeField] float chaseDistance = 5.0f;

        private void Update()
        {
            if (DistanceToPlayer() <= chaseDistance)
            {
                print("Player is being chased by " + this.name);
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(this.transform.position, player.transform.position);
        }
    }
}