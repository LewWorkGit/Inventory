using UnityEngine;
using UnityEngine.InputSystem;

public class SingleShotWeapon : Weapons
{
    //стрельба из оружия без автоматического режима огня
    public override void ShootWeapon(InputAction.CallbackContext context)
    {
        if (context.started && weaponAmmo > 0 && countFireRateTimer == 0 && !reloadBool )
        {
          
            weaponAmmo--;

            Rigidbody2D bullet = bulletPoolMassive[bulletPoolCountIndex];
            bullet.gameObject.SetActive(true);
            bullet.velocity = Vector2.zero;
            bullet.transform.position = shotPoint.position;      
            bullet.simulated = true;
            bullet.GetComponent<Rigidbody2D>().AddForce(shotVector * speedBullet, ForceMode2D.Impulse);


            TrailRenderer trail = bullet.GetComponent<TrailRenderer>();
            trail.enabled = true;
            trail.Clear();
          
            bulletPoolCountIndex++;

            audioSource.PlayOneShot(shootSound);
            ammoUI.SetWeaponAmmoText(weaponAmmo);

            if (weaponAmmo == 0)//перезарядка если магазин пустой
            {
                reload(context);
            }

        }
    }
}
