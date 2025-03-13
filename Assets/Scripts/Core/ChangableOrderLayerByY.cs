using UnityEngine;

public class ChangableOrderLayerByY : MonoBehaviour
{
    [SerializeField]
    private Transform _root;

    [SerializeField]
    private bool _isUpdate = false;

    [Space(10)]
    [SerializeField]
    private PresetSpriteRenderer[] _sprites;

    private const int Multiply = 100;

    private void OnEnable()
    {
        if (_isUpdate == true)
            return;

        InvokeRepeating(nameof(Set), 0.1f, 5f);
    }

    private void Update()
    {
        if (_isUpdate == false)
            return;

        Set();
    }

    public void Set()
    {
        float orderLayer = -_root.position.y * Multiply;

        foreach (PresetSpriteRenderer sprite in _sprites)
        {
            sprite.spriteRenderer.sortingOrder = (int) orderLayer + sprite.offset;
        }
    }
}

[System.Serializable]
public struct PresetSpriteRenderer
{
    public SpriteRenderer spriteRenderer;

    public int offset;
}