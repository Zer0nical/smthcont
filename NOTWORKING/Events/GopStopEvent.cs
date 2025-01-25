/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Players;
using smthcont.Content.NPCs.Enemies;
namespace smthcont.Content.Events
{
    public class GopStopEvent
    {
        public static bool IsActive = false; // Активно ли событие?
        private static int hooliganKillCount = 0; // Убито мобов
        private static int spawnRate = 60; // Время между спавнами (60 тиков = 1 секунда)

        // Проверка активации события
        public static void TryStartEvent(Player player)
        {
            if (!IsActive && player.GetModPlayer<GopStopPlayer>().HasPlatinumCoin) 
            {
                if (Main.rand.NextFloat() < 0.5f) // 50% шанс каждую секунду
                {
                    IsActive = true;
                    hooliganKillCount = 0;
                    Main.NewText("ГопСтоп начался!", 255, 50, 50);
                    //Main.musicBox = ModContent.GetInstance<smthcont>().GetSoundSlot(SoundType.Music, "Sounds/Music/Gop");
                }
            }
        }

        // Окончание события
        public static void CheckEndEvent()
        {
            if (IsActive && hooliganKillCount >= 50) 
            {
                IsActive = false;
                Main.NewText("ГопСтоп закончился!", 50, 255, 50);
            }
        }

        // Увеличение счетчика убийств
        public static void OnHooliganKilled()
        {
            if (IsActive)
            {
                hooliganKillCount++;
                CheckEndEvent();
            }
        }

        public static void SpawnHooligans()
        {
            if (IsActive && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player.active && !player.dead)
                    {
                        // Попытка спавна моба рядом с игроком
                        if (Main.rand.NextBool(5)) // 20% шанс каждые 60 тиков (1 секунда)
                        {
                            Vector2 spawnPosition = player.Center + new Vector2(Main.rand.Next(-800, 800), -200); // 800 блоков в стороны и 200 вверх
                            if (Main.tile[(int)spawnPosition.X / 16, (int)spawnPosition.Y / 16].HasUnactuatedTile) // Проверка на свободное место
                            {
                                NPC.NewNPC(null, (int)spawnPosition.X, (int)spawnPosition.Y, ModContent.NPCType<Hooligan>());
                            }
                        }
                    }
                }
            }
        }
    }
}*/