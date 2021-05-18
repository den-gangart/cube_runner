using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float startSpeed = 0;
    public float maxSpeed = 20;
    public float gravity =-9.8f;
    public float jumpSpeed = 15.0f;
    [SerializeField] private Transform platform;

    private CharacterController _characterController;
    private float verticalVelocity;
    private bool _alive;
    private bool _jump;
    private bool _touchGround;
    private float minPos;
    private float maxPos;
    private float speed;
    private Animator _animator;
    private UISounds uISounds;
    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 60;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        uISounds = GameObject.FindGameObjectWithTag("UISound").GetComponent<UISounds>();
        StartCoroutine(StartMove());
        StartCoroutine(UpdateSpeed());
        _touchGround = true;
        minPos = -(platform.localScale.x - 1) / 2;
        maxPos = (platform.localScale.x - 1) / 2;
        speed = 0;
        _alive = true;
        _jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;

        if (_alive)
        {
            Vector3 movement = new Vector3();
            movement.z = speed;

            float inputX = Input.GetAxis("Horizontal");

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x < Screen.width / 2) inputX = -0.6f;
                    else inputX = 0.7f;
                }
            }


            movement.y = gravity;

            if ((inputX < 0 && transform.position.x > minPos) || (inputX > 0 && transform.position.x < maxPos))
                movement.x = inputX * speed*PlayerBonuses._speedImprove;

            transform.TransformDirection(movement);
            _characterController.Move(movement * Time.deltaTime);

            if (_characterController.isGrounded)
            {
                if(!_touchGround)
                {
                    _touchGround = true;
                    uISounds.OnFall();
                }

                if (_jump)
                {
                    uISounds.OnJump();
                    verticalVelocity = jumpSpeed*PlayerBonuses._jumpImprove;
                    _jump = false;
                    _animator.SetBool("Jump", true);
                    _touchGround = false;
                    StartCoroutine(EndJump());
                }
            }
            else if (_jump)
                _jump = false;

            Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
            _characterController.Move(jumpVector * Time.deltaTime);
            verticalVelocity += gravity*Time.deltaTime*(speed/10);
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
        if (!alive)
            uISounds.OnDestroyObject();
    }


    public bool GetAlive()
    {
        return _alive;
    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1.5f);
        speed = startSpeed;
    }

    private IEnumerator UpdateSpeed()
    {
        yield return new WaitForSeconds(2);
        if (speed < maxSpeed)
        {
            speed += 0.1f;
            StartCoroutine(UpdateSpeed());
        }
    }

    private IEnumerator EndJump()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("Jump", false);
    }

    public void LetJump()
    {
        _jump = true;
    }
}
