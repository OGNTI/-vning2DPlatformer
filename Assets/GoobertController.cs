using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoobertController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float force = 5;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    float groundRadius = 0.2f;

    Rigidbody2D rigidBody;

    bool hasReleasedJump;

    Vector2 footPosition;

    Vector2 bottomColliderSize = Vector2.zero;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(moveX, 0) * speed * Time.deltaTime;

        transform.Translate(movement);

        footPosition = transform.position;

        // bool isGrounded = Physics2D.OverlapCircle(footPosition, groundRadius, groundLayer);
        bool isGrounded = Physics2D.OverlapBox(GetFootPos(), GetFootSize(), 0);

        if (Input.GetAxisRaw("Jump") > 0 && hasReleasedJump && isGrounded)
        {
            rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            hasReleasedJump = false;
        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            hasReleasedJump = true;
        }
    }



    private Vector2 GetFootPos()
    {
        float height = GetComponent<Collider2D>().bounds.size.y;
        return transform.position + Vector3.down * height / 2;
    }

    private Vector2 GetFootSize()
    {
        return new Vector2(GetComponent<Collider2D>().bounds.size.x * 0.9f, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(GetFootPos(), GetFootSize());
    }
}
