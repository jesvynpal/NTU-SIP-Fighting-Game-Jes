using UnityEngine;
using System.Collections;
using System;

public class Enemy : Character {


    private IEnemyState currentState;

    public GameObject Target { get; set; }

    private Vector3 startPos;

    [SerializeField]
    private float meeleRange;

    [SerializeField]
    private Transform leftEdge;

    [SerializeField]
    private Transform rightEdge;

    private Canvas healthCanvas;

    public bool InMeeleRange
    {
        get
        {
            if (Target !=null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meeleRange ;
            }

            return false;
        }
    }

    public override bool IsDead
    {
        get
        {
            return healthStat.CurrentVal <= 0;
        }
    }

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        startPos = transform.position;
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
        healthCanvas = transform.GetComponentInChildren<Canvas>();
	}
	
    
	// Update is called once per frame
	void Update () {

        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
        }
        
        LookAtTarget();
	
	}

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());   
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        if (!attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            
            else if (currentState is PatrolState)
            {
                ChangeDirection();
            }
            
        }
        
    }

    public override void ChangeDirection()
    {
        Transform tmp = transform.FindChild("EnemyHealthBarCanvas").transform;
        Vector3 pos = tmp.position;
        tmp.SetParent(null);
        base.ChangeDirection();
        tmp.SetParent(transform);
        tmp.position = pos;
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left; 
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage()
    {
        if (!healthCanvas.isActiveAndEnabled)
        {
            healthCanvas.enabled = true;
        }

        healthStat.CurrentVal -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            GameObject coin = (GameObject)Instantiate(GameManager.Instance.CoinPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity); //spawns coin when enemy despawn

            Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // coin wont land on top of enemy head

            MyAnimator.SetTrigger("die");
            yield return null;
        }
    }

    public override void Death()
    {
        
        MyAnimator.ResetTrigger("die");
        Destroy(gameObject); //despawns the enemy

        //MyAnimator.SetTrigger("idle");
        //healthStat.CurrentVal = healthStat.MaxVal;  
        //transform.position = startPos;
        //healthCanvas.enabled = false;
    }
}
