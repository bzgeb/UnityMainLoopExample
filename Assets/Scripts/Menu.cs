using UnityEngine;

public class Menu : MonoBehaviour
{
    GameManager _gameManager;

    public bool IsOpen { get; set; }
    
    public void OnCreated(GameManager gameManager)
    {
        _gameManager = gameManager;
        IsOpen = false;
        gameObject.SetActive(false);
    }

    public void OnUpdate(GameManager.GameInput input)
    {
        // Do some menu stuff
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        gameObject.SetActive(IsOpen);
    }
}