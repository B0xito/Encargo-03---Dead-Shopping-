using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Player Variables
    Rigidbody playerRb;
    public float playerSpeed = 5f;
    public float playerJumpHeight = 5f;
    Animator playerAnim;
    [SerializeField] private bool isGrounded;
    #endregion

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        #region Movement
        float h = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        transform.Translate(h, 0 , v);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            playerAnim.SetFloat("Speed", 1);
        }
        else
        {
            playerAnim.SetFloat("Speed", 0);
        }
        #endregion

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * playerJumpHeight, ForceMode.Impulse);
        }
        #endregion
    }

    #region isGrounded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    #endregion
}
