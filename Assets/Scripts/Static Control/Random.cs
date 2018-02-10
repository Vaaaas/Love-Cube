using System;

namespace MersenneTwister
{
    internal class Random : MT19937
    {
        public Random()
        {
            var time = (ulong) (DateTime.Now - DateTime.Parse("1970-01-01 00:00:00")).TotalSeconds;
            Console.WriteLine(time);
            ulong[] key = {time};
            init_by_array(key, 1);
        }

        public int GetRandom(int n)
        {
            var num = (int) (genrand_real1()*n);
            return num;
        }
    }
}