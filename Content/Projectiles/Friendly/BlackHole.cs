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
            // Уменьшение радиуса черной дыры с течением времени
            radius -= 2f / (lifeTime * 60f); // lifeTime = 5 секунд
            if (radius <= 0f)
            {
                Projectile.Kill();
                return;
            }

            // Визуальный эффект (анимация и вращение)
            Projectile.scale = radius / 10f; // Масштабируем черную дыру
            Projectile.rotation += 0.05f; // Вращение

            // Притягивание мобов
            int range = 200 * 16; // Радиус притяжения в пикселях (200 блоков)
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.lifeMax > 5)
                {
                    Vector2 direction = Projectile.Center - npc.Center; // Направление к черной дыре
                    float distance = direction.Length();

                    if (distance <= range)
                    {
                        direction.Normalize(); // Приведение вектора к единичной длине

                        // Рассчитываем силу притяжения
                        float pullStrength = MathHelper.Lerp(2f, 0f, distance / range); // Сила ослабевает с расстоянием
                        npc.velocity += direction * pullStrength;

                        // Наносим урон, если моб в радиусе черной дыры
                        if (distance <= radius * 16f)
                        {
                            npc.StrikeNPC(50, 0f, 0); // Периодический урон 50
                        }
                    }
                }
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