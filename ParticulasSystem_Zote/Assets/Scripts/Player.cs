using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    float xInput;
    Rigidbody2D rb;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float junpImpulse = 10f;

    bool isGrounded = false;

    [SerializeField]
    LayerMask groundlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
     //   Debug.Log(xInput);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
           rb.AddForce(new Vector2(0,junpImpulse), ForceMode2D.Impulse );
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
        IsGrounded();
    }


    private void IsGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 5, groundlayer);
       // Physics2D.overlap
    }

    private void OnDrawGizmos()
    {
       // Gizmos.DrawSphere(transform.position, 3);
       Gizmos.DrawLine(transform.position, (Vector2) transform.position + Vector2.down);
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.right * xInput * speed);
    }


}
