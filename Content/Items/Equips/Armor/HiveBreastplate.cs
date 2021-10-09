﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using iriesmod.Common.Players;

namespace iriesmod.Content.Items.Equips.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class HiveBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hive Breastplate");
			Tooltip.SetDefault("Increases bee damage by 4%\nIncreases your max number of minions by 1");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = Item.sellPrice(silver: 50);
			item.rare = ItemRarityID.Green;
		}

		public override void UpdateEquip(Player player)
		{
			iriesplayer.beeDamage += 0.04f;
			player.maxMinions++;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Stinger, 10);
			recipe.AddIngredient(ItemID.Hive, 20);
			recipe.AddIngredient(ItemID.HoneyBlock, 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}