using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Projectiles.Friendly;

namespace smthcont.Content.Items.Weapons.Melee
{
    public class ScytheOfAnnihilation : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15000; // Базовый урон
            Item.DamageType = DamageClass.Melee; // Тип урона ближнего боя
            Item.width = 50; // Ширина предмета
            Item.height = 50; // Высота предмета
            Item.useTime = 100; // Скорость атаки (чем меньше, тем быстрее)
            Item.useAnimation = 100; // Длительность анимации атаки
            Item.useStyle = ItemUseStyleID.Swing; // Тип использования (махание)
            Item.knockBack = 6f; // Отбрасывание
            Item.value = Item.buyPrice(0, 50, 0, 0); // Цена: 50 золотых
            Item.rare = ItemRarityID.Red; // Редкость предмета
            Item.UseSound = SoundID.Item71; // Звук атаки
            Item.autoReuse = true; // Автоматическая повторная атака  
            Item.shoot = ModContent.ProjectileType<ScytheOfAnnihilationProjectile>(); // Тип снаряда
            Item.shootSpeed = 10f; // Скорость снаряда
            Item.crit = 100;
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Стреляем вращающимся снарядом (ScytheOfAnnihilationProjectile)
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false; // Возвращаем false, чтобы не использовать стандартное поведение стрельбы
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 15); // 15 святых слитков
            recipe.AddIngredient(ItemID.SoulofNight, 10); // 10 Душ Ночи
            recipe.AddIngredient(ItemID.SoulofFright, 10); // 10 Душ Ужаса
            recipe.AddIngredient(ItemID.SoulofMight, 10); // 10 Душ Силы
            recipe.AddTile(TileID.MythrilAnvil); // Создается на мифриловом или орекалковом наковальне
            recipe.Register();
        }
    }
}
