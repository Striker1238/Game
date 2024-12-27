using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitText : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(task());
    }
    public IEnumerator task()
    {
        Vector3 StartPosition = transform.position;
        Vector3 EndPosition = new Vector2(StartPosition.x + Random.RandomRange(-0.5f, 0.5f), StartPosition.y + Random.RandomRange(-0.5f, 0.5f));
        for (int i = 0; i < 50; i++)
        {
            transform.position = Vector3.Lerp(StartPosition, EndPosition, i/100f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
