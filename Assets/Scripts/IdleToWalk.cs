﻿using System.Collections;
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
            animator.SetInteger("Condition", 1);
            animator.Play("swipe");
        }

        if (buffcount > 3)
        {
            finalcore.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if (other.gameObject.CompareTag("buff"))
        {
            animator.Play("swipe");
            StartCoroutine(SwingTimer(other));
            buffcount++;
            Debug.Log("buffcount is at: " + buffcount);

        }

        if (other.gameObject.CompareTag("final-core"))
        {
            Debug.Log("game over");
        }
    }
    
    IEnumerator SwingTimer(Collider other)
    {
        yield return new WaitForSeconds(1.5f);
        other.gameObject.SetActive(false);

    }

}
