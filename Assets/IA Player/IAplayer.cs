using System;
using System.Collections;
using System.Collections.Generic;
using ATH.HealthBar;
using Photon.Pun;
using Players.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Pathfinding;


public abstract class IAplayer : MonoBehaviour
{
    public float speed;
    private Vector2 _dir = Vector2.zero;
    [SerializeField] private HealthBar healthBar;
    public Rigidbody2D rb;
    protected readonly string[] Target;
    private (int, int) _coordinate;
    protected Vector2 Movement;
    public Animator animator; 
    
   
    private int _money;
    private InputAction _giveMoney;
    private InputAction _upgradeInv;
    
    protected AIPath AI;
    public AIDestinationSetter AIsetter;
    
    
    public Text helmetText;
    public Text chestPlateText;
    public Text glovesText;
    public Text bootsText;
    public Text swordText;
    public Text moneyText;
    
    public int nbTree;
    public int nbRock;
    private bool _canBeFarm;
    private Collider2D _farmingElt;
    private float _attackTimeCounter;
    private double _attackTime;
    private bool _attacking;
    private bool _walking;
    private float _lastMoveX;
    private float _lastMoveY;

    protected void Awake()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            _attackTime = 0.5;
            _attacking = false;
        }

    }

    protected void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            animator.SetFloat("X", _dir.x);
            animator.SetFloat("Y", _dir.y);
            if (_attacking)
            {
                _attackTime -= Time.deltaTime;
                if (_attackTime <= 0)
                {
                    animator.SetBool("Attack", false);
                    _attackTime = 0.5;
                    _attacking = false;
                }
            }

        }

    }
    protected void FixedUpdate()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            _dir = new Vector2(5, 5);
            rb.velocity = new Vector2(_dir.x * speed, _dir.y * speed);
            if (rb.velocity != Vector2.zero)
            {
                animator.SetBool("Walking", true);
                _walking = true;
                _lastMoveX = _dir.x;
                _lastMoveY = _dir.y;
            }
            else if (_walking)
            {
                animator.SetBool("Walking" , false);
                animator.SetFloat("LastMoveX", _lastMoveX);
                animator.SetFloat("LastMoveY",_lastMoveY);
            }
        }
    }

    private void Give(InputAction.CallbackContext context)
    {
        _money += 10;
        moneyText.text = _money.ToString();
    }
    
    
    


}


