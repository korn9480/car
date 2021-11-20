using Godot;
using System;

public class Map : Node2D
{
    [Signal]
    public delegate void NamePlayer(string name);

    public override void _Ready()
    {
        GD.Print("map");
    }
    public void PassPositionStar(Node body){
        if (body.Name!="TileMap"){
            EmitSignal(nameof(NamePlayer),body.Name);
        }
    }
}
