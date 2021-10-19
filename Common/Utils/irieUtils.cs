﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using iriesmod.Common.List;
using iriesmod.Common.ID;
using Terraria.ID;
using iriesmod.Common.players;

namespace iriesmod.Common.Utils
{
    public static class irieUtils
    {
        public static iriesplayer Getiriesplayer(this Player player)
        {
            return player.GetModPlayer<iriesplayer>();
        }
        public static void ProjectileStickToPlatform(this Projectile proj)
        {
            Tile tile = Framing.GetTileSafely((int)proj.position.X, (int)proj.position.Y);
            if (TileID.Sets.Platforms[tile.type])
            {
                proj.velocity = new(0, 0);
            }
        }

        public static NPC GetTarget(Projectile proj, float maxDistance, out float distance, out Vector2 TargetCenter, out bool is_target)
        {

            distance = maxDistance;
            is_target = false;
            TargetCenter = proj.Center;
            NPC retNPC;


            retNPC = proj.OwnerMinionAttackTargetNPC;
            if (retNPC != null && retNPC.CanBeChasedBy(proj))
            {
                float distanceCompare = Vector2.Distance(retNPC.Center, proj.Center);
                float tiledistance = distance * 3f;
                if (distanceCompare < tiledistance && !is_target && Collision.CanHitLine(proj.position, proj.width, proj.height, retNPC.position, retNPC.width, retNPC.height))
                {
                    distance = distanceCompare;
                    TargetCenter = retNPC.Center;
                    is_target = true;
                }
            }

            if (!is_target)
            {
                for (int nPCindex = 0; nPCindex < 200; nPCindex++)
                {
                    retNPC = Main.npc[nPCindex];
                    if (retNPC.CanBeChasedBy(proj))
                    {
                        float distanceCompare2 = Vector2.Distance(retNPC.Center, proj.Center);
                        if (!(distanceCompare2 >= distance) && Collision.CanHitLine(proj.position, proj.width, proj.height, retNPC.position, retNPC.width, retNPC.height))
                        {
                            distance = distanceCompare2;
                            TargetCenter = retNPC.Center;
                            is_target = true;
                        }
                    }
                }
            }


            return retNPC;
        }

        public static void BeeSpawn(Player player, int minBeeDamage, int maxBeeDamage, Item item, int typeofbee = -1)
        {
            // bool makeStrongBee;
            bool strongBees = player.strongBees;
            int HurtNumberBee = 1 + Main.rand.Next(3);
            float HurtBeeDamage = Main.rand.Next(minBeeDamage, maxBeeDamage + 1);


            if (strongBees && Main.rand.Next(3) == 0)
            {
                HurtNumberBee++;
            }


            if (strongBees)
            {
                HurtBeeDamage += 5f;
            }
            if (Main.expertMode)
            {
                HurtBeeDamage *= 1.5f;
            }
            if (typeofbee < 0)
            {
                typeofbee = player.beeType();
            }
            for (int i = 0; i < HurtNumberBee; i++)
            {
                float speedX = Main.rand.Next(-35, 36) * 0.02f;
                float speedY = Main.rand.Next(-35, 36) * 0.02f;
                Vector2 velocity = new Vector2(speedX, speedY);
                Projectile.NewProjectile(player.GetProjectileSource_Item(item), player.position, velocity, typeofbee, BeeDamage(player, (int)HurtBeeDamage), 0f, Main.myPlayer);
            }


            /*
            int beeType()
            {
                if (strongBees && Main.rand.Next(2) == 0)
                {
                    makeStrongBee = true;
                    return 566;
                }

                makeStrongBee = false;
                return 181;
            }
            int beeDamage(int damage)
            {
                if (makeStrongBee)
                {
                    return damage + Main.rand.Next(1, 4);
                }
                
                return damage + Main.rand.Next(2);
            }
            */


        }
        public static int[] BeeDebuff(Player player)
        {
            iriesplayer modplayer = player.Getiriesplayer();
            switch (modplayer.BeeBackpack)
            {
                case irieItemID.ObsidianHivePack:
                    return new int[]{ 1, BuffID.OnFire };
                case irieItemID.CursedFlameHivePack:
                    return new int[] { 1, BuffID.CursedInferno };
                case irieItemID.IchorHivePack:
                    return new int[] { 1, BuffID.Ichor };
                case irieItemID.MechaHivePack:
                    return new int[] { 1, BuffID.CursedInferno, BuffID.Ichor };
                case irieItemID.VenomHivePack:
                    return new int[] { 1, BuffID.Venom };
                case irieItemID.BeetleHivePack:
                    return new int[] { 1, BuffID.Venom };
                case irieItemID.StardustHivePack:
                    return new int[] { 1, BuffID.OnFire, BuffID.CursedInferno, BuffID.Ichor, BuffID.Venom };
            }
            return new int[] { 0 };
        }
        public static int BeeDamage(Player player, int damage)
        {
            iriesplayer modplayer = player.Getiriesplayer();

            int beePackDamage = 0;

            switch (modplayer.BeeBackpack)
            {
                case irieItemID.ObsidianHivePack:
                    beePackDamage += Main.rand.Next(2, 9);
                    break;
                case irieItemID.CursedFlameHivePack:
                    beePackDamage += Main.rand.Next(5, 11);
                    break;
                case irieItemID.IchorHivePack:
                    beePackDamage += Main.rand.Next(5, 11);
                    break;
                case irieItemID.MechaHivePack:
                    beePackDamage += Main.rand.Next(10, 16);
                    break;
                case irieItemID.VenomHivePack:
                    beePackDamage += Main.rand.Next(17, 23);
                    break;
                case irieItemID.BeetleHivePack:
                    beePackDamage += Main.rand.Next(26, 32);
                    break;
                case irieItemID.StardustHivePack:
                    beePackDamage += Main.rand.Next(36, 42);
                    break;
            }

            return (int)((damage + beePackDamage) * (1f + modplayer.beeDamage));
        }
        public static int BeePenetrate(short BeeBackpack)
        {
            switch (BeeBackpack)
            {
                case irieItemID.ObsidianHivePack:
                    return 1;
                case irieItemID.CursedFlameHivePack:
                case irieItemID.IchorHivePack:
                    return 2;
                case irieItemID.MechaHivePack:
                    return 3;
                case irieItemID.VenomHivePack:
                case irieItemID.BeetleHivePack:
                case irieItemID.StardustHivePack:
                    return 4;
            }

            return 0;
        }

    }
}
