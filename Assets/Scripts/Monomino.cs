using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monomino : MonoBehaviour
{
    private Action<Monomino, Monomino> onHitOtherMonomino = null;
    private Action onMonominoDestroyed = null;
    public List<Monomino> LinkedMonominos { get; private set; } = new List<Monomino>();

    public void RegisterOnHitAction(Action<Monomino, Monomino> onHitOtherMonomino)
    {
        this.onHitOtherMonomino = onHitOtherMonomino;
    }

    public void RegisterOnMonominoDestroyed(Action onMonominoDestroyed)
    {
        this.onMonominoDestroyed = onMonominoDestroyed;
    }

    public void LinkMonominoToSource(Monomino monomino)
    {
        LinkedMonominos.Add(monomino);
        monomino.transform.SetParent(transform);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        onHitOtherMonomino?.Invoke(this, collider.gameObject.GetComponent<Monomino>());
    }

    protected void OnDestroy()
    {
        onMonominoDestroyed?.Invoke();
    }
}
