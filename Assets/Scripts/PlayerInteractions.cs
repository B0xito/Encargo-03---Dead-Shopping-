using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField] Rigidbody playerRb;
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float playerJumpHeight = 5;
    [SerializeField] private bool isGrounded;

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
        float v = Input.GetAxis("Vertical") * playerSpeed + Time.deltaTime;
        transform.Translate(h, 0 , v);
        #endregion

        #region Jump

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.
        }
        #endregion
    }
}
