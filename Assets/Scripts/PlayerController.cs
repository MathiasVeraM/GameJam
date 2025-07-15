using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Referencias")]
    public Transform cameraTransform;

    [Header("Crouch")]
    public bool isCrouching = false;
    public float crouchSpeed = 2f;
    public float normalSpeed = 5f;
    public float crouchHeight = 1f;
    public float normalHeight = 2f;

    [Header("Inventario")]
    public int maxItems = 2;
    private List<ItemType> inventory = new List<ItemType>();

    private CapsuleCollider capsule;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private GameObject nearbyPickup;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
        moveSpeed = normalSpeed;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TryPickupItem();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            TryCraftItem();
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        moveDirection = camForward * vertical + camRight * horizontal;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Movimiento
            Vector3 targetVelocity = moveDirection.normalized * moveSpeed;
            Vector3 velocity = rb.linearVelocity;
            Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Rotación
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        // Si no hay input, frenar el movimiento gradualmente
        if (moveDirection.magnitude < 0.1f)
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.x *= 0.9f; // o usa Mathf.Lerp(velocity.x, 0, 0.1f)
            velocity.z *= 0.9f;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crafteable"))
        {
            nearbyPickup = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crafteable") && other.gameObject == nearbyPickup)
        {
            nearbyPickup = null;
        }
    }

    void TryPickupItem()
    {
        if (nearbyPickup != null && inventory.Count < maxItems)
        {
            ItemData itemData = nearbyPickup.GetComponent<ItemData>();
            if (itemData != null)
            {
                inventory.Add(itemData.itemType);
                Debug.Log("Objeto recogido: " + itemData.itemType);

                nearbyPickup.SetActive(false);
                nearbyPickup = null;
            }
        }
        else if (inventory.Count >= maxItems)
        {
            Debug.Log("Inventario lleno.");
        }
    }

    void TryCraftItem()
    {
        if (inventory.Contains(ItemType.Alambre) && inventory.Contains(ItemType.Tornillo))
        {
            inventory.Remove(ItemType.Alambre);
            inventory.Remove(ItemType.Tornillo);
            inventory.Add(ItemType.Ganzua);
            Debug.Log("¡Has crafteado una ganzúa!");
        }
        else if (inventory.Contains(ItemType.Metal) && inventory.Contains(ItemType.Madera))
        {
            inventory.Remove(ItemType.Metal);
            inventory.Remove(ItemType.Madera);
            inventory.Add(ItemType.Palanca);
            Debug.Log("¡Has crafteado una palanca!");
        }
        else if (inventory.Contains(ItemType.Alambre) && inventory.Contains(ItemType.Destornillador))
        {
            inventory.Remove(ItemType.Alambre);
            inventory.Remove(ItemType.Destornillador);
            inventory.Add(ItemType.Llave);
            Debug.Log("¡Has crafteado una llave!");
        }
        else
        {
            Debug.Log("No tienes materiales suficientes para craftear.");
        }
    }

    public List<ItemType> GetInventory()
    {
        return inventory;
    }
}
