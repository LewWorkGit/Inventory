using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LootSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> lootList;
    private bool isQuitting = false;
    [Inject] private DiContainer containerDI = new DiContainer();


    private void OnApplicationQuit()
    {
        isQuitting = true; // Игнорируем спавн при выходе из игры
    }
    //рандомный спавн лута при уничтожении обьекта или врага 
    private void OnDisable()
    {
        if (!isQuitting && gameObject.scene.isLoaded)
        {
            containerDI.InstantiatePrefab(lootList[Random.Range(0, lootList.Count)], transform.position, Quaternion.identity, null);
        }
    }

}
