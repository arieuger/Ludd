using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {

    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] private LayerMask selfMask;
    [SerializeField] private LayerMask rivalMask;

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        Debug.Log("Damage Taken");
        if (currentHealth > 0) {
            anim.SetTrigger("hurt");
            // StartCoroutine(Invunerability());
        }
        else if (!dead) {
            anim.SetTrigger("die");

            if (GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;

            if (GetComponent<EnemyAttack>() != null)
                GetComponent<EnemyAttack>().enabled = false;

            if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;
            
            dead = true;
        }
    }
    public void AddHealth(float value) {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    private IEnumerator Invunerability() {
        Physics2D.IgnoreLayerCollision((int) Mathf.Log(selfMask.value, 2), (int) Mathf.Log(selfMask.value, 2), true);
        yield return new WaitForSeconds(invulnerabilityTime);
        Physics2D.IgnoreLayerCollision((int) Mathf.Log(selfMask.value, 2), (int) Mathf.Log(selfMask.value, 2), false);
    }

}
