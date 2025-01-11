using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float ParallaxEffect;
    public Transform cam;
    private float length, startPosX;

    void Start()
    {
        // ������������� ��������� ������� � ����� �������
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        // ���������� ��������� � ������ ����������
        float dist = (cam.position.x * ParallaxEffect);
        float temp = (cam.position.x * (1 - ParallaxEffect));

        // ���������� ������� �������
        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);

        // ������� ������� ��� ������ �� ������� �����
        if (temp > startPosX + length)
            startPosX += length;
        else if (temp < startPosX - length)
            startPosX -= length;
    }

}
