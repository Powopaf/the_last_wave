using System;
using UnityEngine;

public abstract class Zombie: MonoBehaviour
{
    protected int _health;
    protected int _damage;
    private string _name;
    //private Item[] _loot;
    private string[] _target;
    private (int, int) _coordinate;
    public float _speed;
    public Transform playerobject; //On doit pouvoir changer l'objet avec la fonction TargetZombie()
    public Rigidbody2D rb;
    protected Vector2 movement;

    protected Zombie(string name = "", string[] target = null,
        int health = 1, int damage = 1, float speed = 1f)
    {
        _name = name;
        _target = target;
        _health = health;
        _damage = damage;
        _speed = speed;
    }

   
    
    protected string TargetZombie()
    {
        Vector3 closertargetPosition = GameObject.Find(_target[0]).transform.position;
        string result = _target[0];
        Vector3 firstcomparison;
        Vector3 secondcomparison;
        for (int i = 1; i < _target.Length; i++)
        {
            Vector3 newtargetPosition = GameObject.Find(_target[i]).transform.position;
            firstcomparison = transform.position - closertargetPosition;
            secondcomparison = transform.position - newtargetPosition;
            if (secondcomparison.magnitude < firstcomparison.magnitude)
            {
                closertargetPosition = newtargetPosition;
                result = _target[i];
            }
        }

        return result;
    }

    protected abstract void Update();
    

    protected abstract void Start();

    protected abstract void FixedUpdate();
    protected abstract void ZombieMovement(Vector2 direction);
}
