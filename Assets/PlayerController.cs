using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 1f;
    public float JumpForce = 10f;

    float horizontalMove = 0f;
    bool facingRight = true;
    bool jump = false;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        
        _animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        _animator.SetFloat("VSpeed", _rigidbody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 characterScale = transform.localScale;
        if (facingRight && horizontalMove < 0)
        {
            facingRight = false;
            characterScale.x *= -1; 
        }
        else if (!facingRight && horizontalMove > 0)
        {
            facingRight = true;
            characterScale.x *= -1;
        }
        transform.localScale = characterScale;
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * runSpeed;
        if (jump && (Mathf.Abs(_rigidbody.velocity.y) < 0.001))
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        jump = false;
    }
}
