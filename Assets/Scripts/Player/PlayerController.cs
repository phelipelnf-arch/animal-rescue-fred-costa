using UnityEngine;
using System;

/// <summary>
/// Controla o movimento e ações do jogador (Fred Costa).
/// Responsável por input, movimento, animações e interações.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDrag = 5f;

    [Header("Controle")]
    [SerializeField] private float groundDist = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Componentes")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0f;
    private bool isGrounded = false;
    private bool isInteracting = false;

    private HealthSystem healthSystem;
    private InventorySystem inventorySystem;
    private Animal nearbyAnimal;

    public event Action OnInteract;
    public event Action<Vector3> OnMove;

    public bool IsInteracting => isInteracting;
    public Animal NearbyAnimal => nearbyAnimal;

    private void Awake()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        inventorySystem = GetComponent<InventorySystem>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        HandleInput();
        UpdateGroundedStatus();
        ApplyGravity();
        MoveCharacter();
        UpdateAnimations();
    }

    /// <summary>
    /// Processa input do jogador
    /// </summary>
    private void HandleInput()
    {
        // Movimento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude > 0)
        {
            OnMove?.Invoke(moveDirection);
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Interação
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    /// <summary>
    /// Move o personagem
    /// </summary>
    private void MoveCharacter()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        Vector3 movement = moveDirection * currentSpeed;
        movement.y = verticalVelocity;

        characterController.Move(movement * Time.deltaTime);

        // Rotacionar personagem na direção do movimento
        if (moveDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    /// <summary>
    /// Faz o personagem pular
    /// </summary>
    private void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
    }

    /// <summary>
    /// Aplica gravidade
    /// </summary>
    private void ApplyGravity()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Pequena gravidade para manter no chão
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    /// <summary>
    /// Atualiza status de estar no chão
    /// </summary>
    private void UpdateGroundedStatus()
    {
        isGrounded = Physics.CheckSphere(
            transform.position - Vector3.up * groundDist,
            0.1f,
            groundLayer
        );
    }

    /// <summary>
    /// Atualiza animações
    /// </summary>
    private void UpdateAnimations()
    {
        if (animator == null) return;

        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsInteracting", isInteracting);
    }

    /// <summary>
    /// Interage com objeto próximo
    /// </summary>
    private void Interact()
    {
        if (nearbyAnimal != null && !isInteracting)
        {
            isInteracting = true;
            OnInteract?.Invoke();
            // Lógica de resgate será implementada pelo RescueSystem
        }
    }

    /// <summary>
    /// Tira dano do jogador
    /// </summary>
    public void TakeDamage(int damage)
    {
        healthSystem?.TakeDamage(damage);
    }

    /// <summary>
    /// Cura o jogador
    /// </summary>
    public void Heal(int amount)
    {
        healthSystem?.Heal(amount);
    }

    /// <summary>
    /// Deteta animal próximo
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Animal>(out var animal))
        {
            nearbyAnimal = animal;
        }
    }

    /// <summary>
    /// Perde detecção do animal
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Animal>(out var animal) && nearbyAnimal == animal)
        {
            nearbyAnimal = null;
            isInteracting = false;
        }
    }
}