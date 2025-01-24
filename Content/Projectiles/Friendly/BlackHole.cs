using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace smthcont.Content.Projectiles.Friendly
{
    public class BlackHole : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 480; // Изначальный радиус 30 блоков
            Projectile.height = 480;
            Projectile.friendly = true;
            Projectile.tileCollide = false; // Не взаимодействует с блоками
            Projectile.timeLeft = 480; // 8 секунд
            Projectile.penetrate = -1; // Не исчезает при столкновении
            Projectile.light = 1f; // Эффект света
            Projectile.ignoreWater = true;
            Projectile.scale = 1f; // Изначальный масштаб
        }

        public override void AI()
        {
            // Расчет текущего процента жизни
            float lifeRatio = (float)Projectile.timeLeft / 480f;

            // Масштаб черной дыры уменьшается с временем
            Projectile.scale = lifeRatio;

            // Центрируем черную дыру
            Projectile.width = (int)(480 * lifeRatio);
            Projectile.height = (int)(480 * lifeRatio);
            Projectile.position = Projectile.Center - new Vector2(Projectile.width / 2, Projectile.height / 2);

            // Радиус притяжения
            int radius = (int)(400 * 16 * lifeRatio); // Радиус уменьшается вместе с черной дырой

            // Притягивание NPC
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < radius)
                {
                    Vector2 direction = Projectile.Center - npc.Center;
                    direction.Normalize();
                    float pullStrength = 20f; // Сила притяжения
                    npc.velocity += direction * pullStrength * (1 - npc.Distance(Projectile.Center) / radius);
                }
            }

            // Притягивание снарядов
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.hostile && proj.Distance(Projectile.Center) < radius)
                {
                    Vector2 direction = Projectile.Center - proj.Center;
                    direction.Normalize();
                    float pullStrength = 25f; // Сила притяжения для снарядов
                    proj.velocity += direction * pullStrength * (1 - proj.Distance(Projectile.Center) / radius);
                }
            }

            // Нанесение урона в радиусе
            if (Projectile.timeLeft % 10 == 0) // Каждые 10 тиков (0.16 секунды)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < Projectile.width / 2)
                    {
                        int baseDamage = 160000; // Урон (увеличен в 20 раз от предыдущего)
                        int damage = Math.Max(50, (int)(baseDamage * lifeRatio)); // Урон уменьшается до минимума 50
                    }
                }
            }

            // Эффект искривления пространства
            CreateDistortionEffect();
        }

        public override void Kill(int timeLeft)
        {
            // Эффект при исчезновении черной дыры
            for (int i = 0; i < 100; i++)
            {
                Vector2 dustPosition = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.height / 2);
                Dust.NewDustPerfect(dustPosition, DustID.PortalBolt, Main.rand.NextVector2Circular(3f, 3f), 100, Color.Black, 2f).noGravity = true;
            }
        }

        private void CreateDistortionEffect()
        {
            // Генерация пыли для эффекта искривления пространства
            for (int i = 0; i < 30; i++)
            {
                Vector2 dustPosition = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.height / 2);
                Dust dust = Dust.NewDustPerfect(dustPosition, DustID.Shadowflame, Vector2.Zero, 150, Color.DarkViolet, 1.5f);
                dust.noGravity = true;

                // Пыль движется к черной дыре
                dust.velocity = (Projectile.Center - dustPosition).SafeNormalize(Vector2.Zero) * 2f;

                // Прозрачность зависит от расстояния
                dust.scale *= (0.5f + 0.5f * (1f - dust.velocity.Length()));
            }

            // Генерация "пульсирующего" визуального эффекта
            for (int i = 0; i < 10; i++)
            {
                Vector2 offset = Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.height / 2);
                Dust.NewDustPerfect(Projectile.Center + offset, DustID.PortalBolt, Vector2.Zero, 200, Color.Black, 2.5f).noGravity = true;
            }
        }
    }
}