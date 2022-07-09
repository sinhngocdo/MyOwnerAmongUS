using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AU_PlayerController : MonoBehaviour
{
    [SerializeField] bool hasControl;
    public static AU_PlayerController localPlayer;

    //component
    Rigidbody rb;
    Transform myAvatar;
    Animator playerAnim;

    //player movement
    [SerializeField] InputAction WASD;
    Vector2 movementInput;
    [SerializeField] float movementSpeed;

    //Player color
    static Color myColor;
    SpriteRenderer myAvatarSprite;


    private void OnEnable()
    {
        WASD.Enable();
    }

    private void OnDisable()
    {
        WASD.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (hasControl)
        {
            localPlayer = this;
        }

        rb = GetComponent<Rigidbody>();
        myAvatar = transform.GetChild(0);
        playerAnim = GetComponent<Animator>();
        myAvatarSprite = myAvatar.GetComponent<SpriteRenderer>();
        if(myColor == Color.clear)
        {
            myColor = Color.white;
        }
        myAvatarSprite.color = myColor;

    }

    // Update is called once per frame
    void Update()
    {
        movementInput = WASD.ReadValue<Vector2>();

        if(movementInput.x != 0 || movementInput.y != 0)
        {
            myAvatar.localScale = new Vector2(Mathf.Sign(movementInput.x), 1);
            playerAnim.SetBool("isRun", true);
        }
        else
        {
            playerAnim.SetBool("isRun", false);
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = movementInput * movementSpeed;
    }

    public void SetColor(Color newColor)
    {
        myColor = newColor;
        if(myAvatarSprite != null)
        {
            myAvatarSprite.color = myColor;
        }
    }

}
