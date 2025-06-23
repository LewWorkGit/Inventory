using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items")]
[System.Serializable]
public class Items : ScriptableObject
{
    [SerializeField] private Sprite spriteItem;

    public Sprite GetSpriteItem()
    {
        return spriteItem;
    }
}
