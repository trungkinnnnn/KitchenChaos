using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterData _characterData;
    [SerializeField] Animator _animator;
    [SerializeField] GameInput _gameInput;
    
    private int _PARA_ANI_ISWALKING = Animator.StringToHash("IsWalking");

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 input = _gameInput.GetMovementVectorNormalized();

        _animator.SetBool(_PARA_ANI_ISWALKING, input != Vector3.zero);

        input *= _characterData.speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + input);

        if(input != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, input, Time.fixedDeltaTime * _characterData.speed);
    }    
}
