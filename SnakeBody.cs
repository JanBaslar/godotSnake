using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake;
public partial class SnakeBody : Sprite2D
{
	[Signal]
	public delegate void GameOverEventHandler();
	
	const double _maxTimeToReact = 0.3;
	private const int _screenWidth = 32;
	private const int _screenHeight = 16;
	private const int _pointSize = 40;
	
	private const int _rightBoundary = _screenWidth * _pointSize - _pointSize;
	private const int _bottomBoundary = _screenHeight * _pointSize - _pointSize;

	private double _time = 0;

	private String _direction;
	private List<Rect2> _body;
	private bool _eat = false;
	private bool _crash = false;
	
	public override void _Ready()
	{
		_direction = "RIGHT";
		_body = new List<Rect2>
		{
			new Rect2(0, 0, _pointSize, _pointSize),
			new Rect2(_pointSize, 0, _pointSize, _pointSize)
		};
		ZIndex = 1;
	}

	public override void _Draw()
	{
		var bodyColor = new Color(0, 1, 1);
		var headColor = new Color(1, 0.3f, 0.3f);
		var head = true;
		foreach(var rect in _body){
			if (head) {
				DrawRect(new Rect2(rect.Position.X+2, rect.Position.Y+2, 36, 36), headColor);
				head = false;
			} else {
				DrawRect(new Rect2(rect.Position.X+2, rect.Position.Y+2, 36, 36), bodyColor);
			}
		}
	}

	public bool TryEat(Berry berry)
	{
		if(_body[0].Position.X == berry.Position.X && _body[0].Position.Y == berry.Position.Y){
			_eat = true;
			return _eat;
		}
		return false;
	}

	public bool CheckHeadOnBodyCollision()
	{
		return _body.Skip(1).Any( t => {
			return t.Position.X == _body[0].Position.X && t.Position.Y == _body[0].Position.Y;
		});
	}

	public override void _Process(double delta)
	{
		_time += delta;
		if(_time > _maxTimeToReact){
			var translation = ChangeSnakeHeadPosition(_direction);
			if (_body.Count > 0){
				var newRect = new Rect2(_body[0].Position, _body[0].Size);
				newRect.Position += translation;
				if(newRect.Position.X < 0){
					_crash = true;
					EmitSignal(SignalName.GameOver);
				}
				if(newRect.Position.X > _rightBoundary){
					_crash = true;
					EmitSignal(SignalName.GameOver);
				}
				if(newRect.Position.Y < 0){
					_crash = true;
					EmitSignal(SignalName.GameOver);
				}    
				if(newRect.Position.Y > _bottomBoundary){
					_crash = true;
					EmitSignal(SignalName.GameOver);
				}

				_body.Insert(0, newRect);
				if(!_eat){
					_body.RemoveAt(_body.Count-1);
				}
				if(CheckHeadOnBodyCollision()){
					_crash = true;
					EmitSignal(SignalName.GameOver);
				}
			}
			if (!_crash){
				QueueRedraw();
			}
			_eat = false;
			_time = 0;
		}
	}
	
	private static Vector2 ChangeSnakeHeadPosition(string movementInput)
	{
		switch (movementInput)
		{
			case "UP":
				return new Vector2(0, -40);
			case "DOWN":
				return new Vector2(0, 40);
			case "LEFT":
				return new Vector2(-40, 0);
			case "RIGHT":
				return new Vector2(40, 0);
			default:
				return new Vector2(40, 0);
		}
	}

	public override void _Input(InputEvent @event)
	{
		if(@event.IsAction("ui_left") && _direction != "RIGHT")
		{
			_direction = "LEFT";
			return;
		}
		if(@event.IsAction("ui_right") && _direction != "LEFT")
		{
			_direction = "RIGHT";
			return;
		}
		if(@event.IsAction("ui_up") && _direction != "DOWN")
		{
			_direction = "UP";
			return;
		}
		if(@event.IsAction("ui_down") && _direction != "UP")
		{
			_direction = "DOWN";
			return;
		}
	}
}
