using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalagtiteController : MonoBehaviour
{
    private Rigidbody rb;

    private bool isInitiated = false;

    [SerializeField] private float fallSpeed;

    [SerializeField] private float destroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isInitiated)
        {
            //this is bad, dont do this lol
            Quaternion temp = transform.rotation;
            if(temp.z == 1)
            {
                rb.AddForce(Vector3.up * fallSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(Vector3.down * fallSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
            }
            StartCoroutine(DestroyUselessStalagtite());
        }
    }

    private IEnumerator DestroyUselessStalagtite()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }

    public void Initiate()
    {
        isInitiated = true;
    }
}
