using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager _gameManager;
    
    [SerializeField] CharacterController _characterController;
    [SerializeField] float _moveSpeed = 10f;

    public void OnCreated(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void OnUpdate(GameManager.GameInput input)
    {
        _characterController.Move(new Vector3(input.Horizontal * _moveSpeed * Time.deltaTime, 
            0f,
            input.Vertical * _moveSpeed * Time.deltaTime));
    }
}