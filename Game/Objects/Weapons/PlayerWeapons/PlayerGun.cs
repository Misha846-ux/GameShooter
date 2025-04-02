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
using Game.Objects.Weapons;
using Game.Bullets.PlayerBullets;
using Game.Bullets.EnemyBullets;

namespace Game.Objects.Weapons.PlayerWeapons
{
    class PlayerGun : Gun
    {
        public PlayerGun()
        {
            rateOfFireTime = 20;
            rateOfFireTimer = rateOfFireTime;
            reloadTime = 50;
            reloadTimer = 0;
            maxAmmo = 10;
            ammo = maxAmmo;
        }
        public override void Shot(Point mousePosition, List<PlayerOrdinaryBullet> bullets, Canvas MyCanvas)
        {
            if (CheckIfFirePossible())
            {
                PlayerOrdinaryBullet newBullet = new PlayerOrdinaryBullet(PlayerPosition, mousePosition, 5, bullets);
                MyCanvas.Children.Add(newBullet.GetBullet());
            }
        }
        public override void Shot(Point mousePosition, List<EnemOrdinaryBullet> bullets, Canvas MyCanvas) { }
    }
}
