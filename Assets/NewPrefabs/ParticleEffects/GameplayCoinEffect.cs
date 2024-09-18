using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCoinEffect : MonoBehaviour
{
    private PlayerStats playerStats;
    private Rigidbody rb;

    public float minForce = 25f;
    public float maxForce = 200f;
    public float radius = 10f;

    private float lastTime = 0f;

    public Vector3 pos;
    public Vector3 targetPos;

    public float durationTime = 1f;
    public float speed = 5f;

    public bool isMoving = false;
   
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        lastTime = Time.time;

        _Explode();
        
    }

    // Update is called once per frame
    void Update()
    {
        _Timer();
    }

    void _Explode()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
        }
    }

    void _Timer()
    {
        float timeFromLast = Time.time - lastTime;
        if (timeFromLast >= 0.7f && isMoving == false)
        {
            isMoving = true;
            StartCoroutine(_Move());
        }
    }

    IEnumerator _Move()
    {
        pos = transform.position;

        float i = 0.0f;
        float rate = (1.0f / durationTime) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(pos, targetPos, i);
            yield return null;
        }
        if (i > 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
