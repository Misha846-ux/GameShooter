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

namespace Game.Objects.Weapons.EnemyWeapons
{
    class EnemyGun : Gun
    {
        public EnemyGun()
        {
            this.bulletType = typeof(EnemyOrdinaryBullet);
            rateOfFireTime = 20;
            rateOfFireTimer = rateOfFireTime;
            reloadTime = 50;
            reloadTimer = 0;
            damage = 5;
            maxAmmo = 10;
            ammo = maxAmmo;
        }
    }
}
