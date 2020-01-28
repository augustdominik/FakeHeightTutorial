using UnityEngine;

public class Dude : MonoBehaviour
{

    //Weaponry
    public GameObject melon;
    public Vector2 groundDispenseVelocity;
    public Vector2 verticalDispenseVelocity;

    //References
    public Transform trnsGun;
    public Transform trnsGunTip;
    public SpriteRenderer sprRndDude;
    public SpriteRenderer sprRndGun;

    //Movement
    public float movementSpeed;
    private Vector2 inputVector;

    //Animation
    private int curAnimIndex = 0;
    private float animTimer = 0;
    public int fps;
    public Sprite[] spritesRunAnim;
    public Sprite spriteIdle;
    
    private Vector2 mousePos;
    private Vector2 mouseWorldPos;
    

    void Update()
    {
        Movement();
        RotateGun();
        Animate();
        FlipSprites();
        Shoot();
    }

    void Movement()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if(inputVector != Vector2.zero)
        {
            transform.position += (Vector3)inputVector.normalized * movementSpeed * Time.deltaTime;
        }
    }

    void RotateGun()
    {
        mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(trnsGun.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        trnsGun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot(){

        if(Input.GetMouseButtonDown(0)){
            GameObject insantiatedMelon = Instantiate(melon, trnsGunTip.position, Quaternion.identity);
            insantiatedMelon.GetComponent<FakeHeightObject>().Initialize(trnsGun.right * Random.Range(groundDispenseVelocity.x, groundDispenseVelocity.y), Random.Range(verticalDispenseVelocity.x, verticalDispenseVelocity.y));
           
        }

    }

    void Animate()
    {
        if(inputVector != Vector2.zero)
        {

            if (animTimer > 1f / fps)
            {
                sprRndDude.sprite = spritesRunAnim[curAnimIndex];

                curAnimIndex++;

                if (curAnimIndex >= spritesRunAnim.Length)
                    curAnimIndex = 0;

                animTimer = 0;
            }

            animTimer += Time.deltaTime;
        }
        else
        {
            sprRndDude.sprite = spriteIdle;
            curAnimIndex = 0;
            animTimer = 0;
        }
    }

    void FlipSprites()
    {

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mouseWorldPos.x > transform.position.x)
        {
            sprRndDude.flipX = false;
            sprRndGun.flipY = false;
        }
        else
        {
            sprRndDude.flipX = true;
            sprRndGun.flipY = true;
        }
    }

}
