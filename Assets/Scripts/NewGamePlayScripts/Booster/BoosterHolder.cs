using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterHolder : MonoBehaviour
{
    public Vector3 movePos;
    public Vector3 originPos;

    public float durationTime = 2f;
    public float speed = 5f;

    public bool isMoving = false;
    public bool isUsing = false;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == false && isUsing == true && transform.position == originPos)
        {
            StartCoroutine(_Move(transform.position, movePos));
        }
        else
        if(isMoving == false && isUsing == false && transform.position == movePos)
        {
            StartCoroutine(_Move(transform.position, originPos));
        }
    }

    IEnumerator _Move(Vector3 currentPos ,Vector3 targetPos)
    {
        isMoving = true;
        float i = 0.0f;
        float rate = (1.0f / durationTime) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localPosition = Vector3.Lerp(currentPos, targetPos, i);
            yield return null;
        }

        if(transform.position == targetPos)
        {
            isMoving = false;
        }
    }
}
