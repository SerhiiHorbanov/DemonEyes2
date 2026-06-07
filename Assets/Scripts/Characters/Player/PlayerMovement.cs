using UnityEngine;

namespace Characters.Player
{
	public abstract class PlayerMovement : MonoBehaviour
	{
		protected Vector2 MoveDirection;
		
		public void SetMoveDirection(Vector2 value)
			=> MoveDirection = value;
	}
}