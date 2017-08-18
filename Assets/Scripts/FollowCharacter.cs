using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour {

    public GameObject target;
    private Camera cameraComponentReference;
    public GameObject backGroundOfThisLevel;
    private SpriteRenderer BackgroundSprite;
    private Bounds BackGroundBounds;
    float verticalExtent;
    float horizontalExtend;
    [HideInInspector]
    public  float minX ;
    [HideInInspector]
    public  float maxX ;
    float minY ;
    float maxY ;
    float backgroundWidth;
    float backgroundHeight;
    private GameObject oldTarget;
    // Use this for initialization
    void Start () {
        cameraComponentReference = GetComponent<Camera>();
       
        AdjustCameraForBackgroundBoundaries();
        oldTarget = backGroundOfThisLevel;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (backGroundOfThisLevel != oldTarget)
        {
            AdjustCameraForBackgroundBoundaries();
            oldTarget = backGroundOfThisLevel;
        }


        //camera always centers on player unless it touches the boundaries of background
        if (target != null)
        {
            transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, minX + cameraComponentReference.orthographicSize * 1.4f, maxX - cameraComponentReference.orthographicSize * 1.4f), transform.position.y, transform.position.z);
        }
    }
    void AdjustCameraForBackgroundBoundaries()
    {
        //This gets called every time the background screen changes
        BackgroundSprite = backGroundOfThisLevel.GetComponent<SpriteRenderer>();
        BackGroundBounds = BackgroundSprite.bounds;
        maxX = BackGroundBounds.max.x;
        minX = BackGroundBounds.min.x;
        
    }
}
