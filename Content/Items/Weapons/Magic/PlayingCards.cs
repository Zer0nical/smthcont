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
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.mana = 2;
            Item.shoot = ModContent.ProjectileType<HeartsAce>(); // По умолчанию
            Item.shootSpeed = 30f;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            Vector2 targetPosition = Main.MouseWorld;
            Vector2 direction = (targetPosition - position).SafeNormalize(Vector2.UnitX);
            float baseAngle = direction.ToRotation();

            // Выбор случайных карт
            int[] cardTypes = new int[]
            {
                ModContent.ProjectileType<HeartsAce>(),
                ModContent.ProjectileType<SpadesAce>(),
                ModContent.ProjectileType<DiamondsAce>(),
                ModContent.ProjectileType<ClubsAce>()
            };

            for (int i = 0; i < 3; i++)
            {
                float angleOffset = MathHelper.ToRadians(30 * (i - 1));
                Vector2 cardDirection = direction.RotatedBy(angleOffset);
                int randomCard = Main.rand.Next(cardTypes.Length);
                Projectile.NewProjectile(
                    source,
                    position,
                    cardDirection * Item.shootSpeed,
                    cardTypes[randomCard],
                    damage,
                    knockBack,
                    player.whoAmI
                );
            }

            return false; // Предотвращает стандартное поведение стрельбы
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