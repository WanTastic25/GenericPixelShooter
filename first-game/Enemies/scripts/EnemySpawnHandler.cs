using Godot;
using System;

public partial class EnemySpawnHandler : Node2D
{
	[Export] public PackedScene enemyGameObject;
    [Export] public CollisionShape2D spawnArea;
    public bool enemyCanSpawn = true;

    public override void _Ready()
	{
    }

	public override void _Process(double delta)
	{        
        Timer spawnEnemyTimer = GetNode<Timer>("enemySpawnTimer");
        RectangleShape2D spawnAreaShape = spawnArea.Shape as RectangleShape2D;

        float randX = (float)GD.RandRange(-450, 450);
        float randY = (float)GD.RandRange(-250, 250);

        Vector2 spawningPoint = new Vector2(randX, randY);

        enemyCanSpawn = false;

        if (enemyCanSpawn)
        {
            var enemy = enemyGameObject.Instantiate<RigidBody2D>();
            enemy.Position = spawningPoint;
            enemy.AddToGroup("enemy");
            GetTree().CurrentScene.AddChild(enemy);
            enemyCanSpawn = false;
            spawnEnemyTimer.Start();
        }
    }

	private void _on_enemy_spawn_timer_timeout()
	{
        enemyCanSpawn = true;
	}
}
