using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToWalk : MonoBehaviour
{
    public Animator animator;
    public float InputX;
    public float InputY;
    public float speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxis("Horizontal");
        InputY = Input.GetAxis("Vertical");
        animator.SetFloat("InputX", InputX);
        animator.SetFloat("InputY", InputY);
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
        }
    }




    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("buff"))
        {
            Debug.Log("entered");
            collision.gameObject.SetActive(false);
        }
    }

}
