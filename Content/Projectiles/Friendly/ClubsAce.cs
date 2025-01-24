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
            Projectile.damage = 140;
            Projectile.penetrate = 3; // Пронзает 3 врагов
            Projectile.tileCollide = true; // Исчезает при столкновении с блоками
            Projectile.light = 0.5f; // Освещает
            Projectile.scale = 0.65f;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            // Меняем местами игрока и врага
            Vector2 tempPosition = player.Center;
            player.Center = target.Center;
            target.Center = tempPosition;

            // Звук телепортации
            //SoundEngine.PlaySound(SoundID.Item8, player.position);

            // Спавним 14 гранат вокруг врага с рандомным отклонением
            for (int i = 0; i < 14; i++)
            {
                Vector2 randomOffset = new Vector2(
                    Main.rand.Next(-8 * 16, 8 * 16), // Отклонение по X (8 блоков)
                    Main.rand.Next(-3 * 16, 3 * 16) // Отклонение по Y (3 блока)
                );
                Vector2 grenadePosition = target.Center + new Vector2(0, -7 * 16) + randomOffset; // Высота 7 блоков над врагом
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    grenadePosition,
                    Vector2.Zero,
                    ProjectileID.Grenade,
                    50, // Урон гранат
                    1f,
                    player.whoAmI
                );
            }
        }
    }
}