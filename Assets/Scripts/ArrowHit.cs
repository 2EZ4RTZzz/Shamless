using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public GameObject arrowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Shoot()
    {
        Instantiate(arrowPrefab,transform.position,transform.rotation);
    }

}
