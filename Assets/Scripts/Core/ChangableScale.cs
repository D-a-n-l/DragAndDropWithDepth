using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ChangableScale : MonoBehaviour
{
    [SerializeField]
    private DraggableObject _draggable;

    [Space(10)]
    [SerializeField]
    private MinMax _offsetY;

    [SerializeField]
    private MinMax _scale;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnFalled;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rigidbody.linearVelocityY == 0 && _draggable.IsDragging == false)
        {
            float depth = Mathf.InverseLerp(_offsetY.Min, _offsetY.Max, transform.position.y);

            float scale = Mathf.Lerp(_scale.Min, _scale.Max, depth);

            transform.localScale = new Vector3(scale, scale, scale);

            OnFalled?.Invoke();
        }
    }
}