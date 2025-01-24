using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class SpadesAce : ModProjectile
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
            Projectile.scale = 0.58f;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextFloat() <= 0.9f) // 90% шанс
                target.AddBuff(BuffID.Poisoned, 900); // Отравление на 15 секунд

            if (Main.rand.NextFloat() <= 0.75f) // 75% шанс
                target.AddBuff(BuffID.Venom, 420); // Яд на 7 секунд

            if (Main.rand.NextFloat() <= 0.5f) // 50% шанс
                target.AddBuff(BuffID.Slow, 300); // Замедление на 5 секунд
        }
    }
}