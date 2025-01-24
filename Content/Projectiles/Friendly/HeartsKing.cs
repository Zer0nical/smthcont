using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class HeartsKing : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.damage = 130;
            //Projectile.magic = true;
            Projectile.penetrate = 3; // Пронзает 3 врагов
            Projectile.tileCollide = true; // Исчезает при столкновении с блоками
            Projectile.light = 0.5f; // Освещает
            Projectile.scale = 0.58f;
        }

        public override void AI()
        {
            // Добавляем гравитацию
            Projectile.velocity.Y += 0.45f; // Чем больше число, тем сильнее гравитация

            // Ограничиваем максимальную скорость падения
            if (Projectile.velocity.Y > 10f) 
            {
                Projectile.velocity.Y = 10f; // Максимальная скорость падения
            }
            Projectile.rotation += 0.3f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextFloat() <= 0.92f) // 92% шанс
            {
                player.statLife += 18; // Лечение
                player.HealEffect(18);
                player.AddBuff(BuffID.Regeneration, 900); // 15 секунд 
                player.AddBuff(BuffID.ManaRegeneration, 900); 
                player.AddBuff(BuffID.Honey, 900);
                player.AddBuff(BuffID.RapidHealing, 900); 
                player.AddBuff(BuffID.Campfire, 900); 
                player.AddBuff(BuffID.HeartLamp, 900); 
                player.AddBuff(BuffID.Lifeforce, 900); 
                player.AddBuff(BuffID.NebulaUpLife2, 900); 
            }
        }
    }
}