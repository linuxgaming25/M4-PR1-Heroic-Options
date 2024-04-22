using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;

    public delegate void JumpingEvent();

    public event JumpingEvent playerJump;
    public bool demoKinematicMovement = false;

    public float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;

    private CapsuleCollider _col;
    private GameBehavior _gameManager;


    public GameObject bullet;
    public float bulletSpeed = 100f;

    private bool doJump = false;
    private bool doShoot = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        //if (demoKinematicMovement)
        //{
        //    MoveKinematically();
        //}

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            doShoot = true;
        }

    }

    private void FixedUpdate()
    {
        //if (demoKinematicMovement)
        //{
        //    return;
        //}


        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }

        if (doJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            doJump = false;
            playerJump();
        }

        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (doShoot)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            doShoot = false;
        }


        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);

        //if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        //{
        //    _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        //}
    }
    //void MoveKinematically()
    //{
    //    this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
    //    this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
    //}

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        //bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround);


        //grounded = false;
        Debug.Log("Grounded: " + _col.bounds.center + ", " + capsuleBottom + ", " + distanceToGround);
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name== "Enemy")
        {
            _gameManager.HP -= 25;
        }
    }
}