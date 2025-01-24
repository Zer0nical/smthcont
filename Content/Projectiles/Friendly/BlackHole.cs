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
            Projectile.width = 160; // Изначальный радиус 10 блоков (16 пикселей на блок)
            Projectile.height = 160;
            Projectile.friendly = true;
            Projectile.tileCollide = false; // Не взаимодействует с блоками
            Projectile.timeLeft = 300; // 5 секунд (60 тиков в секунду)
            Projectile.penetrate = -1; // Не исчезает при столкновении
            Projectile.light = 0.5f; // Эффект света
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            // Уменьшение размера со временем
            float lifeRatio = (float)Projectile.timeLeft / 300f;
            Projectile.scale = lifeRatio; // Масштаб зависит от оставшегося времени
            Projectile.width = (int)(160 * lifeRatio);
            Projectile.height = (int)(160 * lifeRatio);

            // Притягивание NPC
            int radius = 200 * 16; // 200 блоков в пикселях
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < radius)
                {
                    Vector2 direction = Projectile.Center - npc.Center;
                    direction.Normalize();
                    float pullStrength = 10f; // Сила притяжения
                    npc.velocity += direction * pullStrength * (1 - npc.Distance(Projectile.Center) / radius); // Слабее по мере отдаления
                }
            }

            // Нанесение урона в радиусе
            if (Projectile.timeLeft % 10 == 0) // Каждые 10 тиков (0.16 секунды)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < Projectile.width / 2)
                    {
                        int damage = 50; // Урон черной дыры
                        //npc.StrikeNPC(damage, 0f, 0);
                    }
                }
            }

            // Эффект пыли (визуализация)
            for (int i = 0; i < 10; i++)
            {
                Vector2 dustPosition = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.height / 2);
                Dust.NewDustPerfect(dustPosition, DustID.Shadowflame, Vector2.Zero, 100, Color.Black, 1.5f).noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            // Эффект при исчезновении черной дыры
            for (int i = 0; i < 30; i++)
            {
                Vector2 dustPosition = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.height / 2);
                Dust.NewDustPerfect(dustPosition, DustID.Shadowflame, Main.rand.NextVector2Circular(2f, 2f), 100, Color.Black, 1.5f).noGravity = true;
            }
        }
    }
}