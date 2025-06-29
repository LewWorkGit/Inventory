using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] public List<GameObject> slots;


    public override void InstallBindings()
    {
         List<ISlotModel> slotsModel = slots
         .Select(go => go.GetComponent<ISlotModel>())
         .Where(slot => slot != null)
         .ToList();

         Container.Bind<List<ISlotModel>>()
        .FromInstance(slotsModel)
        .AsSingle();

         Container.Bind<IPanelItemsMenu>()
        .FromComponentInHierarchy()
        .AsSingle();

        Container.Bind<IMovePlayer>()
        .FromComponentInHierarchy()
        .AsSingle();

        Container.Bind<IHealthBar>()
        .FromComponentInHierarchy()
        .AsSingle();

        Container.Bind<GameOver>()
        .FromComponentInHierarchy()
        .AsSingle();

        Container.Bind<IAmmoUI>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<IInventoryController>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<SaveLoadManager>()
       .FromComponentInHierarchy()
       .AsSingle();

        Container.Bind<InventorySaveLoad>()
       .FromComponentInHierarchy()
       .AsSingle();
    }
}
