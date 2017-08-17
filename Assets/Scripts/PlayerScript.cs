using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    public float speed = 1.0f;
    public string axisName = "Horizontal";
    private Animator anim;
    private float defaultScaleZ;
    private FollowCharacter CameraReference;
    public GameObject currentBackground;
    private SpriteRenderer currentbackgroundSprite;
    private float spriteWidth;
    private float spriteHeight;
    private Rigidbody2D SimonBody;
    public float jumpspeed = 10f;
    private CapsuleCollider2D myCollider;
    private bool isGrounded = false;
    private float distToGround;
    private bool DoOnce;
    private SpriteRenderer SimonSprite;
    private bool attacking;
    public float WhippingDuration;

    // Use this for initialization
    void Start()
    {
        currentbackgroundSprite = currentBackground.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        defaultScaleZ = transform.position.z;
        CameraReference = FindObjectOfType<FollowCharacter>();
        spriteWidth = GetComponent<SpriteRenderer>().bounds.max.x - GetComponent<SpriteRenderer>().bounds.min.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.max.y - GetComponent<SpriteRenderer>().bounds.min.y;
        SimonBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        SimonSprite = GetComponent<SpriteRenderer>();
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        anim.SetBool("Attacking", attacking);

        print(isGrounded);
        
        if (Input.GetAxis(axisName) <0 && transform.rotation.y != 180)
        {

            transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w));
        }
        if(Input.GetAxis(axisName) > 0 && !DoOnce && transform.rotation.y != 0)
        {
            transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w));
        }

        anim.SetFloat("aVelocity",Mathf.Abs (Input.GetAxis(axisName)));
        anim.SetBool("inAir", !isGrounded);
        
        
        if (Input.GetButtonDown("Attack"))
        {
            attacking = true;
            StopAttacking();
        }

        //allows player to move as long as it is withing boundaries of camera
        
            transform.position += new Vector3((Input.GetAxis(axisName) * speed * Time.deltaTime), 0, 0);
        
            transform.position = (new Vector3(Mathf.Clamp(transform.position.x, currentbackgroundSprite.bounds.min.x + spriteWidth, currentbackgroundSprite.bounds.max.x -spriteWidth), transform.position.y, transform.position.z));
        if (Input.GetButtonDown("Jump"))
        {
            print("jumping");
            SimonBody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
        }  
    }
    IEnumerator costopAttacking()
    {
        yield return new WaitForSeconds(WhippingDuration);
        attacking = false;
    }
    void StopAttacking()
    {
        StartCoroutine(costopAttacking());
    }
}
