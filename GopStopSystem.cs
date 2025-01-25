using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Players;
using smthcont.Content.NPCs.Enemies;
namespace smthcont.System
{
    public class GopStopSystem : ModSystem
    {
        private bool eventActive = false; // Переменная для отслеживания состояния события
        private int hooliganKillCount = 0; // Счетчик убитых хулиганов
        private const int KillRequirement = 50; // Требуемое количество убийств для завершения события

        public override void PreUpdateWorld()
        {
            // Проверяем, есть ли у игрока платиновая монета
            if (!eventActive && Main.LocalPlayer.HasItem(ItemID.PlatinumCoin))
            {
                if (Main.rand.NextBool(2)) // 50% шанс каждую секунду
                {
                    eventActive = true;
                    hooliganKillCount = 0;
                    Main.NewText("Событие ГопСтоп началось!", 255, 50, 50);
                }
            }

            // Завершаем событие, если достигнут лимит убийств
            if (eventActive && hooliganKillCount >= KillRequirement)
            {
                eventActive = false;
                Main.NewText("Событие ГопСтоп завершено!", 50, 255, 50);
            }
        }

        public bool IsEventActive()
        {
            return eventActive;
        }

        public void IncrementKillCount()
        {
            hooliganKillCount++;
        }
    }
}