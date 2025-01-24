using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace smthcont.Content.Projectiles
{
    public class PerfectRubyBladeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10; // Длина следа
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 64; // Ширина снаряда (8 блоков = 64 пикселя)
            Projectile.height = 64; // Высота снаряда
            Projectile.friendly = true; // Снаряд дружелюбный
            Projectile.hostile = false; // Не наносит урон игроку
            Projectile.DamageType = DamageClass.Melee; // Тип урона
            Projectile.penetrate = -1; // Бесконечное проникновение
            Projectile.timeLeft = 300; // Время жизни (5 секунд)
            Projectile.tileCollide = false; // Проходит сквозь блоки
            Projectile.light = 1f; // Освещение
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            // Вращение в направлении движения
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // Эффекты света
            Lighting.AddLight(Projectile.Center, 0.5f, 0.1f, 0.6f); // Фиолетовый свет

            // Создание пыли
            if (Main.rand.NextBool(2)) // 50% шанс
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Наложение дебаффов
            target.AddBuff(BuffID.Bleeding, 300); // Кровотечение на 5 секунд
            target.AddBuff(BuffID.CursedInferno, 180); // Проклятый огонь на 3 секунды

            // Вампиризм
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextFloat() <= 0.7f) // Шанс 70%
            {
                player.statLife += 1; // Увеличение здоровья игрока на 1
                player.HealEffect(1, true); // Отображение визуального эффекта исцеления
            }
        }
    }
}