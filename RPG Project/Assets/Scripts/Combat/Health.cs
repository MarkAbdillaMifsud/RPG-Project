using UnityEngine;

namespace RPG.Combat 
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float health = 100f;   

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0); //take either the value of health - damage, or 0 is the previous falls into the negative
            print(health);
        } 
    }
}