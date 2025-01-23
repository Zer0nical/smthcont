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
            Projectile.penetrate = 1; // Количество проникновений (3 попадания)
            Projectile.timeLeft = 300; // Время жизни снаряда (в кадрах)
            Projectile.damage = 40; // Урон снаряда
            Projectile.light = 0.8f; // Освещение вокруг снаряда
            Projectile.ignoreWater = true; // Не замедляется в воде
            Projectile.tileCollide = true; // Снаряд сталкивается с блоками
        }

        public override void AI()
        {
            // Самонаводка на ближайшего врага
            NPC target = FindClosestNPC(500f); // Радиус поиска - 500 пикселей
            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center; // Направление к цели
                direction.Normalize(); // Нормализация вектора (длина 1)
                direction *= 12f; // Скорость снаряда (можно настроить)
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction, 0.1f); // Плавное изменение направления
            }

            // Эффекты (пыль)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
        }
        /*public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damage, int hitDirection) 
        //NPC target, NPC.HitInfo hit, int damageDone -working
        //NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection -plz
        //NPC target, NPC.HitInfo hit, int damageDone, ref int damage, ref float knockback, ref bool crit, ref int hitDirection 
        {
            target.AddBuff(BuffID.OnFire, 300); // 1 секунды = 60 кадров
        }*/

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