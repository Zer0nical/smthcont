using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using smthcont.Content.Projectiles;
using System;
// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
namespace smthcont.Content.Items.Weapons.Melee
{ 
	public class Begin : ModItem
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
			Item.rare = ItemRarityID.Lime; //
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.crit = 25;
			Item.shoot = ModContent.ProjectileType<BeginProjectile>(); // Привязка к новому снаряду
            Item.shootSpeed = 0f; // Скорость снарядов (не используется)
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Наложение дебаффов
			target.AddBuff(BuffID.Bleeding, 300); // Кровотечение на 5 секунд
			target.AddBuff(BuffID.Confused, 60); // Конфузия на 1 секунду
			// Вампиризм: 100% шанс исцелить игрока на 2 хп
			player.statLife += 2; // Восстановление 2 хп
			player.HealEffect(2, true); // Визуальный эффект лечения
		}

		//связь со снарядом
		public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			// Определяем направление к курсору
			Vector2 targetPosition = Main.MouseWorld; // Позиция курсора в мире
			Vector2 direction = (targetPosition - player.Center).SafeNormalize(Vector2.Zero); // Направление к курсору

			// Настройки радиуса и начального угла
			float radius = 5 * 16f; // Радиус в пикселях (5 блоков)
			float baseAngle = direction.ToRotation(); // Угол направления к курсору

			// Генерация снарядов
			for (int i = 0; i < 5; i++) // 5 снарядов
			{
				// Расчёт угла для каждого снаряда
				float angle = baseAngle + MathHelper.ToRadians(72 * i); // Смещение на 72° для каждого снаряда
				Vector2 spawnDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); // Вектор направления
				Vector2 spawnPosition = player.Center + spawnDirection * radius; // Позиция снаряда

				// Создаём снаряд
				Projectile.NewProjectile(
					source, 
					spawnPosition, 
					spawnDirection * 12f, // Скорость снаряда
					type, 
					damage, 
					knockBack, 
					player.whoAmI
				);
			}

			return false; // Возвращаем false, чтобы предотвратить стандартный выстрел
		}
		

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeCrystal, 3); //3 LifeCrystal
			recipe.AddIngredient(ItemID.HeartreachPotion, 5);//5 HeartreachPotion
			recipe.AddIngredient(ItemID.GreaterHealingPotion, 50);//50 GreaterHealingPotion
			recipe.AddIngredient(ItemID.LargeRuby, 1);//1 LargeRuby
			recipe.AddIngredient(ItemID.GoldDust, 100);//100 GoldDust
			recipe.AddIngredient(ItemID.ViciousPowder, 30);//30 Vicious Powder
			recipe.AddIngredient(ItemID.Ichor, 150);//150 	Ichor
			recipe.AddIngredient(ItemID.HallowedBar, 24);//24	Hallowed Bar
			recipe.AddIngredient(ItemID.Cloud, 50);//50 	Cloud
			recipe.AddIngredient(ItemID.RubyStaff, 1);//1	RubyStaff
			//станция
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}