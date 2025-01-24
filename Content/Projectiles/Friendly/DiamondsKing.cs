using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class DiamondsKing : ModProjectile
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
            // Добавляем гравитацию
            Projectile.velocity.Y += 0.8f; // Чем больше число, тем сильнее гравитация

            // Ограничиваем максимальную скорость падения
            if (Projectile.velocity.Y > 10f) 
            {
                Projectile.velocity.Y = 10f; // Максимальная скорость падения
            }
            Projectile.rotation += 0.68f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.rand.NextFloat() <= 0.9f) // 90% шанс
                target.AddBuff(BuffID.Bleeding, 900); // Кровотечение на 15 секунд

            // Случайный бафф для игрока
            int[] buffs = { BuffID.Regeneration, BuffID.Swiftness, BuffID.Ironskin, BuffID.ManaRegeneration, BuffID.MagicPower, BuffID.Thorns,
            BuffID.Archery,BuffID.WellFed,BuffID.PaladinsShield,BuffID.Honey,BuffID.RapidHealing,BuffID.Panic,BuffID.HeartLamp,BuffID.BeetleEndurance2,
            BuffID.BeetleMight2,BuffID.Lifeforce,BuffID.Endurance,BuffID.Rage,BuffID.Inferno,BuffID.Wrath,BuffID.Sunflower,BuffID.SolarShield2,
            BuffID.NebulaUpLife2,BuffID.NebulaUpMana2,BuffID.NebulaUpDmg2,BuffID.ParryDamageBuff};
            int randomBuff = Main.rand.Next(buffs.Length);
            player.AddBuff(buffs[randomBuff], 12000); // Бафф на 200 секунд

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
