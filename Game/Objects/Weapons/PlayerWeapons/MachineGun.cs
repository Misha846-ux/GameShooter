using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Bullets.PlayerBullets;

namespace Game.Objects.Weapons.PlayerWeapons
{
    internal class MachineGun: Gun
    {
        public MachineGun()
        {
            this.bulletType = typeof(PlayerOrdinaryBullet);
            rateOfFireTime = 2;
            rateOfFireTimer = rateOfFireTime;
            reloadTime = 50;
            reloadTimer = 0;
            damage = 5;
            maxAmmo = 100;
            ammo = maxAmmo;
        }
    }
}
