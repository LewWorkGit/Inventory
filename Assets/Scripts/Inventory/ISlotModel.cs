public interface ISlotModel
{
    public void AddItem(Items _item, int countItem);
    public void DeleteItem();

    public Items GetItem();

    public int GetCountItem();
}
