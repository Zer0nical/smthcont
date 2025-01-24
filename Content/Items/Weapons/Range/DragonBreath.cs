using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using smthcont.Content.Projectiles;
using System;

namespace smthcont.Content.Items 
{
    public class DragonBreath : ModItem 
	{
		public override void SetStaticDefaults () 
		{
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults () {
			int width = 56; int height = 20;
			Item.Size = new Vector2(width, height);

			Item.DamageType = DamageClass.Ranged;
			Item.damage = 148;

			Item.knockBack = 1.5f;
			Item.noMelee = true;

			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = Item.useTime = 13;

			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;

			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(gold: 6, silver: 15);

			Item.shoot = ProjectileID.DD2PhoenixBowShot;
			Item.shootSpeed = 150f;
		}

		public override Vector2? HoldoutOffset ()
			=> new Vector2(-5, 0);

		public override void ModifyShootStats (Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			Vector2 _velocity = Utils.SafeNormalize(new Vector2(velocity.X, velocity.Y), Vector2.Zero);
			position += new Vector2(-_velocity.Y, _velocity.X) * (2.5f * player.direction);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PhoenixBlaster, 1); //1 PhoenixBlaster 
			recipe.AddIngredient(ItemID.DD2PhoenixBow, 1);// 1 DD2PhoenixBow
			recipe.AddIngredient(ItemID.Fireblossom, 7);//Fireblossom * 7
			recipe.AddIngredient(ItemID.SoulofFright, 20);//SoulofFright * 20
			recipe.AddIngredient(ItemID.FireFeather, 5);//5 FireFeather
			recipe.AddIngredient(ItemID.ChlorophyteBar, 12);//12 ChlorophyteBar
			//station
			recipe.AddTile(TileID.HellstoneBrick);
			recipe.Register();
		}
	}
}