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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MovingSprite : SpriteBase
    {
        public enum Direction { North, South, East, West, Unknown, Stopped };

        protected EdgeAction edge;

        public MovingSprite()
            : base()
        {
            spd = new Vector2(0, 0);
            edge = EdgeAction.Stop;
        }

        public MovingSprite(Texture2D texture, Vector2 position, SpriteEffects effects,
                            double timeBetweenUpdates, Vector2 speed, EdgeAction edge)
            : base(texture, position, effects, timeBetweenUpdates)
        {

            this.spd = speed;
            this.edge = edge;

        }


        public Direction getDirection()
        {
            if (spd.Y == 0 && spd.X > 0)
                return Direction.East;
            else
                if (spd.Y == 0 && spd.X < 0)
                    return Direction.West;
                else
                    if (spd.X == 0 && spd.Y < 0)
                        return Direction.North;
                    else
                        if (spd.X == 0 && spd.Y > 0)
                            return Direction.South;
                        else
                            if (spd.X == 0 && spd.Y == 0)
                                return Direction.Stopped;
                            else
                                return Direction.Unknown;
        }

        //move sprite on the opposite direction
        public virtual void backup()
        {

        }
        //set speed
        public void setSpeed(int dx, int dy)
        {
            spd.X = dx;
            spd.Y = dy;
        }

        //reverse the direction 
        public void reverse()
        {
            // backUp();
            spd.X = -spd.X;  // turn around
            spd.Y = -spd.Y;
            if (fx != SpriteEffects.None)
                fx = SpriteEffects.None;
            else
                fx = SpriteEffects.FlipHorizontally;
        }


        //Turn right and left 

        public void turnLeft()
        {
            if (spd.Y == 0)
            {
                float temp = spd.X;
                spd.X = spd.Y;
                spd.Y = -temp;
            }
            else
            {
                float temp = spd.X;
                spd.X = spd.Y;
                spd.Y = temp;
            }

        }

        public void turnRight()
        {
            if (spd.Y == 0)
            {
                float temp = spd.X;
                spd.X = spd.Y;
                spd.Y = temp;
            }
            else
            {
                float temp = spd.X;
                spd.X = spd.Y;
                spd.Y = -temp;
            }
        }


        // set the speed to go in a compass direction
        //
        public void goNorth()
        {
            spd.Y = -Math.Max(Math.Abs(spd.X), Math.Abs(spd.Y));
            spd.X = 0;
        }
        public void goSouth()
        {
            spd.Y = Math.Max(Math.Abs(spd.X), Math.Abs(spd.Y));
            spd.X = 0;
        }
        public void goEast()
        {
            spd.X = Math.Max(Math.Abs(spd.X), Math.Abs(spd.Y));
            spd.Y = 0;
        }

        public void goWest()
        {
            spd.X = -Math.Max(Math.Abs(spd.X), Math.Abs(spd.Y));
            spd.Y = 0;
        }



        public override void Update(GameTime gameTime, Rectangle ClientBounds)
        {

            base.Update(gameTime, ClientBounds);
        }


    }
}