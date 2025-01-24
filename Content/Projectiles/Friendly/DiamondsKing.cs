using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class DiamondsAce : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.friendly = true;
            //Projectile.magic = true;
            Projectile.damage = 130;
            Projectile.penetrate = 3; // Пронзает 3 врагов
            Projectile.tileCollide = true; // Исчезает при столкновении с блоками
            Projectile.light = 0.5f; // Освещает
            Projectile.scale = 0.58f;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.rand.NextFloat() <= 0.9f) // 90% шанс
                target.AddBuff(BuffID.Bleeding, 900); // Кровотечение на 15 секунд

            // Случайный бафф для игрока
            int[] buffs = { BuffID.Ironskin, BuffID.Endurance, BuffID.Rage };
            int randomBuff = Main.rand.Next(buffs.Length);
            player.AddBuff(buffs[randomBuff], 1200); // Бафф на 20 секунд

            // Телепортируем врага случайным образом
            Vector2 randomTeleportOffset = new Vector2(
                Main.rand.Next(-10 * 16, 10 * 16), // Отклонение по X (до 10 блоков)
                Main.rand.Next(-10 * 16, 10 * 16) // Отклонение по Y (до 10 блоков)
            );
            target.position += randomTeleportOffset;

            // Звук телепортации для врага
            //SoundEngine.PlaySound(SoundID.Item8, target.position);
        }
    }
}
