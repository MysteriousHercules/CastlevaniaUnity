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
    private bool isGrounded;
    private float distToGround;

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
        
    }
    bool isGroundedfunc()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckIfGrounded();   
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    private void CheckIfGrounded()
    {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll(positionToCheck, new Vector2(0, -1), 0.01f);

        //if a collider was hit, we are grounded
        if (hits.Length > 0)
        {
            isGrounded = true;
        }
    }
        // Update is called once per frame
        void FixedUpdate()
    {


        print(isGrounded);
        isGrounded = isGroundedfunc();



        //allows player to move as long as it is withing boundaries of camera
        
            transform.position += new Vector3((Input.GetAxis(axisName) * speed * Time.deltaTime), 0, 0);
        
            transform.position = (new Vector3(Mathf.Clamp(transform.position.x, currentbackgroundSprite.bounds.min.x + spriteWidth, currentbackgroundSprite.bounds.max.x -spriteWidth), transform.position.y, transform.position.z));
        if (Input.GetButtonDown("Jump"))
        {
            print("jumping");
            SimonBody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
        }  
    }
}
