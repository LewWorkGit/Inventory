using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventorySaveLoad : MonoBehaviour
{
    [Inject] private List<ISlotModel> slots;
    [SerializeField] private List<Items> items;

    private List<int> saveSlotslist;
    private List<int> saveCountItemlist;

    //сохранение инвентаря
    public void SaveInventory()
    {

        for (int i = 0; i < slots.Count; i++)
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (slots[i].GetItem() == items[j])
                {
                    saveSlotslist[i] = j;
                    saveCountItemlist[i] = slots[i].GetCountItem();

                }
            }
        }
        ES3.Save<List<int>>("saveSlotslist", saveSlotslist);
        ES3.Save<List<int>>("saveCountItemlist", saveCountItemlist);
    }

    //загрузка инвентаря
    public void LoadInventory()
    {
        if (ES3.KeyExists("saveSlotslist"))
        {
            saveSlotslist = ES3.Load<List<int>>("saveSlotslist");
            saveCountItemlist = ES3.Load<List<int>>("saveCountItemlist");

            for (int i = 0; i < slots.Count; i++)
            {
                if (saveSlotslist[i] >= 0)
                {
                    slots[i].AddItem(items[saveSlotslist[i]], saveCountItemlist[i]);
                }
            }
        }
        else
        {
            saveSlotslist = new List<int>(slots.Count);
            saveCountItemlist = new List<int>(slots.Count);

            for (int i = 0; i < slots.Count; i++)
            {
                saveSlotslist.Add(-1);
                saveCountItemlist.Add(0);
            }
        }
    }
}
