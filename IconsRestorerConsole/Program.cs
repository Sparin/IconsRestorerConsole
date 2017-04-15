using IconsRestorer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IconsRestorer
{
    class Program
    {
        private static readonly DesktopRegistry _registry = new DesktopRegistry();
        private static readonly Desktop _desktop = new Desktop();
        private static readonly Storage _storage = new Storage();

        static void Main(string[] args)
        {
            foreach(string arg in args)
                switch(arg)
                {
                    case "-s":
                    case "--save": SavePositions(); break;
                    case "-r":
                    case "--restore": RestorePositions(); break;
                    default: Console.WriteLine("Unrecognized arguement: {0}", arg); Console.WriteLine("-r --restore\tRestore position of icons\r\n-s --save\tSave position of icons"); break;
                }
        }

        static void SavePositions()
        {
            var registryValues = _registry.GetRegistryValues();
            var iconPositions = _desktop.GetIconsPositions();
            _storage.SaveIconPositions(iconPositions, registryValues);
        }

        static void RestorePositions()
        {
            var registryValues = _storage.GetRegistryValues();
            _registry.SetRegistryValues(registryValues);
            var iconPositions = _storage.GetIconPositions();
            _desktop.SetIconPositions(iconPositions);
            _desktop.Refresh();
        }
    }
}
