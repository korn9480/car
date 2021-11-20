using Godot;
using System;
// using System.Threading;
public class Root : Node2D
{
    [Signal]
    public delegate void get_contro(string player_star);
    public string player_star;

    LineEdit round;
    Label name_player;
    Label one;
    Label go;
    Timer timer;

    bool result_star=false;
    int number=1;
    public override void _Ready()
    {
        round = GetNode<LineEdit>("Round/round");
        var map = GetNode<Node2D>("Map");
        map.Connect("NamePlayer",this,nameof(NumberWin));
        one = GetNode<Label>("UI/One");
        go =GetNode<Label>("UI/Go");
        timer = GetNode<Timer>("UI/Timer");
        GD.Print("Main");
    }
    public void TimeSleep(){
        string[] time_star={"3","2","1","Go"};
        foreach(string i in time_star){
            if (timer.OneShot==false){
                go.Text=i;
                GD.Print("false");
                timer.OneShot=true;
            }
            if (timer.OneShot==true) {
                timer.OneShot=false;
            }
            
        }
    }
    public void StarGame(){
        TimeSleep();
        GetNode<Label>("Round/Kon").Text = "1/"+round.Text;
        GetNode<Label>("Round/Tin").Text = "1/"+round.Text;
        player_star ="star";
        EmitSignal(nameof(get_contro),"player_star");
    }

    public void Clear(){
        GetTree().ReloadCurrentScene();
    }

    public void NumberWin(string name){
        GD.Print(name);
        name_player = GetNode<Label>("Round/"+name);
        string round_str = name_player.Text[0].ToString();  
        if (round_str == round.Text){
            if (one.Text.Contains(name)){
            
            } 
            else {
                one.Text += number +"."+name+"\n";
                number+=1;
            }
        }
        else {
            int round_int = 1+Convert.ToInt32(Convert.ToDouble(round_str));
            name_player.Text =round_int +"/"+round.Text; 
        }
    }

}
