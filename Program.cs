using System;

namespace PingPong
{
    public delegate void PingPong(string message);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game Ping Pong.Let's start\n");
            Ping ping = new();
            Pong pong = new();
            ping.Notify += Message;
            pong.Notify += Message;
            Console.WriteLine("Please enter a name that launches the game:");
            ping.Player1 = ValidationString();
            Console.WriteLine("Please enter the name of the second player:");
            pong.Player2 = ValidationString();
            Console.WriteLine("How many serves will be?");
            byte NumOfServe = Validation();
            Console.WriteLine("How many hits will be?");
            byte NumOfHits = Validation();
            int countScore = 0;
            for (int j = 0; j < NumOfServe; j++)
            {
                Console.WriteLine($"\nServe {j + 1}\n");
                Console.WriteLine();
                for (int i = 0; i < NumOfHits; i++)
                {
                    if (i % 2 == 0)
                    {
                        ping.GetPing();
                        pong.Received();
                    }
                    else if (i % 2 != 0)
                    {
                        pong.GetPong();
                        ping.Received();
                    }
                }
                countScore++;
            }
            if(NumOfHits%2==0)
            {
                Console.WriteLine($"{ping.Player1} can't sent");
                Console.WriteLine($"\n{pong.Player2} won\n");
            }
            else
            {
                Console.WriteLine($"{pong.Player2} can't sent");
                Console.WriteLine($"\n{ping.Player1} won\n");
            }
            Console.WriteLine($"Score {countScore}:0\nGame over Bye.");
            Console.ReadKey();
        }
        private static void Message(string message)
        {
            Console.WriteLine(message);
        }
        static byte Validation()
        {
            while (true)
            {
                if (byte.TryParse(Console.ReadLine(), out byte value) && value > 0 && value < 21)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("\nMax number of hits and serves is 21, min 1\n");
                }
            }
        }
        static string ValidationString()
        {
            string invalid = "1234567890,.;<>'/-=+?!@`\t#%$^*~(\\)%{}[]:0 ";
            int count;
            string Info;
            do
            {
                Info = Console.ReadLine();
                count = EmptyString(Info);
                foreach (int i in Info)
                {
                    foreach (int j in invalid)
                    {
                        if (j == i)
                        {
                            count++;
                        }
                    }
                }
                if (count > 0)
                {
                    Console.WriteLine("Please enter valid name");
                }
            } while (count != 0);
            Info = Info.ToUpper()[0] + Info.Substring(1).ToLower();
            return Info;
        }
         static int EmptyString(string name)
        {
            int count = 0;
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Empty string.");
                count++;
            }
            return count;
        }
    }
    class Ping
    {
        public event PingPong Notify;
        public string Player1 { get; set; }
        public void GetPing()
        {
            Notify?.Invoke($"{Player1} sent Ping");
        }
        public void Received()
        {
            Notify?.Invoke($"{Player1} received Pong");
        }
    }
    class Pong
    {
        public event PingPong Notify;
        public string Player2 { get; set; }
        public void GetPong()
        {
            Notify?.Invoke($"{Player2} sent Pong");
        }
        public void Received()
        {
            Notify?.Invoke($"{Player2} received Ping");
        }
    }
}
