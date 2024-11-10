using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController = default;
    [SerializeField] private Vector2 tileSize = default;
    [SerializeField] private int stepSize = 1;

    private readonly float MoveCooldown = 0.075f;

    private float elapsedTime = 0f;

    protected void Update()
    {
        elapsedTime += Time.deltaTime;
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.A))
            HandleMoveLeft();

        if (Input.GetKey(KeyCode.D))
            HandleMoveRight();

        if (Input.GetKey(KeyCode.W))
            HandleMoveUp();

        if (Input.GetKey(KeyCode.S))
            HandleMoveDown();
    }

    private void HandleMoveLeft()
    {
        if (elapsedTime < MoveCooldown)
            return;

        elapsedTime = 0;
        Vector2 playerPos = playerController.GetPlayerPosition();
        if (playerPos.x - stepSize < -tileSize.x / 2f)
            return;
        
        playerController.MovePlayerLeft(stepSize);
    }

    private void HandleMoveRight()
    {
        if (elapsedTime < MoveCooldown)
            return;
            
        elapsedTime = 0;
        Vector2 playerPos = playerController.GetPlayerPosition();
        if (playerPos.x + stepSize > tileSize.x / 2f)
            return;
        
        playerController.MovePlayerRight(stepSize);
    }

    private void HandleMoveUp()
    {
        if (elapsedTime < MoveCooldown)
            return;
            
        elapsedTime = 0;
        Vector2 playerPos = playerController.GetPlayerPosition();
        if (playerPos.y + stepSize > tileSize.y / 2f)
            return;
        
        playerController.MovePlayerUp(stepSize);
    }

    private void HandleMoveDown()
    {
        if (elapsedTime < MoveCooldown)
            return;
            
        elapsedTime = 0;
        Vector2 playerPos = playerController.GetPlayerPosition();
        if (playerPos.y - stepSize < -tileSize.y / 2f)
            return;
        
        playerController.MovePlayerDown(stepSize);
    }
}
