using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
public class PlayerScript : MonoBehaviour
{

    public float SideStepspeed = 1.0f;
    public float sideStepDuration = 1.0f;
    private Animator anim;
    private FollowCharacter CameraReference;
    public GameObject currentBackground;
    private SpriteRenderer currentbackgroundSprite;
    private float spriteWidth;
    private float spriteHeight;
    private Rigidbody2D SimonBody;
    private SpriteRenderer SimonSprite;
    private bool attacking;
    public float WhippingDuration;
    private bool isDodging;
    private bool lookingRight = true;
    
    [HideInInspector]
    public Collider2D whipHitbox;
    private PlatformerCharacter2D myEngine;
    // Use this for initialization
    void Start()
    {
        currentbackgroundSprite = currentBackground.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        
        CameraReference = FindObjectOfType<FollowCharacter>();
        spriteWidth = GetComponent<SpriteRenderer>().bounds.max.x - GetComponent<SpriteRenderer>().bounds.min.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.max.y - GetComponent<SpriteRenderer>().bounds.min.y;
        SimonBody = GetComponent<Rigidbody2D>();
        
        SimonSprite = GetComponent<SpriteRenderer>();
        whipHitbox = GetComponentsInChildren<BoxCollider2D>()[1];
        whipHitbox.enabled = false;
        myEngine = GetComponent<PlatformerCharacter2D>();
        lookingRight = myEngine.m_FacingRight;
    }



   

    private void Dodgeing()
    {
        
        if (isDodging)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, 8,true);


            if (lookingRight)
            {
                transform.position += new Vector3(1 * SideStepspeed/10, 0, 0);
            }
            else
            {
                transform.position -= new Vector3(1 * SideStepspeed/10, 0, 0);

            }
            endDodgeing();

        }
    }
    private void endDodgeing()
    {
        StartCoroutine(enddodgeingAfterSeconds());
    }
    IEnumerator enddodgeingAfterSeconds()
    {
        yield return new WaitForSeconds(sideStepDuration);
        isDodging = false;
        Physics2D.IgnoreLayerCollision(gameObject.layer, 8, false);
    }
    // Update is called once per frame
    void FixedUpdate()

    {
        
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if (whipHitbox == null)
        {
            whipHitbox = GetComponentsInChildren<BoxCollider2D>()[1];
        }
        if(myEngine == null)
        {
            myEngine = GetComponent<PlatformerCharacter2D>();
        }
        else {
            lookingRight = myEngine.m_FacingRight;

        }
        
        Dodgeing();

        if (Input.GetButtonDown("Dodge"))
        {
            isDodging = true;

        }

        anim.SetBool("Attacking", attacking);
        if (attacking && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            whipHitbox.enabled = true;
        }
        





        if (Input.GetButtonDown("Attack"))
        {
            attacking = true;
            StopAttacking();
        }

        //allows player to move as long as it is withing boundaries of camera

        transform.position = (new Vector3(Mathf.Clamp(transform.position.x, currentbackgroundSprite.bounds.min.x + spriteWidth - 3, currentbackgroundSprite.bounds.max.x - spriteWidth), transform.position.y, transform.position.z));

    }   
    
    IEnumerator costopAttacking()
    {
        yield return new WaitForSeconds(WhippingDuration);
        attacking = false;
        whipHitbox.enabled = false;
    }
    void StopAttacking()
    {
        StartCoroutine(costopAttacking());
    }

	void OnCollisionEnter2D(Collision2D coll) {
     
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        

       
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
