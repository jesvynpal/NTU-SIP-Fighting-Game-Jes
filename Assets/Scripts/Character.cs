using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {

    
    public Animator MyAnimator { get; private set; }
    
    [SerializeField]
    protected Stat healthStat;

    public abstract bool IsDead { get; }

    [SerializeField]
    private EdgeCollider2D SwordCollider;

    [SerializeField]
    private List<string> damageSources; 

    [SerializeField]
    protected float movementSpeed;

    protected bool attack;

    public bool TakingDamage { get; set; }

   
    protected bool facingRight;

    // Use this for initialization
    public virtual void Start () {

        
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
        healthStat.Initialize();

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public virtual void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    

    public void MeleeAttack()
    {
        SwordCollider.enabled = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        } 
    }
}
