using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float ParallaxEffect;
    public Transform cam;
    private float length, startPosX;

    void Start()
    {
        // Инициализация начальной позиции и длины объекта
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        // Вычисление дистанции с учетом параллакса
        float dist = (cam.position.x * ParallaxEffect);
        float temp = (cam.position.x * (1 - ParallaxEffect));

        // Обновление позиции объекта
        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);

        // Перенос объекта при выходе за пределы длины
        if (temp > startPosX + length)
            startPosX += length;
        else if (temp < startPosX - length)
            startPosX -= length;
    }

}
