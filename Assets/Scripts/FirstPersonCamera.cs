using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstPersonCamera : MonoBehaviour
{

    public Transform player;

    public float mouseSensitivity = 2f;
    private float cameraVerticalRotation = 0f;

    #region ECONOMY VARIABLES
    Camera cam;
    [SerializeField] float rayDistance;
    [SerializeField] GameObject product;
    public LayerMask mask;

    [SerializeField] Transform hands;
    [SerializeField] Transform productGrabbed;
    [SerializeField] bool isDragging = false;
    #endregion

    [SerializeField] Transform lower;
    [SerializeField] Transform cart;
    [SerializeField] bool drivingCart = false;

    public GameObject pricePanel;
    public TMP_Text productNameTxt;
    public TMP_Text priceText;

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
            if (hit.transform.GetComponent<ProductData>())
            {
                product = hit.transform.gameObject;
                productNameTxt.text = product.GetComponent<ProductData>().productName;
                priceText.text = product.GetComponent<ProductData>().productPrice.ToString();
                pricePanel.SetActive(true);
            }

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

            if (hit.transform.tag == "Shopping Cart" && Input.GetKeyDown(KeyCode.F))
            {
                drivingCart = !drivingCart;
                if (drivingCart)
                {
                    cart = hit.transform;
                    cart.GetComponent<Rigidbody>().isKinematic = true;
                    cart.SetParent(lower);
                    cart.localPosition = Vector3.zero;
                }
                else 
                {
                    cart.GetComponent<Rigidbody>().isKinematic = false;
                    cart.SetParent(null);
                    cart = null;
                }
            }
        }
        else
        {
            pricePanel.SetActive(false);
        }
        #endregion       
    }

}
