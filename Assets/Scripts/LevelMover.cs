using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{

    private Vector2 levelSize;
    // Start is called before the first frame update
    void Start()
    {
        levelSize = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -levelSize.x + 3) // 3 is offset to stop moving level
        {
            transform.position = new Vector3(transform.position.x - 5f * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
