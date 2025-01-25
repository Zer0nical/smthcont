using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using smthcont.Content.Events;
namespace smthcont.Content.NPCs.Enemies
{
    public class Hooligan : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.lifeMax = 210; // Здоровье уменьшено
            NPC.damage = 15;    // Урон уменьшен
            NPC.defense = 14;
            NPC.knockBackResist = 0.1f; // Устойчивость к отбрасыванию
            NPC.value = Item.buyPrice(0, 0, 15, 0);
            NPC.aiStyle = -1;  // Кастомный AI
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 16;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            // Если игрок мертв или не активен, останавливаемся
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                NPC.velocity.X = 0f;
                return;
            }

            Vector2 directionToPlayer = player.Center - NPC.Center;
            float distanceToPlayer = directionToPlayer.Length();
            directionToPlayer.Normalize();

            // Увеличенная скорость передвижения
            float moveSpeed = 8f;

            // Движение к игроку
            if (distanceToPlayer > 50f)
            {
                NPC.velocity.X = directionToPlayer.X * moveSpeed;
            }

            // Проверка препятствий и перепрыгивание
            if (Collision.SolidCollision(NPC.position + new Vector2(0, 1), NPC.width, NPC.height))
            {
                if (NPC.velocity.Y == 0) // Если на земле
                {
                    NPC.velocity.Y = -7f; // Высокий прыжок
                }
            }

            // Прыжок в сторону игрока, если слишком близко
            if (distanceToPlayer <= 100f && NPC.velocity.Y == 0)
            {
                NPC.velocity.Y = -4f;
                NPC.velocity.X = directionToPlayer.X * (moveSpeed + 3f); // Быстрее к игроку
            }

            // Устанавливаем направление NPC
            NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
            NPC.spriteDirection = NPC.direction;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;

            // Анимация движения
            if (NPC.velocity.Y == 0)
            {
                if (Math.Abs(NPC.velocity.X) > 0.5f)
                {
                    if (NPC.frameCounter >= 5) // Медленная смена кадров
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                        if (NPC.frame.Y >= frameHeight * 16) // Цикл кадров
                        {
                            NPC.frame.Y = frameHeight * 2;
                        }
                    }
                }
                else
                {
                    // Кадр, если NPC стоит на месте
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                // Кадр в прыжке
                NPC.frame.Y = frameHeight;
            }
        }

        public override void OnKill()
        {
            int dropAmount = Main.rand.Next(3, 11); // Выпадает 3-10 блоков земли
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.DirtBlock, dropAmount);
            GopStopEvent.OnHooliganKilled();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime || spawnInfo.SpawnTileY > Main.worldSurface)
                return 0f;
            return 0.2f; // 20% шанс спавна
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Накладываем дебафф Broken Armor на 5 секунд
            target.AddBuff(BuffID.BrokenArmor, 300);

            // Воровство денег
            int stolenCoins = Main.rand.Next(1, 51); // Случайное число монет
            if (stolenCoins > 0)
            {
                // Проверяем, сколько денег у игрока
                if (Main.LocalPlayer.HasItem(ItemID.CopperCoin))
                {
                    Main.NewText($"Хулиган украл {stolenCoins} монет!", Microsoft.Xna.Framework.Color.Red);
                }
            }
        }
    }
}
