using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;
    private Animator animator;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;


    void Start() {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    void Update() {

        cooldownTimer += Time.deltaTime;

        if (PlayerInSight()) {
            if (cooldownTimer >= attackCooldown) {
                animator.SetTrigger("attack");
                cooldownTimer = 0;
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance, 
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null) {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void DamagePlayer() {
        if (PlayerInSight()) {
            playerHealth.TakeDamage(damage);
        }
    }

    private void DestroyOnDead() {
        Destroy(gameObject.GetComponentInParent<EnemyPatrol>().gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance, 
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y));
    }
}
