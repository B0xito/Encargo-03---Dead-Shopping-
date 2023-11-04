using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public Transform player;

    public float mouseSensitivity = 2f;
    private float cameraVerticalRotation = 0f;

    #region ECONOMY VARIABLES
    Camera cam;
    [SerializeField] float rayDistance = 2f;
    [SerializeField] GameObject product;
    public LayerMask mask;

    [SerializeField] Transform hands;
    [SerializeField] Transform productGrabbed;
    [SerializeField] bool isDragging = false;
    #endregion

    private void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    private void Update()
    {
        #region PLAYER AND CAMERA ROTATION
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;

        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);
        #endregion

        #region PRICE READER & GRAB PRODUCTS
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.yellow);
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, mask))
        {
            product = hit.transform.gameObject;
            print(product.GetComponent<ProductData>().productName);
            print(product.GetComponent<ProductData>().productPrice);

            #region GRABP RODUCTS
            if (hit.transform.tag == "Product" && Input.GetMouseButtonDown(0))
            {
                isDragging = !isDragging;
                if (isDragging)
                {
                    productGrabbed = hit.transform;
                    productGrabbed.GetComponent<Rigidbody>().isKinematic = true;
                    productGrabbed.SetParent(hands);
                    productGrabbed.localPosition = Vector3.zero;
                }
                else
                {
                    productGrabbed.GetComponent<Rigidbody>().isKinematic = false;
                    productGrabbed.SetParent(null);
                    productGrabbed = null;
                }
            }
            #endregion
        }
        #endregion       
    }

}
