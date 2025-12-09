using Godot;
using System;

[GlobalClass]
public partial class PlayerStats : Resource
{
    [Export] public int MaxHealth = 100;
    [Export] public int Health = 100;
}
