using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.NPCs.Enemies
{
    public class Hooligan : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 18; // Ширина хитбокса NPC
            NPC.height = 40; // Высота хитбокса
            NPC.lifeMax = 1500; // Здоровье
            NPC.damage = 70; // Атака
            NPC.defense = 25; // Защита
            NPC.knockBackResist = 0.2f; // Сопротивление отбрасыванию
            NPC.value = Item.buyPrice(0, 0, 5, 0); // Цена (5 серебра)
            NPC.aiStyle = 3; // AI стиль (преследование игрока)
            NPC.HitSound = SoundID.NPCHit1; // Звук при ударе
            NPC.DeathSound = SoundID.NPCDeath1; // Звук при смерти
            //AnimationType = NPCID.Guide; // Тип анимации (как у Гида)
            NPC.velocity.X = NPC.direction * 6.5f;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 16;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
        }
        public override void FindFrame(int frameHeight)
        {
            // Увеличение счетчика кадров
            NPC.frameCounter++;

            // Если NPC стоит на земле
            if (NPC.velocity.Y == 0)
            {
                if (Math.Abs(NPC.velocity.X) > 0.5f) // Если NPC движется
                {
                    if (NPC.frameCounter >= 5) // Скорость смены кадров (5 = быстрая смена)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight; // Переход к следующему кадру
                        if (NPC.frame.Y < frameHeight * 2 || NPC.frame.Y >= frameHeight * 16) // Кадры движения (3-16)
                        {
                            NPC.frame.Y = frameHeight * 2; // Сброс на начало движения
                        }
                    }
                }
                else
                {
                    // Если NPC стоит на месте
                    NPC.frame.Y = 0; // Первый кадр
                }
            }
            else
            {
                // Если NPC падает
                NPC.frame.Y = frameHeight; // Второй кадр
            }
        }
        public override void OnKill()
        {
            // Дроп 1-5 блоков земли
            int dropAmount = Main.rand.Next(1, 6);
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.DirtBlock, dropAmount);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            /*// Спавнится на поверхности днем
            if (!Main.dayTime || spawnInfo.Player.ZoneUndergroundHeight)
                return 0f;*/
            return 0.8f; // 10% шанс спавна
        }
    }
}