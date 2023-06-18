using UnityEngine;
using Cinemachine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
    [Header ("Movement and velocity")]
    // Movemento e velocidade
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool smoothActivated = false;
    [Range(0,0.3f)][SerializeField] private float movementSmooth;
    private Rigidbody2D rb;
    private float horizontalMovement;
    public float HorizontalMovement {
        get { return horizontalMovement; }
        set { horizontalMovement = value; }
    }
    private Vector3 velocity = Vector3.zero;
    private bool lookingRight = true;
    private bool isRunning = true;
    public bool IsRunning { 
        get { return isRunning; } 
        set { isRunning = value; } 
    } 

    [Header ("Jump and groundcheck")]
    // Salto e suelo
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector3 dimensionBox;
    [SerializeField] private float fallGravityScale;
    
    private float defaultGravityScale;
    private bool isGrounded;
    public bool IsGrounded {
        get { return isGrounded; }
        set { isGrounded = value; }
    }
    private bool jump = false;
    public bool Jump {
        get { return jump; }
        set { jump = value; }
    }
    private bool doubleJump;

    [Header ("Particle Effects")]
    // Partículas
    [SerializeField] private ParticleSystem footstepsEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    private ParticleSystem.EmissionModule footEmission;
    private ParticleSystem.MinMaxCurve initialFootEmissionROT;

    // Cámara e animacións
    private CinemachineVirtualCamera vCam;
    private Animator animator;

    // Singleton
    public static PlayerMovement Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    // Conciencia
    private Conscience conscience;
    private bool conscienceAllowsMovement = true;
    private Material mat;
    private Color starterLightColor;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        footEmission = footstepsEffect.emission;
        initialFootEmissionROT = footEmission.rateOverTime;
        defaultGravityScale = rb.gravityScale;
        mat = GetComponent<SpriteRenderer>().material;
        starterLightColor = mat.GetColor("_GlowColor");
        conscience = GetComponent<Conscience>();
        StartCoroutine(CheckConscience());
    }

    void Update() {
        horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * (isRunning ? 1.5f : 1f) * (conscienceAllowsMovement ? 1f : 0);
        if (Input.GetButtonDown("Jump")) jump = true;
        UpdateAnimations();
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapBox(groundController.position, dimensionBox, 0f, groundLayers);
        Move(horizontalMovement * Time.fixedDeltaTime); 
        CheckGravity();
    }

    private void Move(float moving) {
        if (conscienceAllowsMovement) {
            Vector3 targetVelocity = new Vector2(moving, rb.velocity.y);
            rb.velocity = smoothActivated ? Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmooth) : targetVelocity;


            if (moving > 0 && !lookingRight) Turn();
            else if (moving < 0 && lookingRight) Turn();

            // efectos de partículas
            if (moving != 0 && isGrounded) {
                footEmission.rateOverTime = initialFootEmissionROT;
            } else {
                footEmission.rateOverTime = 0f;
            }

            if (jump) {
                if (isGrounded) {
                    isGrounded = false;
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    jumpEffect.Play();
                    doubleJump = true;
                } else if (doubleJump) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    animator.SetTrigger("doubleJump");
                    doubleJump = false;
                }            
            }
        } else {
            footEmission.rateOverTime = 0f;
            if (isGrounded) {
                rb.velocity = Vector2.zero;
            }
        }

        jump = false;
    }

    private void Turn() {
        lookingRight = !lookingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        vCam.GetComponentInChildren<CinemachineFramingTransposer>().m_TrackedObjectOffset.x *= -1f;
    }


    private void UpdateAnimations() {
        animator.SetFloat("horizontalMovement", Mathf.Abs(horizontalMovement));
        animator.SetFloat("verticalMovement", isGrounded ? 0 : rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void CheckGravity() {
        if (rb.velocity.y < -0.1f) {
            rb.gravityScale = fallGravityScale;
            if (vCam.GetComponentInChildren<CinemachineFramingTransposer>().m_TrackedObjectOffset.y > 0f)
                vCam.GetComponentInChildren<CinemachineFramingTransposer>().m_TrackedObjectOffset.y *= -1f;
        } else {
            rb.gravityScale = defaultGravityScale;
            vCam.GetComponentInChildren<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = Mathf.Abs(vCam.GetComponentInChildren<CinemachineFramingTransposer>().m_TrackedObjectOffset.y);
        }
    }

    private IEnumerator CheckConscience() {
        while (true) {
            conscienceAllowsMovement = Random.value + 0.1f > conscience.currentConscience / 10;
            if (!conscienceAllowsMovement)
                mat.SetColor("_GlowColor", Color.red * 4);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            
            conscienceAllowsMovement = true;
            mat.SetColor("_GlowColor", starterLightColor);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, dimensionBox);
    }
}
