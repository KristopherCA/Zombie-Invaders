using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ZombieInvaders
{
    class bullet : SpriteBase
    {
        public bullet()
        {

        }
        public bullet(Texture2D txtre, Rectangle pstn, Vector2 spd, double msprfrme, Color tnt)
            : base(txtre, pstn, spd, msprfrme, tnt, SpriteEffects.None)
        {
            eg = EdgeAction.Die;
        }
        public override void Update(GameTime gameTime, Rectangle ClientBounds)
        {



            base.Update(gameTime, ClientBounds);



        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

        }



    }
}
