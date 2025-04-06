﻿using System;
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
using Game.Objects.Weapons;
using Game.Objects.Weapons.PlayerWeapons;
using Game.Bullets.EnemyBullets;
using Game.Bullets.PlayerBullets;
using Game.Objects.Weapons.EnemyWeapons;
using Game.Objects;
using Game.Objects.Other;

namespace Game.Creatures.Enemies
{
    internal class Enemy: Creature
    {
        private EnemyGun gun;
        public Enemy(Point spawnPoint, Canvas MyCanvas, List<Enemy> enemies)
        {
            this.Health = 50;
            this.body.Fill = new SolidColorBrush(Colors.Blue);
            this.body.Name = "enemy";
            this.creatureSpeed = 10;
            MyCanvas.Children.Add(this.body);
            Canvas.SetLeft(this.body, spawnPoint.X);
            Canvas.SetTop(this.body, spawnPoint.Y);

            enemies.Add(this);

            gun = new EnemyGun();
            gun.PlayerPosition = spawnPoint;

            hitBox = new Rect(Canvas.GetLeft(this.body), Canvas.GetTop(this.body), body.Width, body.Height);
        }

        public void Shot(Rectangle player, List<EnemyOrdinaryBullet> bullets, Canvas MyCanvas)
        {
            gun.GunReload();
            gun.Shot(new Point(Canvas.GetLeft(player), Canvas.GetTop(player)), bullets, MyCanvas);
        }

        public bool CheckForCollision(Player player, List<Enemy> enemies, List<GameObject> gameObjects, Point nextPosition)
        {
            hitBox.X = nextPosition.X;
            hitBox.Y = nextPosition.Y;
            if (this.hitBox.IntersectsWith(player.hitBox))
            {
                return false;
            }
            foreach (var item in enemies)
            {
                if (item != this && this.hitBox.IntersectsWith(item.hitBox))
                {
                    return false;
                }
            }
            foreach (var item in gameObjects)
            {
                if (!(item is EnemySummoningPoint) && this.hitBox.IntersectsWith(item.hitBox))
                {
                    return false;
                }
            }
            return true;
        }

        public void move(Point playerPosition, Canvas MyCanvas, Player player, List<Enemy> enemies, List<GameObject> gameObjects)
        {
            Point nextPosition = CalculatePathToPlayer(playerPosition);
            Point nextXPosition = new Point(nextPosition.X, Canvas.GetTop(this.body));
            Point nextYPosition = new Point(Canvas.GetLeft(this.body), nextPosition.Y);
            
            if(CheckForCollision(player,enemies,gameObjects, nextXPosition))
            {
                Canvas.SetLeft(this.body, nextPosition.X);
            }
            
            if(CheckForCollision(player, enemies, gameObjects, nextYPosition))
            {
                Canvas.SetTop(this.body, nextPosition.Y);
            }
            hitBox.X = nextPosition.X;
            hitBox.Y = nextPosition.Y;

            gun.PlayerPosition.X = nextPosition.X;
            gun.PlayerPosition.Y = nextPosition.Y;
        }

        protected Point CalculatePathToPlayer(Point playerPosition)
        {
            double angle = Math.Atan2(playerPosition.Y - GetPosition().Y, playerPosition.X - GetPosition().X);

            double vX = this.creatureSpeed * Math.Cos(angle);
            double vY = this.creatureSpeed * Math.Sin(angle);

            double x = GetPosition().X + vX;
            double y = GetPosition().Y + vY;

            return new Point(x, y);
        }
    }
}
