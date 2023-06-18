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

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        if (currentHealth > 0) {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else if (!dead) {
            currentHealth = 0;
            anim.SetTrigger("die");

            if (GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;

            if (GetComponent<EnemyAttack>() != null) {
                GetComponent<EnemyAttack>().enabled = false;
                GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }                
            
            dead = true;
        }
    }
    public void AddHealth(float value) {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    public void Respawn() {
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        GetComponent<PlayerMovement>().enabled = true;
        dead = false;
    }

    private IEnumerator Invunerability() {
        Physics2D.IgnoreLayerCollision(7,8, true);
        yield return new WaitForSeconds(invulnerabilityTime);
        Physics2D.IgnoreLayerCollision(7,8, false);
    }

}
