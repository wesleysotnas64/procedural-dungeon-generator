using UnityEngine;

public class CellCave : MonoBehaviour
{
    private bool isWall;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetToWall()
    {
        IsWall(true);
        _spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1.0f);
    }

    public void SetToEmpty()
    {
        IsWall(false);
        _spriteRenderer.color = new Color(0.2f, 0.4f, 0.7f, 0.0f);

    }

    public void IsWall(bool wall)
    {
        isWall = wall;
    }

    public bool IsWall()
    {
        return isWall;
    }
}
