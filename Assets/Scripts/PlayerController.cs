using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float m_speed;

    public float m_jumpSpeed;

    [SerializeField]
    private float m_groundCheckDistance = .55f;

    [SerializeField]
    private float m_fallSpeed;

    private Rigidbody2D m_rigidbody;

    private bool m_isGrounded;

    private Vector2 m_direction;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_direction = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_direction = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Z) && m_isGrounded)
        {
            m_rigidbody.AddForce(Vector2.up * m_jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        m_rigidbody.AddForce(m_direction * m_speed);

        int layerMask = LayerMask.GetMask("Floor");
        m_isGrounded = Physics2D.Raycast(transform.position, Vector2.down, m_groundCheckDistance, layerMask).collider != null;
        Color col = m_isGrounded ? Color.green : Color.red;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * m_groundCheckDistance, col);

        if (!m_isGrounded)
        {
            Vector3 rpos = transform.position + Vector3.right * .5f;
            m_isGrounded = Physics2D.Raycast(rpos, Vector2.down, m_groundCheckDistance, layerMask).collider != null;
            col = m_isGrounded ? Color.green : Color.red;
            Debug.DrawLine(rpos, rpos + Vector3.down * m_groundCheckDistance, col);
        }
        if (!m_isGrounded)
        {
            Vector3 lpos = transform.position + Vector3.left * .5f;
            m_isGrounded = Physics2D.Raycast(lpos, Vector2.down, m_groundCheckDistance, layerMask).collider != null;
            col = m_isGrounded ? Color.green : Color.red;
            Debug.DrawLine(lpos, lpos + Vector3.down * m_groundCheckDistance, col);
        }

        // if we're falling add some more fall speed
        if (!m_isGrounded && m_rigidbody.velocity.y < 0.0f)
        {
            m_rigidbody.AddForce(Vector2.down * m_fallSpeed);
        }
    }

}
