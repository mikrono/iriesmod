﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace iriesmod.Content.Items.Weapons.Summon
{
	public class HornetDefenderStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Horent Defender Staff");
			Tooltip.SetDefault("Place a Hornet Defender that throws bee hive into the enemy");
		}

		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.knockBack = 3f;
			Item.mana = 10;
			Item.width = 56;
			Item.height = 52;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(gold: 1, silver: 30);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item44;

			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.sentry = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.Summon.HornetDefender>();
		}

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI);
			player.UpdateMaxTurrets();

			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();

			recipe.AddIngredient(ItemID.BeeWax, 18);
			recipe.AddIngredient(ItemID.Stinger, 6);

			recipe.AddTile(TileID.HoneyDispenser);

			recipe.Register();
		}
	}
}
