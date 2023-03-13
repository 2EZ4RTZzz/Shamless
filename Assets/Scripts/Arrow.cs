using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed; //original speed
    public int damage;
    public float destoryDistance; //fly x distance will destory itself

    private Rigidbody2D rb2d;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if(distance> destoryDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamge(damage);
        }
    }
}