using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace smthcont.Content.Projectiles
{
    public class BeginProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // Длина следа
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // Режим следа
        }

        public override void SetDefaults()
        {
            Projectile.width = 14; // Ширина снаряда
            Projectile.height = 14; // Высота снаряда
            Projectile.friendly = true; // Снаряд дружелюбный
            Projectile.hostile = false; // Не наносит урон игроку
            Projectile.DamageType = DamageClass.Melee; // Тип урона
            Projectile.penetrate = 3; // Количество проникновений (3 попадания)
            Projectile.timeLeft = 100; // Время жизни снаряда (в кадрах)
            Projectile.damage = 6; // Урон снаряда
            Projectile.light = 0.7f; // Освещение вокруг снаряда
            Projectile.ignoreWater = true; // Не замедляется в воде
            Projectile.tileCollide = true; // Снаряд сталкивается с блоками
        }

        public override void AI()
        {
            // Самонаводка на ближайшего врага
            NPC target = FindClosestNPC(450f); // Радиус поиска - 450 пикселей
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center; // Направление к цели
                direction.Normalize(); // Нормализация вектора (длина 1)
                direction *= 17f; // Скорость снаряда (можно настроить)
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction, 0.16f); // Плавное изменение направления
            }

            // Эффекты (пыль)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Наложение дебаффов на цель
            target.AddBuff(BuffID.Bleeding, 180); // Кровотечение на 3 секунды (180 кадров)
            target.AddBuff(BuffID.BrokenArmor, 180); // Снижение защиты на 3 секунды (180 кадров)
            target.AddBuff(BuffID.Ichor, 180); // Снижение ихор на 3 секунды (180 кадров)
            // Вампиризм: восстановление 1 единицы здоровья игроку
            Player player = Main.player[Projectile.owner];
            Projectile.Kill();
            if (Main.rand.NextFloat() <= 0.7f) // Шанс 70%
            {
                player.statLife += 1; // Увеличение здоровья игрока на 1
                player.HealEffect(1, true); // Отображение визуального эффекта исцеления
            }
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Логика отскока от блоков
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X; // Меняем направление по X
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y; // Меняем направление по Y
            }

            Projectile.penetrate--; // Уменьшаем количество проникновений
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill(); // Уничтожаем снаряд, если проникновения закончились
            }

            return false; // Возвращаем false, чтобы предотвратить уничтожение снаряда
        }

        private NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;
            float closestDistance = maxDetectDistance;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(this) && !npc.friendly)
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestNPC = npc;
                    }
                }
            }

            return closestNPC;
        }
    }
}