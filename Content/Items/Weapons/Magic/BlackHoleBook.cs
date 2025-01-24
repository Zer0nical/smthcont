using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Projectiles.Friendly;

namespace smthcont.Content.Items.Weapons.Magic
{
    public class BlackHoleBook : ModItem
    {
        private const int Cooldown = 60; // 10 секунд (600 тиков)
        private int lastUseTime; // Отслеживание времени последнего использования

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
            Item.mana = 0; // Мана будет обнуляться вручную
            Item.shoot = ModContent.ProjectileType<BlackHole>(); // Тип снаряда (черная дыра)
            Item.shootSpeed = 24f;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
            // Проверяем, прошел ли кулдаун
            if (Main.GameUpdateCount - lastUseTime >= Cooldown)
            {
                return true;
            }

            return false; // Не позволяет использовать до окончания кулдауна
        }
    

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // Проверка маны игрока
            if (player.statMana > 0)
            {
                // Полностью истощить ману игрока
                int manaToUse = player.statMana;
                player.statMana = 0;
                player.manaRegenDelay = 300; // Увеличение задержки регенерации маны

                // Создать черную дыру в месте курсора
                Vector2 targetPosition = Main.MouseWorld;
                Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<BlackHole>(), damage, player.whoAmI);

                // Обновляем время последнего использования
                lastUseTime = (int)Main.GameUpdateCount;

                return false; // Предотвращает стандартный выстрел
            }

            return false; // Если маны недостаточно, ничего не делать
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