using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace smthcont.Content.Projectiles.Friendly
{
    public class SpadesAce : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.friendly = true;
            //Projectile.magic = true;
            Projectile.damage = 150;
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
                target.AddBuff(BuffID.Poisoned, 750);

            if (Main.rand.NextFloat() <= 1.0f) 
                target.AddBuff(BuffID.OnFire, 750);
                
            if (Main.rand.NextFloat() <= 1.0f) 
                target.AddBuff(BuffID.Bleeding, 750);

            if (Main.rand.NextFloat() <= 0.4f) 
                target.AddBuff(BuffID.Confused, 750); 

            if (Main.rand.NextFloat() <= 1.0f) 
                target.AddBuff(BuffID.Slow, 750);  

            if (Main.rand.NextFloat() <= 1.0f)   
                target.AddBuff(BuffID.Weak, 750);  

            if (Main.rand.NextFloat() <= 0.9f)   
                target.AddBuff(BuffID.BrokenArmor, 750);  

            if (Main.rand.NextFloat() <= 0.9f)   
                target.AddBuff(BuffID.CursedInferno, 750);  

            if (Main.rand.NextFloat() <= 0.9f)   
                target.AddBuff(BuffID.Frostburn, 750);  

            if (Main.rand.NextFloat() <= 1.0f)   
                target.AddBuff(BuffID.Burning, 750);  

            if (Main.rand.NextFloat() <= 0.85f)   
                target.AddBuff(BuffID.Ichor, 750); 

            if (Main.rand.NextFloat() <= 0.8f)   
                target.AddBuff(BuffID.Venom, 750);  

            if (Main.rand.NextFloat() <= 1.0f)   
                target.AddBuff(BuffID.Midas, 750);  

            if (Main.rand.NextFloat() <= 0.75f)   
                target.AddBuff(BuffID.Electrified, 750); 

            if (Main.rand.NextFloat() <= 0.5f)   
                target.AddBuff(BuffID.Webbed, 750);  

            if (Main.rand.NextFloat() <= 0.8f)   
                target.AddBuff(BuffID.ShadowFlame, 750);  

            if (Main.rand.NextFloat() <= 0.45f)   
                target.AddBuff(BuffID.VortexDebuff, 750);  

            if (Main.rand.NextFloat() <= 0.6f)   
                target.AddBuff(BuffID.WindPushed, 750);  

            if (Main.rand.NextFloat() <= 0.5f)   
                target.AddBuff(BuffID.BloodButcherer, 750);  

        }
    }
}