using UnityEngine;

namespace RPG.Combat 
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float health = 100f;

        bool isDead = false;   

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0); //take either the value of health - damage, or 0 is the previous falls into the negative
            print(health);
            if (health == 0)
            {
                Die();
            }
        }

        void Die()
        {
            if (!isDead)
            {
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            }
        } 
    }
}