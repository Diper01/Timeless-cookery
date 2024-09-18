using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCrystalEffect : MonoBehaviour
{
    private PlayerStats playerStats;

    public Vector3 pos;
    public Vector3 targetPos;

    public float durationTime = 1f;
    public float speed = 5f;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        StartCoroutine(_Move());
    }

    // Update is called once per frame
    void Update()
    {

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
