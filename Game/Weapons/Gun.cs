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
namespace Game.Weapons
{
    internal abstract class Gun
    {
        protected int rateOfFireTime; // contains a value indicating the delay between shots
        protected int rateOfFireTimer; // contains a value indicating how much time has passed between shots
        protected int reloadTime;
        protected int reloadTimer;
        protected int maxAmmo; // stores maximum amount of ammo
        protected int ammo; // stores the current number of cartridges

        public int MaxAmmo { get { return this.maxAmmo; } }
        public int Ammo { get { return this.ammo; } }
        public Point PlayerPosition { get; set; }

        public void GunReload()
        {
            if(this.rateOfFireTimer < this.reloadTime)
            {
                this.rateOfFireTimer++;
            }
            if (this.reloadTime <= this.reloadTimer && this.ammo <= 0)
            {
                this.ammo = this.maxAmmo;
                this.reloadTimer = 0;
            }
            else if(this.ammo <= 0)
            {
                this.reloadTimer++;
            }
        }
        public bool CheckIfFirePossible() // gidhfdhjdksh
        {
            if(this.ammo > 0)
            {
                if (this.rateOfFireTime <= this.rateOfFireTimer)
                {
                    this.ammo--;
                    this.rateOfFireTimer = 0;
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

        public abstract void Shot(Point mousePosition, List<Bullet> bullets, Canvas MyCanvas);

    }
}
