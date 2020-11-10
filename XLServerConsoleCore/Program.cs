using System;
using System.Threading.Tasks;
using XLMultiplayerServer;

namespace XLServerConsoleCore
{
    class Program
    {
        static int Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;

			Server multiplayerServer = new Server(null, null);

			var serverTask = Task.Run(() => multiplayerServer.ServerLoop());

			Task.Run(() => multiplayerServer.CommandLoop());

			serverTask.Wait();
			return 0;
		}
    }
}
