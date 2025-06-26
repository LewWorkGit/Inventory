using UnityEngine;
public interface IMovePlayer
{
    public Transform GetEnemyTransform();

    public void SetEnemyTransform(Transform enemy);

    public Vector3 GetLookSide();

    public void DisableInput();
}
