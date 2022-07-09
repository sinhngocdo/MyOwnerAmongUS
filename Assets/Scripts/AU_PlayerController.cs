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

    //role
    [SerializeField] bool isImporter;
    [SerializeField] InputAction KILL;

    AU_PlayerController target;
    [SerializeField] Collider myCollider;

    bool isDead;

    private void Awake()
    {
        KILL.performed += KillTarget;
    }


    private void OnEnable()
    {
        WASD.Enable();
        KILL.Enable();
    }

    private void OnDisable()
    {
        WASD.Disable();
        KILL.Disable();
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
        if (!hasControl)
        {
            return;
        }
        myAvatarSprite.color = myColor;

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasControl)
        {
            return;
        }
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

    public void SetRole(bool newRole)
    {
        isImporter = newRole;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AU_PlayerController tempTarget = other.GetComponent<AU_PlayerController>();
            if (isImporter)
            {
                if (tempTarget.isImporter)
                {
                    return;
                }
                else
                {
                    target = tempTarget;
                    //Debug.Log(target.name);
                }
            }
        }
    }

    void KillTarget(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if(target == null)
            {
                return;
            }
            else
            {
                if (target.isDead)
                {
                    return;
                }
                transform.position = target.transform.position;
                target.Die();
                target = null;
            }
        } 
    }

    public void Die()
    {
        isDead = true;

        playerAnim.SetBool("isDead", isDead);
    }

}
