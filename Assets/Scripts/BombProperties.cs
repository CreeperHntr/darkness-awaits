using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProperties : MonoBehaviour
{

    private static int bombReloadTimer = 3;

    public bool isAlive = true;

    [SerializeField] private float explodeTimer;
    [SerializeField] private float dmg;
    [SerializeField] private LineRenderer line;
    [SerializeField] private int segments;
    [SerializeField] private float radius;
    [SerializeField] private ParticleSystem particleSystem;

    private AudioSource source;

    private void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreateRange();
    }

    private void CreateRange()
    {
        float x;
        float y = 0f;
        float z;

        float angle = 20f;

        for (int i = 0; i < line.positionCount; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Explode();
    }

    private void Explode()
    {
        if(explodeTimer <= 0.1)
        {
            isAlive = false;
            particleSystem.transform.parent = null;
            if(!source.isPlaying)
            {
                source.PlayOneShot(source.clip);
            }
            StartCoroutine(DestroyPS());
        }
        if(explodeTimer <= 0)
        {
            particleSystem.Play();
            Destroy(this.gameObject);
        }
        explodeTimer -= 1 * Time.deltaTime;
    }

    private IEnumerator DestroyPS()
    {
        yield return new WaitForSeconds(5);
        Destroy(particleSystem);
    }

    public static int BombReloadTimer
    {
        get { return bombReloadTimer; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !isAlive)
        {
            other.GetComponent<EnemyController>().UpdateHP(dmg);
        }
    }

}
