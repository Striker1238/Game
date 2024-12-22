using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public float ParallaxEffect;
    public GameObject cam;
    private float StartPosition, length;

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        StartPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - ParallaxEffect));
        float dist = cam.transform.position.x * ParallaxEffect;

        transform.position = new Vector3(StartPosition + dist, transform.position.y, transform.position.z);

        if (temp > StartPosition + length) StartPosition += length;
        else if(temp < StartPosition - length) StartPosition -= length;
    }
}
