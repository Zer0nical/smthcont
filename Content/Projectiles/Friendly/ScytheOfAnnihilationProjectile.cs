using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace smthcont.Content.Projectiles.Friendly
{
    public class ScytheOfAnnihilationProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20 * 5; // Увеличиваем ширину снаряда в 5 раз
            Projectile.height = 20 * 5; // Увеличиваем высоту снаряда в 5 раз
            Projectile.friendly = true; // Снаряд дружелюбный (атакует врагов)
            Projectile.hostile = false; // Не атакует игроков
            Projectile.DamageType = DamageClass.Melee; // Тип урона ближнего боя
            Projectile.penetrate = -1; // Снаряд исчезает после первого попадания
            Projectile.timeLeft = 600; // Время жизни снаряда (10 секунд)
            Projectile.tileCollide = false; // Не сталкивается с блоками
            Projectile.ignoreWater = true; // Игнорирует воду
            Projectile.extraUpdates = 1; // Ускоряем обработку снаряда
            Projectile.scale = 1.26f;
            Projectile.ArmorPenetration = 9999;
        }

        public override void AI()
        {
            // Вращение снаряда
            Projectile.rotation += 0.4f * Projectile.direction;
            //Projectile.velocity *= 1.05f; // Увеличиваем скорость снаряда на 5% каждый кадр
            // Плавное движение вперёд
            Projectile.velocity *= 0.99f; // Постепенное замедление

            // Эффект частиц
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Lava);
                dust.velocity *= 0.5f;
                dust.scale = 1.5f;
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Наносим 50% от текущего здоровья моба
            //int healthBasedDamage = (int)(target.life * 0.5f); // 50% от текущего здоровья

            // Обновляем урон
            int healthBasedDamage = (int)(target.life * 0.5f); // 50% от текущего здоровья

            // Устанавливаем новый урон
            hit.Damage = healthBasedDamage;  // Изменяем урон на основе здоровья цели
                damageDone = healthBasedDamage;
            //hit.damageDone = healthBasedDamage;
            //target.Armor = 0;
            // Применяем дебаффы
            target.AddBuff(BuffID.Poisoned, 750);
            target.AddBuff(BuffID.OnFire, 750);
            target.AddBuff(BuffID.Bleeding, 750);
            target.AddBuff(BuffID.Confused, 750); 
            target.AddBuff(BuffID.Slow, 750);  
            target.AddBuff(BuffID.Weak, 750);  
            target.AddBuff(BuffID.BrokenArmor, 750);  
            target.AddBuff(BuffID.CursedInferno, 750);     
            target.AddBuff(BuffID.Frostburn, 750);   
            target.AddBuff(BuffID.Burning, 750);     
            target.AddBuff(BuffID.Ichor, 750); 
            target.AddBuff(BuffID.Venom, 750);  
            target.AddBuff(BuffID.Midas, 750);  
            target.AddBuff(BuffID.Electrified, 750);  
            target.AddBuff(BuffID.Webbed, 750);  
            target.AddBuff(BuffID.ShadowFlame, 750);  
            target.AddBuff(BuffID.VortexDebuff, 750);  
            target.AddBuff(BuffID.WindPushed, 750);  
            target.AddBuff(BuffID.BloodButcherer, 750);  
        }
    }
}

        /*public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            // Наносим урон в размере 50% от текущего здоровья NPC
        }*/