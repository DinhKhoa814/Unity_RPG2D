using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;
    PickUpSpawner pickUpSpawner;
    private void Awake()
    {
        pickUpSpawner = GetComponent<PickUpSpawner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DamageSource>() || collision.gameObject.GetComponent<Projectile>()) { 
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            pickUpSpawner.DropItems();
            //GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
        }
    }

}
