using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum_Gate.Game_Objects
{
    class RoomBuilder //TODO: This is set to no build action, delete once Quantum Gate 1 initializer is working
    {
        private List<String> imageList = new List<string>();
        private List<String> movieList = new List<string>();
        private List<RoomExit> exitList = new List<RoomExit>();

        public Room buildRoom(String roomName)
        {
            if(roomName == "drewQtrs") //Drew's quarters
            {
                imageList.Add("n3c"); //north (door)
                imageList.Add("n3b"); //east (militerm)
                imageList.Add("n3a"); //south (desk)
                imageList.Add("n3d"); //west (bed)

                
                movieList.Add("N3C_3B"); //north to east
                movieList.Add("N3B_3A"); //east to south
                movieList.Add("N3A_3D"); //south to west
                movieList.Add("N3D_3C"); //west to north

                movieList.Add("N3C_3D"); //north to east
                movieList.Add("N3D_3A"); //west to south
                movieList.Add("N3A_3B"); //south to west
                movieList.Add("N3B_3C"); //west to north

                var basePath = (@".\content\drewqtrs\");

                var northExit = new RoomExit("north", "enlistedQtrs", "N3C_2C", basePath, "north", false);
                exitList.Add(northExit);

                var drewQtrs = new Room(imageList, movieList, exitList, basePath, "Drew's Quarters");

                return drewQtrs;
            }

            if (roomName == "enlistedQtrs") //Enlisted Men's quarters
            {
                imageList.Add("n2c"); //north
                imageList.Add("n2b"); //east
                imageList.Add("n2a"); //south
                imageList.Add("n2d"); //west

                movieList.Add("N2C_2B"); //north to east
                movieList.Add("N2B_2A"); //east to south
                movieList.Add("N2A_2D"); //south to west
                movieList.Add("N2D_2C"); //west to north

                movieList.Add("N2C_2D"); //north to west
                movieList.Add("N2D_2A"); //west to south
                movieList.Add("N2A_2B"); //south to east
                movieList.Add("N2B_2C"); //east to north

                var basePath = (@".\content\drewqtrs\");

                var exit1 = new RoomExit("east", "drewQtrs", "N2B_3A", basePath, "south", false);
                var exit2 = new RoomExit("north", "deck2area1", "N2C_1", basePath, "north", false);
                exitList.Add(exit1);
                exitList.Add(exit2);

                var enlistedQtrs = new Room(imageList, movieList, exitList, basePath, "Corporate Quarters");

                return enlistedQtrs;
            }

            if (roomName.Substring(0,9) ==  "deck2area") //Deck 2 corridor
            {
                imageList.Add("NSI1"); //north
                imageList.Add("NHW1"); //east
                imageList.Add("NSO1"); //south
                imageList.Add("NHE1"); //west

                movieList.Add("NSIHW"); //north to east
                movieList.Add("NHWSO"); //east to south
                movieList.Add("NSOHE"); //south to west
                movieList.Add("NHESI"); //west to north

                movieList.Add("NSIHE"); //north to west
                movieList.Add("NHESO"); //west to south
                movieList.Add("NSOHW"); //south to east
                movieList.Add("NHWSI"); //east to north

                var basePath = (@".\content\main\");
                int areaNumber = Convert.ToInt16(roomName.Substring(9, 1));

                if (areaNumber == 1)
                {
                    var exit1 = new RoomExit("south", "enlistedQtrs", "corpqrtrs", basePath, "south", false);
                    var exit2 = new RoomExit("east", "deck2area5", "NHW", basePath, "east", false );
                    var exit3 = new RoomExit("west", "deck2area2", "NHE", basePath, "west", false);
                    var exit4 = new RoomExit("north", "lift1deck2", "deck2lift1", basePath, "north", true);
                    exitList.Add(exit1);
                    exitList.Add(exit2);
                    exitList.Add(exit3);
                    exitList.Add(exit4);
                    var corridor = new Room(imageList, movieList, exitList, basePath, "Corridor - Corporate Quarters");
                    return corridor;
                }
                else if(areaNumber == 2)
                {
                    //RoomExit exit1 = new RoomExit("south", "enlistedQtrs", "corpqrtrs", basePath, "south");
                    var exit2 = new RoomExit("west", "deck2area3", "NHE", basePath, "west", false);
                    var exit3 = new RoomExit("east", "deck2area1", "NHW", basePath, "east", false);
                    //exitList.Add(exit1);
                    exitList.Add(exit2);
                    exitList.Add(exit3);
                    var corridor = new Room(imageList, movieList, exitList, basePath, "Corridor - Sick Bay");
                    return corridor;
                }
                else if (areaNumber == 3)
                {
                    //RoomExit exit1 = new RoomExit("south", "enlistedQtrs", "corpqrtrs", basePath, "south");
                    var exit2 = new RoomExit("west", "deck2area4", "NHE", basePath, "west", false);
                    var exit3 = new RoomExit("east", "deck2area2", "NHW", basePath, "east", false);
                    //exitList.Add(exit1);
                    exitList.Add(exit2);
                    exitList.Add(exit3);
                    var corridor = new Room(imageList, movieList, exitList, basePath, "Corridor - Lounge");
                    return corridor;
                }
                else if (areaNumber == 4)
                {
                   // RoomExit exit1 = new RoomExit("south", "enlistedQtrs", "corpqrtrs", basePath, "south");
                    var exit2 = new RoomExit("west", "deck2area5", "NHE", basePath, "west", false);
                    var exit3 = new RoomExit("east", "deck2area3", "NHW", basePath, "east", false);
                    //exitList.Add(exit1);
                    exitList.Add(exit2);
                    exitList.Add(exit3);
                    var corridor = new Room(imageList, movieList, exitList, basePath, "Corridor - Kitchen/Mess");
                    return corridor;
                }
                else if (areaNumber == 5)
                {
                    //RoomExit exit1 = new RoomExit("south", "enlistedQtrs", "corpqrtrs", basePath, "south");
                    var exit2 = new RoomExit("west", "deck2area1", "NHE", basePath, "west", false);
                    var exit3 = new RoomExit("east", "deck2area4", "NHW", basePath, "east", false);
                    //exitList.Add(exit1);
                    exitList.Add(exit3);
                    exitList.Add(exit2);
                    var corridor = new Room(imageList, movieList, exitList, basePath, "Corridor - Science");
                    return corridor;
                }
            }

            return null;
        }
    }
}
