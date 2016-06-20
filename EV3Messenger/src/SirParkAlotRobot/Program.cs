using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using EV3MessengerLib;

namespace SirParkAlotRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Waiting for file.");
            FileSystemWatcher fileWatcher = new FileSystemWatcher("C:\\Temp", "*");

            fileWatcher.Created += new FileSystemEventHandler(MoveToParkingLot);
            fileWatcher.Changed += new FileSystemEventHandler(MoveToParkingLot);
            fileWatcher.EnableRaisingEvents = true;

            System.Console.WriteLine("Press enter to close.");
            System.Console.ReadLine();
        }

        static void MoveToParkingLot(object source, FileSystemEventArgs e)
        {
            System.Console.WriteLine("File found.");

            String fileName = "C:\\Temp\\parkinglot.txt";
            String port = "COM11";
            String fileContent;

            fileContent = File.ReadAllText(fileName);

            float parkingLot = float.Parse(fileContent);
            System.Console.WriteLine("Moving to parking lot " + parkingLot);

            EV3Messenger messenger = new EV3Messenger();

            if (!messenger.Connect(port))
            {
                System.Console.WriteLine("Failed to connect to port " + port);
                System.Console.WriteLine("Press enter to close.");
                System.Console.ReadLine();
                return;
            }

            System.Console.WriteLine("Successfully connected to EV3 at port " + port);

            messenger.SendMessage("abc", parkingLot);
            messenger.Disconnect();
        }
    }
}
