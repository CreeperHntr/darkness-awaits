using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    [SerializeField] private float bulletDamage;

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(DestroyBullet());
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && this.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log("hit");
            Destroy(this.gameObject);
            other.gameObject.GetComponent<EnemyController>().UpdateHP(bulletDamage);
            
        }
        if (other.CompareTag("Stalagtite"))
        {
            Destroy(this.gameObject);
        }
    }
}
