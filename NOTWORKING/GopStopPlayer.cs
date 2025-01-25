/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Events;
namespace smthcont.Content.Players
{
    public class GopStopPlayer : ModPlayer
    {
        public bool HasPlatinumCoin => Player.CountItem(ItemID.PlatinumCoin) > 0;

        public override void PostUpdate()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                GopStopEvent.TryStartEvent(Player); // Проверка события каждую секунду
            }
        }
    }
}*/