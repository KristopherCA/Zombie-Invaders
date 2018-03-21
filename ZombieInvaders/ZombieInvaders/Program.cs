using System;
using ZombieInvaders;

namespace ZombieInvaders
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ZmbeInvdrs game = new ZmbeInvdrs())
            {
                game.Run();
            }
        }
    }
#endif
}

