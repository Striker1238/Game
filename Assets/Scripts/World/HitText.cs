using DG.Tweening;
using UnityEngine;

public class HitText : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]private float MoveTime = 2f;
    [Min(0f)]
    [SerializeField] private float Scatter = 0.5f;
    public void Start()
    {
        transform
            .DOMove(transform.position + Vector3.one * Random.Range(-Scatter, Scatter), MoveTime)
            .From(transform.position)
            .OnComplete(() => Destroy(gameObject));
    }
}
