using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class HeartsAce : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.magic = true;
            Projectile.penetrate = 3; // Пронзает 3 врагов
            Projectile.tileCollide = true; // Исчезает при столкновении с блоками
            Projectile.light = 0.5f; // Освещает
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextFloat() <= 0.9f) // 90% шанс
            {
                player.statLife += 5; // Лечение
                player.HealEffect(5);
                player.AddBuff(BuffID.Regeneration, 600); // 10 секунд регенерации здоровья
                player.AddBuff(BuffID.ManaRegeneration, 600); // 10 секунд регенерации маны
            }
        }
    }
}