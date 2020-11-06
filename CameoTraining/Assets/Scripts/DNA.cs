using UnityEngine;

public class DNA : MonoBehaviour
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float TimeToDie { get; set; } = 0;
    public bool Dead { get; set; } = false;

    private SpriteRenderer _spriteRender;
    private Collider2D _collider2D;

    private void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();

        _spriteRender.color = new Color(R, G, B);
    }

    private void OnMouseDown()
    {
        Dead = true;
        // TimeToDie = PopulationManager.elepsed;
        Debug.Log("Dead At: " + TimeToDie);
        _spriteRender.enabled = false;
        _collider2D.enabled = false;
    }
}
