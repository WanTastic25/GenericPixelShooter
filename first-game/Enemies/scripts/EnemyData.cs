using Godot;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export] public Texture2D Sprite { get; set; }
    [Export] public int EnemyHealth { get; set; }
    [Export] public float EnemySpeed { get; set; }

    public EnemyData()
    {
        Sprite = null;
        EnemyHealth = 0;
        EnemySpeed = 0;
    }
}
