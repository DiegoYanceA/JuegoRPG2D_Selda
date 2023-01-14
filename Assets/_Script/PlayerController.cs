using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static bool playerCreated;
    public bool canMove = true;
    public bool isTalking = false;

    public float speed = 3.0f;
    private bool walking = false;
    private bool attacking = false;
    public Vector2 lastMovement = Vector2.zero;
l
    private const string AXIX_H = "Horizontal";
    private const string AXIX_V = "Vertical";
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public string nextUuid;

    public float attackTime;
    private float attackTimeCounter;

    void Awake()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
    }


    // Start is called before the first frame update
    void Start()
    {
        
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        playerCreated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        walking = false;

        if (!canMove)
            return;

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if(attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool("Attacking", false);
            }
        } else if (Input.GetKeyDown("j"))
        {
            attacking = true;
            attackTimeCounter = attackTime;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool("Attacking", true);
        }

        if (0.2f < Mathf.Abs(Input.GetAxisRaw(AXIX_H)))
        {
            //Vector3 translation = new Vector3(Input.GetAxisRaw(AXIX_H) * speed * Time.deltaTime, 0, 0);
            //this.transform.Translate(translation);
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIX_H), _rigidbody.velocity.y).normalized * speed;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIX_H), 0);
            walking = true;
        }

        if (0.2f < Mathf.Abs(Input.GetAxisRaw(AXIX_V)))
        {
            //Vector3 translation = new Vector3(0, Input.GetAxisRaw(AXIX_V) * speed * Time.deltaTime, 0);
            //this.transform.Translate(translation);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Input.GetAxisRaw(AXIX_V)).normalized * speed;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIX_V));
            walking = true;
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            _rigidbody.velocity = Vector2.zero;
        }

        if (Input.GetButtonUp("Vertical"))
        {
            _rigidbody.velocity = Vector2.zero;
        }

    }

    private void LateUpdate()
    {
        if (!walking)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        _animator.SetFloat(AXIX_H, Input.GetAxisRaw(AXIX_H));
        _animator.SetFloat(AXIX_V, Input.GetAxisRaw(AXIX_V));

        _animator.SetFloat("LastH", lastMovement.x);
        _animator.SetFloat("LastV", lastMovement.y);
        _animator.SetBool("Walking", walking);
    }
}
