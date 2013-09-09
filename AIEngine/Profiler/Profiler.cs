using System.Diagnostics;
using System.Timers;


namespace AIEngine.Profiler
{
    public static class Profiler
    {
        private static Timer Timer { get; set; }
        private static string Name { get; set; }
        private static long MiliSeconds { get; set; }

        static Profiler()
        {
            Timer = new Timer {Interval = 1};
            Timer.Elapsed += Timer_Elapsed;
        }

        static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MiliSeconds+=1;
        }

        public static void Start(string name)
        {
            Name = name;
            Timer.Start();
        }

        public static void Stop()
        {
            Debug.Print("{0}: {1}ms", Name, MiliSeconds);
            Timer.Stop();
            MiliSeconds = 0;
        }
    }
}