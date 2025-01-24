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
            Item.damage = 135;
            Item.DamageType = DamageClass.Melee;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(gold: 12, silver: 35);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 50;
            Item.shoot = ModContent.ProjectileType<PerfectRubyBladeProjectile>();
            Item.shootSpeed = 0f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Наложение дебаффов
            target.AddBuff(BuffID.Bleeding, 300); // Кровотечение на 5 секунд
            target.AddBuff(BuffID.Confused, 300); // Конфузия на 5 секунд
			target.AddBuff(BuffID.PotionSickness, 6000); // без лечения на 100 секунд
			target.AddBuff(BuffID.Cursed, 180); // Без оружия на 3 секунды
			target.AddBuff(BuffID.OnFire, 600); // Огонь на 10 секунд
			target.AddBuff(BuffID.Slow, 300); // Замедление на 5 секунд
			target.AddBuff(BuffID.ChaosState, 6000); // без тп на 100 секунд

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
            for (int i = -2; i <= 2; i++)
            {
                // Угол для каждого снаряда
                float angleOffset = MathHelper.ToRadians(27 * i); // Смещение на 55° вверх/вниз
                Vector2 rotatedDirection = direction.RotatedBy(angleOffset);

                // Спавн снаряда
                Projectile.NewProjectile(
                    source,
                    player.Center,
                    rotatedDirection * 25f, // Скорость снаряда
                    ModContent.ProjectileType<PerfectRubyBladeProjectile>(), // Новый снаряд
                    damage,
                    knockBack,
                    player.whoAmI
                );
            }

			for (int i = 0; i <= 2; i++)
            {
                Projectile.NewProjectile(
					source,
					Main.MouseWorld,
					Vector2.Zero,
					ProjectileID.FairyQueenMagicItemShot, // Эффект радуги
					0,
					0,
					player.whoAmI
					);
			}

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