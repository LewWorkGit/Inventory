using UnityEngine;

public interface IEnemy
{
    public void StartVision();
    public void StopVision();
    public void StartChasePlayer();
    public void StopChasing();
    public Transform GetTarget();

}
