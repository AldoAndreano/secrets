using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController = default;
    [SerializeField] private Vector2 tileSize = default;
    [SerializeField] private int stepSize = 1;
    [SerializeField] private Transform monominoContainer = default;
    [SerializeField] private Monomino monominoPrefab = default;
    [SerializeField] private List<Transform> horizontalSpawnPoints = new List<Transform>();

    private readonly float MoveCooldown = 0.075f;
    private readonly float SpawnCooldown = 2f;

    private float movementTimer = 0f;
    private float spawnTimer = 0f;

    protected void Update()
    {
        CheckSpawn();
        CheckInput();
    }

    private void CheckSpawn()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > SpawnCooldown)
        {
            spawnTimer = 0;
            SpawnMonominoHorizontally();
        }
    }

    private void CheckInput()
    {
        movementTimer += Time.deltaTime;

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
        if (movementTimer < MoveCooldown)
            return;

        movementTimer = 0;
        // Vector2 playerPos = playerController.GetPlayerPosition();
        // if (playerPos.x - stepSize < -tileSize.x / 2f)
        //     return;
        
        playerController.MovePlayerLeft(stepSize);
    }

    private void HandleMoveRight()
    {
        if (movementTimer < MoveCooldown)
            return;
            
        movementTimer = 0;
        // Vector2 playerPos = playerController.GetPlayerPosition();
        // if (playerPos.x + stepSize > tileSize.x / 2f)
        //     return;
        
        playerController.MovePlayerRight(stepSize);
    }

    private void HandleMoveUp()
    {
        if (movementTimer < MoveCooldown)
            return;
            
        movementTimer = 0;
        // Vector2 playerPos = playerController.GetPlayerPosition();
        // if (playerPos.y + stepSize > tileSize.y / 2f)
        //     return;
        
        playerController.MovePlayerUp(stepSize);
    }

    private void HandleMoveDown()
    {
        if (movementTimer < MoveCooldown)
            return;
            
        movementTimer = 0;
        // Vector2 playerPos = playerController.GetPlayerPosition();
        // if (playerPos.y - stepSize < -tileSize.y / 2f)
        //     return;
        
        playerController.MovePlayerDown(stepSize);
    }

    private void SpawnMonominoHorizontally()
    {
        int randomIndex = UnityEngine.Random.Range(0, horizontalSpawnPoints.Count);
        Monomino monomino = Instantiate(monominoPrefab, monominoContainer);
        monomino.tag = "PositiveMonomino";
        monomino.RegisterOnMonominoReachTarget(RecycleMonomino);

        float xSpawnPos = UnityEngine.Random.Range(0, 2) == 0 ? -10 : 10;
        float ySpawnPos = horizontalSpawnPoints[randomIndex].position.y;
        monomino.transform.position = new Vector3(xSpawnPos, ySpawnPos);
        monomino.SetTargetPos(new Vector3(-1 * xSpawnPos, ySpawnPos));
    }

    private void RecycleMonomino(Monomino monomino)
    {
        Destroy(monomino.gameObject);
    }
}
