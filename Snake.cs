using Godot;
using System;
using System.Timers;

///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake;
public partial class Snake : Node2D
{
	private const int _screenWidth = 32;
	private const int _screenHeight = 16;
	
	private SnakeBody _snakeBody;
	private Berry _berry;
	
	private string movementInput = "RIGHT";
	private static Random randomNumber = new(); //Used to randomize Berry position
	private int _gameScore = 5;
	
	private const int _pointSize = 40;
	private Vector2I _gameSize = new (_screenWidth, _screenHeight);
	
	public override void _Ready()
	{		
		_snakeBody = GetNode<SnakeBody>("SnakeBody");
		_snakeBody.Position = new Vector2(0,0);

		_berry = GetNode("Berry") as Berry;
		_berry.Position = new Vector2(randomNumber.Next(_gameSize.X) * _pointSize, randomNumber.Next(_gameSize.Y) * _pointSize);

		_snakeBody.GameOver += OnGameOver;
	}

	public override void _Process(double delta) //This is the game loop
	{
		if(_berry is not null){
			if(_snakeBody.TryEat(_berry)){
				_gameScore++;
				RepositionBerry();
			}
		}
	}

	public void OnGameOver() {
			if (_berry is not null){
				RemoveChild(_berry);
			}
	}

	public void RepositionBerry()
	{
		RemoveChild(_berry);
		_berry = null;
		_berry = new Berry
		{
			Position = new Vector2(randomNumber.Next(0, 15) * _pointSize, randomNumber.Next(0, 8) * _pointSize)
		};
		CallDeferred("add_child", _berry);
	}

}
