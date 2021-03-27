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
        if (heldWeapon != null)
        {
            AimWeapon();
        }

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

        if (heldWeapon != null)
        {
            // Weapon rotation. #FornowTotallyNotGonnaLeaveItHere.
            heldWeapon.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        transform.Rotate(Vector3.up * mouseX);
    }

    void FireWeapon()
    {
        heldWeapon.Fire();
    }

    void PickUpWeapon(Weapon weapon)
    {
        heldWeapon = weapon;
        weapon.gameObject.transform.SetParent(gameObject.transform);
        weapon.gameObject.transform.SetPositionAndRotation(new Vector3(transform.position.x + 0.95f, transform.position.y + 0.3f, transform.position.z), transform.rotation);
    }

    void AimWeapon()
    {
        // THIS CODE WAS KINDA SCUFFED. WAS SUPPOSED TO "AIM" You can uncomment. If you dare >:)
        //RaycastHit raycastHit = new RaycastHit();
        //Physics.Raycast(transform.position, transform.forward, out raycastHit, 100f);

        //if (raycastHit.point != Vector3.zero)
        //{
        //    heldWeapon.gameObject.transform.LookAt(raycastHit.point, Vector3.up);
        //}
        //else
        //{
        //    heldWeapon.gameObject.transform.rotation = transform.rotation;
        //}
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Destroy(collision.gameObject.GetComponent<Collider>());
            PickUpWeapon(collision.gameObject.GetComponent<Weapon>());
        }
    }
}
