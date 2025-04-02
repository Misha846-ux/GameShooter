using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game.Creatures.Players;
using Game.Bullets;
using Game.Bullets.PlayerBullets;
using Game.Bullets.EnemyBullets;
namespace Game.Objects.Weapons
{

    internal abstract class Gun
    {
        protected int rateOfFireTime; // contains a value indicating the delay between shots
        protected int rateOfFireTimer; // contains a value indicating how much time has passed between shots
        protected int reloadTime;
        protected int reloadTimer;
        protected int maxAmmo; // stores maximum amount of ammo
        protected int ammo; // stores the current number of cartridges

        public int MaxAmmo { get { return maxAmmo; } }
        public int Ammo { get { return ammo; } }
        public Point PlayerPosition { get; set; }

        public void FastReload()
        {
            ammo = 0;
        }
        public void GunReload()
        {
            if (rateOfFireTimer < reloadTime)
            {
                rateOfFireTimer++;
            }
            if (reloadTime <= reloadTimer && ammo <= 0)
            {
                ammo = maxAmmo;
                reloadTimer = 0;
            }
            else if (ammo <= 0)
            {
                reloadTimer++;
            }
        }
        public bool CheckIfFirePossible() // gidhfdhjdksh
        {
            if (ammo > 0)
            {
                if (rateOfFireTime <= rateOfFireTimer)
                {
                    ammo--;
                    rateOfFireTimer = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public abstract void Shot(Point mousePosition, List<PlayerOrdinaryBullet> bullets, Canvas MyCanvas);
        public abstract void Shot(Point mousePosition, List<EnemOrdinaryBullet> bullets, Canvas MyCanvas);
    }
}
