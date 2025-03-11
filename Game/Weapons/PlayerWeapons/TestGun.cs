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
using System.Windows.Automation.Text;

namespace Game.Weapons.PlayerWeapons
{
    internal class TestGun: Gun
    {

        public TestGun()
        {
            this.rateOfFireTime = 20;
            this.rateOfFireTimer = this.rateOfFireTime;
            this.reloadTime = 50;
            this.reloadTimer = 0;
            this.maxAmmo = 10;
            this.ammo = this.maxAmmo;
        }
        public override void Shot(Point mousePosition, List<Bullet> bullets, Canvas MyCanvas)
        {
            if (this.CheckIfFirePossible())
            {
                Bullet newBullet = new Bullet(this.PlayerPosition, mousePosition);
                bullets.Add(newBullet);
                MyCanvas.Children.Add(newBullet.GetBullet());
            }
        }
    }
}
