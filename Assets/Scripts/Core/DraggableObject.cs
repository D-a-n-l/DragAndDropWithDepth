using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableObject : MonoBehaviour
{
    [SerializeField]
    private float _offsetForFall = 0;

    private Rigidbody2D _rigidbody;

    private Vector3 _offset;

    public bool IsDragging { get; private set; } = false;

    public UnityEvent OnMouseDowned;

    public UnityEvent OnMouseUpped;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.gravityScale = 0;
    }

    private void OnMouseDown()
    {
        IsDragging = true;

        _rigidbody.gravityScale = 0;

        _rigidbody.linearVelocity = Vector2.zero;

        _offset = transform.position - GetMouseWorldPosition();

        OnMouseDowned?.Invoke();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + _offset;
    }

    private void OnMouseUp()
    {
        IsDragging = false;

        if (transform.position.y >= _offsetForFall)
        {
            _rigidbody.gravityScale = 1;
        }

        OnMouseUpped?.Invoke();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}