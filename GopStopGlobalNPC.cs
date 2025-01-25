using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Players;
using smthcont.Content.NPCs.Enemies;
namespace smthcont.NPCs
{
    public class GopStopGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            // Проверяем, активен ли ивент ГопСтоп
            if (ModContent.GetInstance<System.GopStopSystem>().IsEventActive() &&
                npc.type == ModContent.NPCType<Hooligan>())
            {
                // Увеличиваем счетчик убийств
                ModContent.GetInstance<System.GopStopSystem>().IncrementKillCount();
            }
        }
    }
}