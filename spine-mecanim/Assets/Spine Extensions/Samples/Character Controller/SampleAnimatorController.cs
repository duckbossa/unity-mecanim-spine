using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private bool _isJumping;

    void Start()
    {
        _isJumping = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _anim.SetBool("flipX", true);
            _anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            _anim.SetBool("flipX", false);
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _anim.SetTrigger("shoot");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isJumping)
            {
                _isJumping = true;
                StartCoroutine(Jump());
            }
        }

    }

    IEnumerator Jump()
    {
        _anim.SetTrigger("jump");
        _anim.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.9f);
        _isJumping = false;
        _anim.SetBool("isJumping", false);

    }

}
