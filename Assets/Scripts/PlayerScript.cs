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
    // Use this for initialization
    void Start()
    {
        currentbackgroundSprite = currentBackground.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        defaultScaleZ = transform.position.z;
        CameraReference = FindObjectOfType<FollowCharacter>();
        spriteWidth = GetComponent<SpriteRenderer>().bounds.max.x - GetComponent<SpriteRenderer>().bounds.min.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.max.y - GetComponent<SpriteRenderer>().bounds.min.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(currentbackgroundSprite.bounds.min.x);
        print(currentbackgroundSprite.bounds.max.x);
        //code for animation goes here
        print(anim.GetBool("isMoving"));
        if (Input.GetAxis(axisName) < 0f && Input.GetButtonUp("Attack"))
        {
            transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            
                anim.Play("Running");
            
        }
        else if (Input.GetAxis(axisName) > 0f )
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
                anim.Play("Running");
            
        }
        else if(Input.GetAxis(axisName) == 0)
        {
            anim.Play("Idle");    
        }

        
      
        //allows player to move as long as it is withing boundaries of camera
        transform.position += new Vector3(( Input.GetAxis(axisName) * speed * Time.deltaTime),0,0);
        transform.position = (new Vector3(Mathf.Clamp(transform.position.x, currentbackgroundSprite.bounds.min.x + spriteWidth, currentbackgroundSprite.bounds.max.x -spriteWidth), transform.position.y, transform.position.z));
    }
}
