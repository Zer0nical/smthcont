using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class ClubsAce : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.friendly = true;
            //Projectile.magic = true;
            Projectile.damage = 150;
            Projectile.penetrate = 3; // Пронзает 3 врагов
            Projectile.tileCollide = true; // Исчезает при столкновении с блоками
            Projectile.light = 0.5f; // Освещает
            Projectile.scale = 0.58f;
        }

        public override void AI()
        {
            // Добавляем гравитацию
            Projectile.velocity.Y += 0.8f; // Чем больше число, тем сильнее гравитация

            // Ограничиваем максимальную скорость падения
            if (Projectile.velocity.Y > 10f) 
            {
                Projectile.velocity.Y = 10f; // Максимальная скорость падения
            }
            Projectile.rotation += 0.68f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            // Меняем местами игрока и врага
            Vector2 tempPosition = player.Center;
            player.Center = target.Center;
            target.Center = tempPosition;
            player.AddBuff(BuffID.ShadowDodge, 30); // 0.5 секунд додж
            // Звук телепортации
            //SoundEngine.PlaySound(SoundID.Item8, player.position);

            // Спавним 12 фаерболов вокруг врага с рандомным отклонением
            for (int i = 0; i < 12; i++)
            {
                Vector2 randomOffset = new Vector2(
                    Main.rand.Next(-8 * 16, 8 * 16), // Отклонение по X (8 блоков)
                    Main.rand.Next(-3 * 16, 3 * 16) // Отклонение по Y (3 блока)
                );
                Vector2 grenadePosition = target.Center + new Vector2(0, -8 * 16) + randomOffset; // Высота 8 блоков над врагом
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    grenadePosition,
                    Vector2.Zero,
                    ProjectileID.BallofFire,
                    100, // Урон фаерболов
                    1f,
                    player.whoAmI
                );
            }
            // Спавним 10 звезд вокруг врага с рандомным отклонением
            for (int i = 0; i < 10; i++)
            {
                Vector2 randomOffset = new Vector2(
                    Main.rand.Next(-8 * 16, 8 * 16), // Отклонение по X (8 блоков)
                    Main.rand.Next(-1 * 16, 1 * 16) // Отклонение по Y (1 блок)
                );
                Vector2 grenadePosition = target.Center + new Vector2(0, -20 * 16) + randomOffset; // Высота 20 блоков над врагом
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    grenadePosition,
                    Vector2.Zero,
                    ProjectileID.FallingStar,
                    90, // Урон звезд
                    1f,
                    player.whoAmI
                );
            }
        }
    }
}