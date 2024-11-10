using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monomino : MonoBehaviour
{
    private readonly float MovementSpeed = 0.1f;

    private Action<Monomino, Monomino> onHitOtherMonomino = null;
    private Action onMonominoDestroyed = null;
    private Action<Monomino> onMonominoReachTarget = null;
    private Vector3 targetPos = default;
    private bool canMove = false;
    private int movementMultiplier = 1;

    public List<Monomino> LinkedMonominos { get; private set; } = new List<Monomino>();

    protected void Update()
    {
        MoveMonomino();
    }

    public void RegisterOnHitOtherMonomino(Action<Monomino, Monomino> onHitOtherMonomino)
    {
        this.onHitOtherMonomino = onHitOtherMonomino;
    }

    public void RegisterOnMonominoDestroyed(Action onMonominoDestroyed)
    {
        this.onMonominoDestroyed = onMonominoDestroyed;
    }

    public void RegisterOnMonominoReachTarget(Action<Monomino> onMonominoReachTarget)
    {
        this.onMonominoReachTarget = onMonominoReachTarget;
    }

    public void LinkMonominoToSource(Monomino monomino)
    {
        LinkedMonominos.Add(monomino);
        monomino.transform.SetParent(transform);
        monomino.DisableMovement();
    }

    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        if (this.targetPos.x < transform.position.x)
            movementMultiplier = -1;
        else
            movementMultiplier = 1;

        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    private void MoveMonomino()
    {
        if (!canMove)
            return;

        transform.position += new Vector3(MovementSpeed * movementMultiplier, 0);
        if (transform.position == targetPos)
        {
            onMonominoReachTarget?.Invoke(this);
            onMonominoReachTarget = null;
            canMove = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("MASHOKKK " + collider);
        Monomino monomino = collider.GetComponent<Monomino>()!;
        if (monomino != null)
            onHitOtherMonomino?.Invoke(this, collider.gameObject.GetComponent<Monomino>());
    }

    protected void OnDestroy()
    {
        onMonominoDestroyed?.Invoke();
    }
}
