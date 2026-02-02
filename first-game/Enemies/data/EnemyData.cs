using Godot;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export] public SpriteFrames SpriteFrames { get; set; }
    [Export] public int EnemyHealth { get; set; }
    [Export] public float EnemySpeed { get; set; }

    public EnemyData()
    {
        SpriteFrames = null;
        EnemyHealth = 0;
        EnemySpeed = 0;
    }
}
