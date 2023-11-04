using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Player Variables
    Rigidbody playerRb;
    public float playerSpeed = 5f;
    public float playerJumpHeight = 5f;
    [SerializeField] private bool isGrounded;
    #endregion

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
