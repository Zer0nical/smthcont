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
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
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
            Projectile.light = 3f; // Освещение
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            // Вращение в направлении движения
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // Эффекты света
            Lighting.AddLight(Projectile.Center, 0.9f, 0.2f, 0.1f); // красный свет

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
            target.AddBuff(BuffID.Confused, 300); // Конфузия на 5 секунд
			target.AddBuff(BuffID.PotionSickness, 6000); // без лечения на 100 секунд
			target.AddBuff(BuffID.Cursed, 180); // Без оружия на 3 секунды
			target.AddBuff(BuffID.OnFire, 600); // Огонь на 10 секунд
			target.AddBuff(BuffID.Slow, 300); // Замедление на 5 секунд
			target.AddBuff(BuffID.ChaosState, 6000); // без тп на 100 секунд


            // Вампиризм
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextFloat() <= 0.45f) // 45% шанс
            {
                player.statLife += 2; // Увеличение здоровья игрока на 2
                player.HealEffect(2, true); // Отображение визуального эффекта исцеления
            }
        }
    }
}