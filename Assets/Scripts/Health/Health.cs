using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [Header ("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private List<EnemyPatrol> enemies;
    [SerializeField] private List<DissapearingPlatform> dissapearingPlatforms;
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
        RespawnEnemies();
        dead = false;
    }

    private void RespawnEnemies() {
        foreach (EnemyPatrol enemy in enemies) {
            enemy.gameObject.SetActive(true);
            enemy.GetComponentInChildren<EnemyAttack>().enabled = true;
        }

        foreach (DissapearingPlatform platform in dissapearingPlatforms) {
            platform.gameObject.SetActive(true);
        }
    }

    private IEnumerator Invunerability() {
        Physics2D.IgnoreLayerCollision(7,8, true);
        yield return new WaitForSeconds(invulnerabilityTime);
        Physics2D.IgnoreLayerCollision(7,8, false);
    }

}
