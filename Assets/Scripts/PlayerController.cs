using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float mouseSensitivity;

    private Weapon heldWeapon;

    float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();

        if (Input.GetButtonDown("Fire1") && heldWeapon != null)
        {
            FireWeapon();
        }
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movmentVector = Vector3.zero;
        movmentVector += transform.forward * v * speed;
        movmentVector += transform.right * h * speed;

        characterController.SimpleMove(movmentVector);
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void FireWeapon()
    {
        heldWeapon.Fire();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            heldWeapon = collision.gameObject.GetComponent<Weapon>();
            Destroy(collision.gameObject.GetComponent<Collider>());
        }
    }
}
