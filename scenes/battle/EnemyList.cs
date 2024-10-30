using Godot;
using System.Linq;
using System.Collections.Generic;

public partial class EnemyList : Control
{
    [Export]
    private Actor player;

    private Actor[] enemies;
    private Actor activeTarget;

    public override void _Ready()
    {
        FindEnemyChildren();
    }

    private void FindEnemyChildren()
    {
        var result = new List<Actor>();
        foreach (var child in GetChildren())
        {
            if (child is Actor enemy)
            {
                result.Add(enemy);
                // set active target if we don't already have one
                if (activeTarget == null || activeTarget.HealthComponent.IsDead)
                {
                    activeTarget = enemy;
                }
            }
        }
        enemies = result.ToArray();
    }

    public void TakeTurn()
    {
        // attack from all enemies
        foreach (var enemy in enemies)
        {
            player.Damage(2); // really stupid enemy damage
        }
    }

    public void DamageActiveTarget(int amount)
    {
        if (activeTarget != null)
        {
            activeTarget.Damage(amount);
            if (activeTarget.HealthComponent.IsDead)
            {
                activeTarget.QueueFree();
                FindEnemyChildren();
            }
        }
    }

    public bool AreAllDead => enemies.All(enemy => enemy.HealthComponent.IsDead);

}
