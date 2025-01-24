using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace smthcont.Content.Projectiles
{
    public class ClubsAce : ModProjectile
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

            // Меняем местами игрока и врага
            Vector2 tempPosition = player.Center;
            player.Center = target.Center;
            target.Center = tempPosition;

            // Дополнительный урон врагу
            target.StrikeNPC(100, 0f, 0);

            // Спавним гранаты вокруг врага через 0.5 секунд
            for (int i = 0; i < 6; i++)
            {
                Vector2 grenadePosition = target.Center + new Vector2(0, -16 * (i + 1)); // Расположение сверху
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), grenadePosition, Vector2.Zero, ProjectileID.Grenade, 50, 1f, player.whoAmI);
            }
        }
    }
}