using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public GameObject arrowPrefab;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }

    void attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("attack_1");
            Shoot();
            //Debug.Log("test123");
            // anim.SetTrigger("attack_1");
            // StartCoroutine(StartAttack());
        }
    }
    void Shoot()
    {

        Instantiate(arrowPrefab, transform.position, transform.rotation);
    }

}
