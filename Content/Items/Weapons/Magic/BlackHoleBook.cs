using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Projectiles.Friendly;

namespace smthcont.Content.Items.Weapons.Magic
{
    public class PlayingCards : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.knockBack = 8;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.mana = 10;
            Item.shoot = ModContent.ProjectileType<HeartsAce>(); // По умолчанию
            Item.shootSpeed = 24f;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // Основное направление к курсору
            Vector2 targetPosition = Main.MouseWorld;
            Vector2 direction = (targetPosition - player.Center).SafeNormalize(Vector2.Zero);
            
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
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}