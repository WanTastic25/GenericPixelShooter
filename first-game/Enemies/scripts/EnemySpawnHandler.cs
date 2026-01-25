using Godot;
using System;

public partial class EnemySpawnHandler : Node2D
{
	[Export] public PackedScene enemyGameObject;
    public bool enemyCanSpawn = true;
    public Area2D spawnArea;
    public CollisionShape2D spawnAreaShape;
    public RectangleShape2D shape2D;
    public RandomNumberGenerator rng;

    public override void _Ready()
	{
        rng = new RandomNumberGenerator();
        spawnArea = GetNode<Area2D>("Area2D");
        spawnAreaShape = spawnArea.GetNode<CollisionShape2D>("spawnArea");
        shape2D = spawnAreaShape.Shape as RectangleShape2D;
    }

	public override void _Process(double delta)
	{        
        Timer spawnEnemyTimer = GetNode<Timer>("enemySpawnTimer");

        //enemyCanSpawn = false;

        if (enemyCanSpawn)
        {
            var enemy = enemyGameObject.Instantiate<RigidBody2D>();
            enemy.Position = GetRandomSpawnPosition();
            enemy.AddToGroup("enemy");
            GetTree().CurrentScene.AddChild(enemy);
            enemyCanSpawn = false;
            spawnEnemyTimer.Start();
        }
    }

    public Vector2 GetRandomSpawnPosition()
    {
        Vector2 worldSize = shape2D.Size * spawnAreaShape.GlobalScale;
        Vector2 worldSizeHalf = worldSize / 2f;

        Vector2 localOffset = new Vector2(
            rng.RandfRange(-worldSizeHalf.X, worldSizeHalf.X),
            rng.RandfRange(-worldSizeHalf.Y, worldSizeHalf.Y)
        );

        return spawnArea.GlobalPosition + localOffset;
    }

    private void _on_enemy_spawn_timer_timeout()
	{
        enemyCanSpawn = true;
	}
}
