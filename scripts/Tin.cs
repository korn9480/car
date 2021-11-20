using Godot;
using System;

public class Tin : KinematicBody2D
{
    [Export]
    private float wheel_base =50;  // Distance from front to rear wheel
    [Export]
    private float steering_angle =20  ;  // Amount that front wheel turns, in degrees
    [Export]
    private int speed=400;
    private Vector2 velocity = Vector2.Zero;
    private float steer_angle;
    string star;
    public override void _Ready()
    {
        GD.Print("Tin");
        // var star = GetNode<Node2D>("Node2D");
        // star.Connect("get_control",this,nameof(get_input));
    }
    public void get_input(string star){
        int turn = 0;
        if (Input.IsActionPressed("Tin_right")){
            turn += 1;
        }
        if (Input.IsActionPressed("Tin_letf")){
            turn -= 1;
        }
        steer_angle = turn * Mathf.Deg2Rad(steering_angle);
        velocity = Vector2.Zero;
        if (Input.IsActionPressed("Tin_up")){
            velocity =Transform.x * speed;
        }
    }
    public void calculate_steering(float delta){
        Vector2 rear_wheel = Position - Transform.x * wheel_base / 2.0f;
        Vector2 front_wheel = Position + Transform.x * wheel_base / 2.0f;
        rear_wheel += velocity * delta;
        front_wheel += velocity.Rotated(steer_angle) * delta;
        Vector2 new_heading = (front_wheel - rear_wheel).Normalized();
        velocity = new_heading * velocity.Length();
        Rotation = new_heading.Angle();
    }
    public override void _PhysicsProcess(float delta){
        get_input(star);
        calculate_steering(delta);
        velocity=MoveAndSlide(velocity);
    }
}
