using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using smthcont.Content.Projectiles;
using System;

namespace smthcont.Content.Items.Weapons.Melee
{
    public class PerfectRubyBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Melee;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(gold: 8, silver: 65);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 25;
            Item.shoot = ModContent.ProjectileType<PerfectRubyBladeProjectile>();
            Item.shootSpeed = 0f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Наложение дебаффов
            target.AddBuff(BuffID.Bleeding, 300); // Кровотечение на 5 секунд
            target.AddBuff(BuffID.Confused, 60); // Конфузия на 1 секунду
            target.AddBuff(BuffID.CursedInferno, 180); // Проклятый огонь на 3 секунды

            // Вампиризм: 100% шанс исцелить игрока на 2 хп
            player.statLife += 2;
            player.HealEffect(2, true);
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // Основное направление к курсору
            Vector2 targetPosition = Main.MouseWorld;
            Vector2 direction = (targetPosition - player.Center).SafeNormalize(Vector2.Zero);

            // Спавн трёх снарядов
            for (int i = -1; i <= 1; i++)
            {
                // Угол для каждого снаряда
                float angleOffset = MathHelper.ToRadians(65 * i); // Смещение на 65° вверх/вниз
                Vector2 rotatedDirection = direction.RotatedBy(angleOffset);

                // Спавн снаряда
                Projectile.NewProjectile(
                    source,
                    player.Center,
                    rotatedDirection * 12f, // Скорость снаряда
                    ModContent.ProjectileType<PerfectRubyBladeProjectile>(), // Новый снаряд
                    damage,
                    knockBack,
                    player.whoAmI
                );
            }

            // Спавн дополнительных эффектов на месте курсора
            Projectile.NewProjectile(
                source,
                Main.MouseWorld,
                Vector2.Zero,
                ProjectileID.RainbowCrystalExplosion, // Эффект радуги
                0,
                0,
                player.whoAmI
            );

            Projectile.NewProjectile(
                source,
                Main.MouseWorld,
                Vector2.Zero,
                ProjectileID.HallowBossRainbowStreak, // Радужная streak (вражеская переделана на дружественную)
                0,
                0,
                player.whoAmI
            );

            return false; // Предотвращает стандартный выстрел
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HolyWater, 999);
            recipe.AddIngredient(ItemID.CrossNecklace, 1);
            recipe.AddIngredient(ModContent.ItemType<Begin>(), 1);
            recipe.AddIngredient(ItemID.CelestialStone, 1);
            recipe.AddIngredient(ItemID.BottomlessShimmerBucket, 1);
            recipe.AddIngredient(ItemID.AegisCrystal, 1);
            recipe.AddIngredient(ItemID.QueenSlimeCrystal, 1);
            recipe.AddTile(TileID.LihzahrdFurnace);
            recipe.Register();
        }
    }
}