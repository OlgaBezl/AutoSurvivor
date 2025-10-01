using Scripts.Heroes;
using UnityEngine;

//[RequireComponent (typeof(Rigidbody2D))]
public class HeroMover : MonoBehaviour
{
    public Vector3 Direction { get; private set; } = Vector3.right;

    private Hero _hero;
    private HeroItem _heroItem;
    private bool _isMove;
    private bool _right = true;
    public void Initialize(Hero hero)
    {
        _hero = hero;
    }

    public void StartMove()
    {
        _isMove = true;
    }

    public void Stop()
    {
        _isMove = false;
    }

    private void Update()
    {
        if (_isMove)
        {
            Direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            if(Direction.x != 0)
            {
                if (_right != Direction.x > 0)
                    _hero.Turn(Direction);

                _right = Direction.x > 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            //_rigidbody.MovePosition(_rigidbody.position + Direction * _heroItem.Speed * Time.fixedDeltaTime);
            transform.position += Direction.normalized * _hero.HeroItem.Speed * Time.fixedDeltaTime;
        }
    }
}
