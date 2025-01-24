using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class SpadesKing : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.friendly = true;
            //Projectile.magic = true;
            Projectile.damage = 130;
            Projectile.penetrate = 3; // Пронзает 3 врагов
            Projectile.tileCollide = true; // Исчезает при столкновении с блоками
            Projectile.light = 0.5f; // Освещает
            Projectile.scale = 0.58f;
        }

        public override void AI()
        {
            // Добавляем гравитацию
            Projectile.velocity.Y += 0.45f; // Чем больше число, тем сильнее гравитация

            // Ограничиваем максимальную скорость падения
            if (Projectile.velocity.Y > 10f) 
            {
                Projectile.velocity.Y = 10f; // Максимальная скорость падения
            }
            Projectile.rotation += 0.3f; // Вращение вокруг оси
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextFloat() <= 1.0f) 
                target.AddBuff(BuffID.Poisoned,  600);

            if (Main.rand.NextFloat() <= 0.95f) 
                target.AddBuff(BuffID.OnFire,  600);
                
            if (Main.rand.NextFloat() <= 1.0f) 
                target.AddBuff(BuffID.Bleeding,  600);

            if (Main.rand.NextFloat() <= 0.35f) 
                target.AddBuff(BuffID.Confused,  600); 

            if (Main.rand.NextFloat() <= 0.95f) 
                target.AddBuff(BuffID.Slow,  600);  

            if (Main.rand.NextFloat() <= 1.0f)   
                target.AddBuff(BuffID.Weak,  600);  

            if (Main.rand.NextFloat() <= 0.85f)   
                target.AddBuff(BuffID.BrokenArmor,  600);  

            if (Main.rand.NextFloat() <= 0.8f)   
                target.AddBuff(BuffID.CursedInferno,  600);  

            if (Main.rand.NextFloat() <= 0.85f)   
                target.AddBuff(BuffID.Frostburn,  600);  

            if (Main.rand.NextFloat() <= 0.85f)   
                target.AddBuff(BuffID.Burning,  600);  

            if (Main.rand.NextFloat() <= 0.8f)   
                target.AddBuff(BuffID.Ichor,  600); 

            if (Main.rand.NextFloat() <= 0.8f)   
                target.AddBuff(BuffID.Venom,  600);  

            if (Main.rand.NextFloat() <= 0.9f)   
                target.AddBuff(BuffID.Midas,  600);  

            if (Main.rand.NextFloat() <= 0.65f)   
                target.AddBuff(BuffID.Electrified,  600); 

            if (Main.rand.NextFloat() <= 0.4f)   
                target.AddBuff(BuffID.Webbed,  600);  

            if (Main.rand.NextFloat() <= 0.75f)   
                target.AddBuff(BuffID.ShadowFlame,  600);  

            if (Main.rand.NextFloat() <= 0.32f)   
                target.AddBuff(BuffID.VortexDebuff,  600);  

            if (Main.rand.NextFloat() <= 0.4f)   
                target.AddBuff(BuffID.WindPushed,  600);  

            if (Main.rand.NextFloat() <= 0.25f)   
                target.AddBuff(BuffID.BloodButcherer,  600);  
        }
    }
}