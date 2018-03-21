using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombieInvaders
{
    class UserControlledSprite : SpriteBase
    {
        public UserControlledSprite()
        {

        }

        public UserControlledSprite(Texture2D txtre, Rectangle pstn, double lstfrme, Color tnt)
             :base(txtre,pstn,Vector2.Zero,lstfrme, tnt,SpriteEffects.None)
        {
            
        }
        public int getXPostion()
        {
            return pstn.X;
        }
        public override void Update(GameTime gameTime, Rectangle ClientBounds)
        {
            const int shotgunspd = 3;

            KeyboardState keystate = Keyboard.GetState();


            if (keystate.IsKeyDown(Keys.Left))
            {

                pstn.X -= shotgunspd;
               
            }

            if (keystate.IsKeyDown(Keys.Right))
            {
                
                pstn.X += shotgunspd;
                
            }

            if (pstn.X < 0)
                pstn.X = 0;

            if (pstn.X > 815)
                pstn.X = 815;

          

            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

        }
        

        }
    }

