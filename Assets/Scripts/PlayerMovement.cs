using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager theGM;
    public Rigidbody theRB;
    public Transform modelHolder;
    public LayerMask whatIsGround;
    public bool onGround;
    public float jumpForce;
    public Animator anim;
    public int coinsCollected;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public float invincibleTime;
    private float invincibleTimer;
    // public Rigidbody theRB;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (theGM.canMove)
        {
            onGround = Physics.OverlapSphere(modelHolder.position, 0.2f, whatIsGround).Length > 0f;

            if (onGround)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // make player jump
                    theRB.velocity = new Vector3(0f, jumpForce, 0f);
                }
            }


        }

        // control invincibility
        if (invincibleTimer > 0f)
        {
            invincibleTimer -= Time.deltaTime;
        }

        // control animations
        anim.SetBool("walking", theGM.canMove);
        anim.SetBool("onground", onGround);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard")
        {
            if (invincibleTimer <= 0f)
            {
                theGM.HitHazard();

                theRB.constraints = RigidbodyConstraints.None;

                theRB.velocity = new Vector3(Random.Range(GameManager._worldSpeed / 2f, -GameManager._worldSpeed / 2f), 2.5f, -GameManager._worldSpeed / 2.2f);
            }
        }

        if (other.tag == "Coin")
        {
            theGM.AddCoin();
            Destroy(other.gameObject);
        }

    }

    public void ResetPlayer()
    {
        theRB.constraints = RigidbodyConstraints.FreezeRotation;
        transform.rotation = startRotation;
        transform.position = startPosition;

        invincibleTimer = invincibleTime;
    }

}
