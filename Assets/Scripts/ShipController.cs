using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    
    [SerializeField] private float fireTimer = .085f;
    private bool isAbleToFire = true;
    private bool isAbleToBomb = true;

    private Vector2 screenBounds;

    private Rigidbody rb;
    [SerializeField] private float hp;
    [SerializeField] private Text hpText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text depthText;
    [SerializeField] private Text bombText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject gunbarrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject bombBay;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip dmgClip;
    [SerializeField] private ParticleSystem particleSystem;

    private float shipSpeed;
    private AudioSource source;

    private int currentSceneIndex;

    void Awake()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        shipSpeed = 1500f;
        hpText.text = hp.ToString();
        scoreText.text = "0";
        GlobalVars.SetIsWinner(false);
        GlobalVars.ResetDepth();
        GlobalVars.ResetScore();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelText.text = currentSceneIndex.ToString();
    }

    void Update()
    {
        if(hp <= 0)
        {
            hp = 0;
            UpdateHP();

            GlobalVars.SetIsWinner(false);
            GlobalVars.AddToTotalScore();
            GlobalVars.AddToTotalDepth();
            SceneManager.LoadScene("End");
        }
        UpdateScore();
        GlobalVars.IncreaseDepth();
        depthText.text = GlobalVars.GetDepth().ToString("F0");
        Bomb();
    }

    private void FixedUpdate()
    {
        Movement();
        Fire();
    }

    private void LateUpdate()
    {
        if(this.gameObject.CompareTag("Player"))
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, -screenBounds.x);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, -screenBounds.y);
            transform.position = viewPos;
        }
    }

    private void Movement()
    {
        // up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.up * shipSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        //down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.down * shipSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        // left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * shipSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        // right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * shipSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        rb.velocity = Vector3.zero;
    }

    private void Fire()
    {
        if(Input.GetKey(KeyCode.C) && isAbleToFire)
        {
            StartCoroutine(WaitForFireTimer(fireTimer));
        }
    }

    private IEnumerator WaitForFireTimer(float timer)
    {
        GameObject temp = Instantiate(bullet, gunbarrel.transform.position, bullet.transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(Vector3.right * 50f, ForceMode.Impulse);
        source.PlayOneShot(fireClip);
        isAbleToFire = false;
        yield return new WaitForSeconds(timer);
        isAbleToFire = true;
    }

    private void Bomb()
    {
        if(Input.GetKey(KeyCode.X) && isAbleToBomb)
        {
            StartCoroutine(BombTimer());
        }
    }

    private IEnumerator BombTimer()
    {
        isAbleToBomb = false;
        Instantiate(bomb, bombBay.transform.position, bomb.transform.rotation);
        bombText.text = "Reloading";
        yield return new WaitForSeconds(BombProperties.BombReloadTimer);
        bombText.text = "Ready!";
        isAbleToBomb = true;
    }

    private void UpdateHP()
    {
        hpText.text = hp.ToString();
        source.PlayOneShot(dmgClip);
        particleSystem.Play();
    }

    private void UpdateScore()
    {
        scoreText.text = GlobalVars.GetScore().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnTrigger"))
        {
            other.GetComponentInParent<PathingSystem>().SetIsAllowedToSpawn(true);
            other.enabled = false;
        }
        if(other.CompareTag("FallingTrigger"))
        {
            other.GetComponentInParent<StalagtiteController>().Initiate();
            other.enabled = false;
        }
        if(other.CompareTag("Stalagtite"))
        {
            hp -= 10;
            UpdateHP();
        }
        if(other.CompareTag("EnemyBullet"))
        {
            hp -= 5;
            Destroy(other.gameObject);
            UpdateHP();
        }

        if(other.CompareTag("End"))
        {
            if(currentSceneIndex + 1 == 3)
            {
                GlobalVars.SetIsWinner(true);
                SceneManager.LoadScene("End");
            }
            GlobalVars.AddToTotalDepth();
            GlobalVars.AddToTotalScore();
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
