using UnityEngine;
using System.Collections;
using System;

public delegate void DeadEventHandler();

public class Player : Character {

    private static Player instance;

    public event DeadEventHandler Dead;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        } 
    }

    private Rigidbody2D myRigidbody;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;


    public GameObject gameOverScreen;

    private bool isGrounded;

    private bool jump;

    private bool jumpAttack;

    private bool immortal = false;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float immortalTime;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0)
            {
                OnDead();
            }
             
            return healthStat.CurrentVal <= 0;
        }
    }

    public bool IsFalling
    {
        get
        {
            return myRigidbody.velocity.y < 0;
        }
    }

    private Vector2 startPos;

   

    // Use this for initialization
    public override void Start () {

       
        base.Start();
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();    
        myRigidbody = GetComponent<Rigidbody2D>();

        Debug.Log("Player !!!!");

        if(Application.loadedLevel != 1 && PlayerPrefs.HasKey("Health"))
        {
            Debug.Log("health player: " + PlayerPrefs.GetFloat("Health"));
            healthStat.CurrentVal = PlayerPrefs.GetFloat("Health");
        }
        else
        {
            Debug.Log("level: " + Application.loadedLevel + " ; " + PlayerPrefs.HasKey("Health"));
        }


    }

    void Update ()
    {
        if (!TakingDamage && !IsDead)
        { 
            if (transform.position.y <= -18f)
            {
                Death();
            }
            HandleInput();
        }
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            isGrounded = IsGrounded();

            HandleMovement(horizontal);

            Flip(horizontal);

            HandleAttacks();

            HandleLayers();

            ResetValues();
        }
        
	}

    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (IsFalling)
        {
            gameObject.layer = 11;
            MyAnimator.SetBool("land", true);
        }

        if (!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            MyAnimator.SetTrigger("jump");
        }
        

        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        if (attack && isGrounded &&!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            MyAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;
        }

        if (jumpAttack && !isGrounded && !this.MyAnimator.GetCurrentAnimatorStateInfo(1).IsName("jumpAttack"))
        {
            MyAnimator.SetBool("jumpAttack", true);
        }

        if (!jumpAttack && !this.MyAnimator.GetCurrentAnimatorStateInfo(1).IsName("jumpAttack"))
        {
            MyAnimator.SetBool("jumpAttack", false);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsFalling)
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            jumpAttack = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

    private void ResetValues()
    {
        attack = false;
        jump = false;
        jumpAttack = false;
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <=0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i=0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        MyAnimator.ResetTrigger("jump");
                        MyAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }

        return false;
    }
    
    private void HandleLayers()
    {
        if (!isGrounded)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    private IEnumerator IndicateImmortal ()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentVal -= 10;

            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }
        }

        if (!immortal && Application.loadedLevel == 3)
        {
            healthStat.CurrentVal -= 20;

            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }

        }
        
    }

        
   

    public override void Death()
    {
        

        MyAnimator.ResetTrigger("die");
        Destroy(gameObject);
        gameOverScreen.SetActive(true);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            GameManager.Instance.CollectedCoins++;
            Destroy(other.gameObject);
        }

        
    }
    
}

