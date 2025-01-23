using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace smthcont.Content.Items
{ 
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Begin : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.smthcont.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 85;
			Item.DamageType = DamageClass.Melee;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 14;
			Item.useAnimation = 14;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 9;
			Item.value = Item.buyPrice(silver: 55);
			Item.rare = ItemRarityID.Master;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeCrystal, 5); //5 LifeCrystal
			//other ingr
			recipe.AddIngredient(ItemID.HeartLantern, 5);//5 HeartLantern
			recipe.AddIngredient(ItemID.HeartStatue, 1);//1 HeartStatue
			recipe.AddIngredient(ItemID.HeartreachPotion, 5);//5 HeartreachPotion
			recipe.AddIngredient(ItemID.GreaterHealingPotion, 50);//50 GreaterHealingPotion
			recipe.AddIngredient(ItemID.Goggles, 1);//1 Goggles
			recipe.AddIngredient(ItemID.LargeRuby, 1);//1 LargeRuby
			recipe.AddIngredient(ItemID.GoldDust, 200);//200 GoldDust
			recipe.AddIngredient(ItemID.ViciousPowder, 20);//20 Vicious Powder
			recipe.AddIngredient(ItemID.Ichor, 100);//100 	Ichor
			recipe.AddIngredient(ItemID.HallowedBar, 20);//20 	Hallowed Bar
			recipe.AddIngredient(ItemID.Cloud, 50);//50 	Cloud
			recipe.AddIngredient(ItemID.RubyStaff, 1);//1	RubyStaff
			//station
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}
