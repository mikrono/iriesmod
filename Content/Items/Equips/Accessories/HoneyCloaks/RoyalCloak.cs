﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using iriesmod.Common.players;
using iriesmod.Common.Utils;
using iriesmod.Content.Items.Materials;

namespace iriesmod.Content.Items.Equips.Accessories.HoneyCloaks
{
	public class RoyalCloak : HoneyCloakTemplate
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Royal Cloak");
			Tooltip.SetDefault("Increases bee damage by 10%\nReleases bees and douses the user in honey when damaged");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 30;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 5;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			iriesplayer modplayer = player.Getiriesplayer();

			modplayer.beeDamage += 0.10f;
			modplayer.RoyalCloak = Item;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BeeCloak);
			recipe.AddIngredient(ItemID.HoneyComb);
			recipe.AddIngredient(ModContent.ItemType<RoyalJelly>(), 12);
			recipe.AddIngredient(ItemID.HoneyBlock, 25);
			recipe.AddTile(TileID.HoneyDispenser);
			recipe.Register();
		}
	}
}
