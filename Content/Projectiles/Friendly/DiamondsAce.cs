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
            Projectile.damage = 150;
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

            if (Main.rand.NextFloat() <= 1.0f) // 100% шанс
                target.AddBuff(BuffID.Bleeding, 1800); // Кровотечение на 30 секунд

            // Случайный бафф для игрока
            int[] buffs = { BuffID.Regeneration, BuffID.Swiftness, BuffID.Ironskin, BuffID.ManaRegeneration, BuffID.MagicPower, BuffID.Thorns,
            BuffID.Archery,BuffID.WellFed,BuffID.PaladinsShield,BuffID.Honey,BuffID.RapidHealing,BuffID.Panic,BuffID.HeartLamp,BuffID.BeetleEndurance3,
            BuffID.BeetleMight3,BuffID.Lifeforce,BuffID.Endurance,BuffID.Rage,BuffID.Inferno,BuffID.Wrath,BuffID.Sunflower,BuffID.SoulDrain,BuffID.SolarShield3,
            BuffID.NebulaUpLife3,BuffID.NebulaUpMana3,BuffID.NebulaUpDmg3,BuffID.ParryDamageBuff,BuffID.HeartyMeal};
            int randomBuff = Main.rand.Next(buffs.Length);
            player.AddBuff(buffs[randomBuff], 14400); // Бафф на 240 секунд

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
