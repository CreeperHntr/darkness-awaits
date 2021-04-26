using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float hp;

    [SerializeField] private GameObject gunbarrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject bombBay;
    [SerializeField] private float speed;
    [SerializeField] private float fireTimer;
    [SerializeField] private float destroyTimer;
    [SerializeField] private int scorePointValue;


    private Slider hpBar;
    private GameObject[] pathingPoints;
    private Rigidbody rb;
    private int nextPoint = 0;
    private bool movePoint = true;
    
    private bool isAbleToFire = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hpBar = GetComponentInChildren<Slider>();
        hpBar.maxValue = hp;
        hpBar.value = hp;
        pathingPoints = GetComponentInParent<PathingSystem>().pathingPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            GlobalVars.UpdateScore(scorePointValue);
        }
    }

    private void FixedUpdate()
    {
        // pathfinding for moving to next point in waypoint system
        if(nextPoint < pathingPoints.Length - 1 && movePoint)
        {
            Vector3 tempVec = pathingPoints[nextPoint+1].transform.position - pathingPoints[nextPoint].transform.position;
            rb.velocity = Vector3.zero;
            rb.AddForce(tempVec * speed, ForceMode.VelocityChange);
            nextPoint++;
            movePoint = false;
        }
        if(isAbleToFire)
        {
            Fire();
        }

        if(this.nextPoint == pathingPoints.Length - 1)
        {
            StartCoroutine(DestroyUselessEnemies());
        }
        
    }

    private IEnumerator DestroyUselessEnemies()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }

    private void Fire()
    {
        StartCoroutine(WaitForFireTimer());
    }

    private IEnumerator WaitForFireTimer()
    {
        GameObject temp = Instantiate(bullet, gunbarrel.transform.position, bullet.transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(Vector3.left * 50f, ForceMode.Impulse);
        isAbleToFire = false;
        yield return new WaitForSeconds(fireTimer);
        isAbleToFire = true;
    }

    public void UpdateHP(float val)
    {
        if(hp < 0)
        {
            Destroy(this.gameObject);
            return;
        }
        hp -= val;
        hpBar.value = hp;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MovePoint"))
        {
            movePoint = true;
        }
    }
}
