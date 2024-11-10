using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Monomino coreMonomino = default;

    private List<Monomino> bondedMonominos = new();

    void Start()
    {
        coreMonomino.RegisterOnHitAction(HandleOnHitOtherMonomino);
        coreMonomino.RegisterOnMonominoDestroyed(HandleOnCoreMonominoDestroyed);
        bondedMonominos.Add(coreMonomino);
    }

    private void HandleOnHitOtherMonomino(Monomino sourceMonomino, Monomino otherMonomino)
    {
        if (otherMonomino.CompareTag("PositiveMonomino")) 
        {
            sourceMonomino.LinkMonominoToSource(otherMonomino);
            otherMonomino.tag = "Player";
            otherMonomino.RegisterOnHitAction(HandleOnHitOtherMonomino);
            bondedMonominos.Add(otherMonomino);
        }
        else if (otherMonomino.CompareTag("NegativeMonomino"))
        {
            bondedMonominos.Remove(sourceMonomino);
            for (int i = 0; i < sourceMonomino.LinkedMonominos.Count; i++) 
            {
                int index = i;
                Monomino monomino = sourceMonomino.LinkedMonominos[index];
                bondedMonominos.Remove(monomino);
            }

            Destroy(sourceMonomino.gameObject);
            Destroy(otherMonomino.gameObject);
        }

        Debug.Log("MASHOk " + JsonUtility.ToJson(bondedMonominos));
    }

    private void HandleOnCoreMonominoDestroyed()
    {
        Debug.Log("YOU LOSE!");
    }

    public Vector2 GetPlayerPosition()
    {
        return coreMonomino.transform.position;
    }

    public void MovePlayerLeft(int moveStep)
    {
        coreMonomino.transform.position -= new Vector3(1, 0) * moveStep;
    }

    public void MovePlayerRight(int moveStep)
    {
        coreMonomino.transform.position += new Vector3(1, 0) * moveStep;
    }

    public void MovePlayerUp(int moveStep)
    {
        coreMonomino.transform.position += new Vector3(0, 1) * moveStep;
    }

    public void MovePlayerDown(int moveStep)
    {
        coreMonomino.transform.position -= new Vector3(0, 1) * moveStep;
    }
}
