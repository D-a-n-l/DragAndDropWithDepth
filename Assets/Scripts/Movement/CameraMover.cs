using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f; // Скорость движения камеры

    [SerializeField]
    private MinMax _border;

    [Range(0, .5f)]
    [SerializeField]
    private float _screenEdgePercent; // % экрана, где активен контроль

    private int _screenWidth;

    private bool _isMovingLeft = false;

    private bool _isMovingRight = false;

    private void Start()
    {
        _screenWidth = Screen.width;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float touchX = touch.position.x;

            float leftBoundary = _screenWidth * _screenEdgePercent;
            float rightBoundary = _screenWidth * (1 - _screenEdgePercent);

            _isMovingLeft = touchX <= leftBoundary;
            _isMovingRight = touchX >= rightBoundary;
        }
        else
        {
            _isMovingLeft = false;
            _isMovingRight = false;
        }

        MoveCamera();
    }

    private void MoveCamera()
    {
        float direction = 0;

        if (_isMovingLeft == true)
            direction = -1;
        else if (_isMovingRight == true)
            direction = 1;

        Vector3 newPosition = transform.position + new Vector3(direction * _speed * Time.deltaTime, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, _border.Min, _border.Max);
        transform.position = newPosition;
    }
}