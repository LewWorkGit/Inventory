using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


public abstract class Weapons : MonoBehaviour
{

    [SerializeField] protected AudioClip shootSound;
    [SerializeField] protected AudioClip reloadSound;
    [SerializeField] protected Transform shotPoint;
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected Rigidbody2D[] bulletPoolMassive;
    protected AudioSource audioSource;
    protected Quaternion startHandRotationValue;
    protected int bulletPoolCountIndex;
    protected Vector2 shotVector;

    [Inject] protected IMovePlayer player;
    [Inject] protected IAmmoUI ammoUI;
    [Inject] protected SaveLoadManager saveLoadManager;


    [Space]
    [Header("параметры выстрела")]

    [SerializeField] [Range(1, 100f)] protected int fireRateValue = 10;
    [SerializeField] [Range(1, 1000f)] protected int damageWeaponValue = 20;
    [SerializeField] protected int speedBullet = 100;

    protected int countFireRateTimer;

    [Space]
    [Header("параметры перезарядки")]

    [SerializeField] [Range(1, 1000f)] protected int maxAmmo = 7;
    [SerializeField] private int inventoryAmmo = 999;
    [SerializeField] [Range(1, 15f)] protected float timeReload = 5;
    [HideInInspector] [SerializeField] protected int weaponAmmo;
    private InputAction.CallbackContext context;
    protected bool reloadBool;

    private void Awake()
    {
        weaponAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        startHandRotationValue = rightHand.localRotation;  

        //задаем значение урона пулям в зависимости от урона оружия
        foreach (var item in bulletPoolMassive)
        {
            item.GetComponent<Bullet>().SetDamageValue(damageWeaponValue);
        }

        saveLoadManager.OnLoadOver += RefreshAmmoUI;
    }

    private void FixedUpdate()
    {

        if (countFireRateTimer > 0)//задержка между выстрелами(скорострельность)
        {
            countFireRateTimer--;
        }

        CalculateShootDirection();
    }

    protected void CalculateShootDirection()
    {
        //расчет вектора выстрела
        if (player.GetEnemyTransform() == null)
        {
            if (player.GetLookSide().x >= 0)
            {
                shotVector = shotPoint.right;
            }
            else
            {
                shotVector = -shotPoint.right;
            }

            rightHand.localRotation = startHandRotationValue;
        }
        else
        {
            rightHand.up = player.GetEnemyTransform().position - rightHand.position;
            shotVector = player.GetEnemyTransform().position - shotPoint.position;
            shotVector.Normalize();
        }
    }

    public abstract void ShootWeapon(InputAction.CallbackContext context);//метод выстрела

    public void reload(InputAction.CallbackContext context)//метод перезарядки
    {
        if (context.started && weaponAmmo != maxAmmo && !reloadBool && inventoryAmmo > 0 && gameObject.activeInHierarchy)
        {
            StartCoroutine(reloadCor());
        }
    }

    IEnumerator reloadCor()
    {
        reloadBool = true;

        audioSource.pitch = 1;
        audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(timeReload);


        int reloadAmmoMinus = maxAmmo - weaponAmmo;

        if (inventoryAmmo >= maxAmmo)
        {
            weaponAmmo += reloadAmmoMinus;

            inventoryAmmo -= reloadAmmoMinus;
            inventoryAmmo = Mathf.Clamp(inventoryAmmo, 0, 999);
            ammoUI.SetInvetoryAmmoText(inventoryAmmo);
        }
        else
        {
            if (inventoryAmmo < reloadAmmoMinus)
            {
                weaponAmmo += inventoryAmmo;
            }
            else
            {
                weaponAmmo += reloadAmmoMinus;
            }

            inventoryAmmo -= reloadAmmoMinus;
            inventoryAmmo = Mathf.Clamp(inventoryAmmo, 0, 999);
            ammoUI.SetInvetoryAmmoText(inventoryAmmo);
        }

        ammoUI.SetWeaponAmmoText(weaponAmmo);
        bulletPoolCountIndex = 0;
        reloadBool = false;
    }

    private void RefreshAmmoUI()
    {
        ammoUI.SetInvetoryAmmoText(inventoryAmmo);
        ammoUI.SetWeaponAmmoText(weaponAmmo);
    }

}
