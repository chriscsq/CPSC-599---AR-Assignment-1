using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToWalk : MonoBehaviour
{
    public Animator animator;
    public float InputX;
    public float InputY;
    public float speed = 50;
    public int condition = 0;
    int buffcount = 0;
    GameObject finalcore;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        finalcore = GameObject.FindGameObjectWithTag("final-core");
        finalcore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxis("Horizontal");
        InputY = Input.GetAxis("Vertical");
        animator.SetFloat("InputX", InputX);
        animator.SetFloat("InputY", InputY);

        // Turn
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
        }

        // Attack
        if (Input.GetKey(KeyCode.Space))
        {
            animator.Play("swipe");
        }

        if (buffcount > 2)
        {
            finalcore.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Collision with other buffs
        if (other.gameObject.CompareTag("buff"))
        {
            animator.Play("swipe");
            StartCoroutine(SwingTimer(other));
        }

        // Collision with final buff
        if (other.gameObject.CompareTag("final-core"))
        {
            buffcount = 0;
            finalcore.gameObject.SetActive(false);
            transform.localScale *= 2f;
            animator.Play("jump-atk");
        }
    }

    IEnumerator SwingTimer(Collider other)
    {
        Debug.Log("buffcounter: " + buffcount);
        yield return new WaitForSeconds(1.5f);
        other.gameObject.SetActive(false);
        buffcount++;

    }

}
