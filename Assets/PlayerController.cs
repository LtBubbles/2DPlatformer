using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D RigidBody;
    public float speed = 5.0f;
    public float jumpForce = 8.0f;
    public float airControlForce = 10.0f;
    public float airControlMax = 1.5f;
    Vector2 boxExtents;


    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        // get extent of collison box
        boxExtents = GetComponent<BoxCollider2D>().bounds.extents;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        // check if we are on the ground
        Vector2 bottom =
        new Vector2(transform.position.x, transform.position.y - boxExtents.y);
        Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);
        RaycastHit2D result = Physics2D.BoxCast(bottom, hitBoxSize, 0.0f,
       new Vector3(0.0f, -1.0f), 0.0f, 1 << LayerMask.NameToLayer("Ground"));
        bool grounded = result.collider != null && result.normal.y > 0.9f;
        if (grounded)
        {
            if (Input.GetAxis("Jump") > 0.0f)
                RigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            else
            {
                // allow a small amount of movement in the air
                float vx = RigidBody.velocity.x;
                if (h * vx < airControlMax)
                    RigidBody.AddForce(new Vector2(h * airControlForce, 0));
            }
        }
    }