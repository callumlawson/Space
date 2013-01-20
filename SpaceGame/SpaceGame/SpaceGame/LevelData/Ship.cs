using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SpaceGame
{
    /*
    public struct Door
    {
        public Room room;
        public WorldObject point;

        public Door(Room room, WorldObject point)
        {
            this.room = room;
            this.point = point;
        }
    }
     */

    public class Ship : IUpdates
    {
        //All the possible rooms
        private List<Room> rooms;

        private List<Room> leftRooms;
        private List<Room> rightRooms;
        private List<Room> upRooms;
        private List<Room> downRooms;

        private Dictionary<DoorObject, DoorObject> doorLinks;

        private int numRooms = 10;

        private Room currentRoom;

        private PlayerObject player;

        Random rand;
        //protected Door mainEntry;

        public Ship(List<Room> rooms)
        {
            this.rooms = rooms;

            leftRooms = new List<Room>();
            rightRooms = new List<Room>();
            upRooms = new List<Room>();
            downRooms = new List<Room>();

            doorLinks = new Dictionary<DoorObject, DoorObject>();
        }

        public void Init(ContentManager content)
        {
            rand = new Random();

            currentRoom = rooms[0];

            player = new PlayerObject();
            player.destroyMe += new destroyMeEventHandler(player_destroyMe);
            player.position = new Vector2(400,400);

            Game1.hacks = player;

            currentRoom.objects.Add(player);


            foreach (Room room in rooms)
            {
                room.Init(content);

                foreach (WorldObject worldObject in room.objects)
                {
                    if (worldObject.type == "Door")
                    {
                        DoorObject door = (DoorObject)worldObject;
                        if (door.props["direction"] == "left")
                        {
                            leftRooms.Add(room);
                        }
                        if (door.props["direction"] == "right")
                        {
                            rightRooms.Add(room);
                        }
                        if (door.props["direction"] == "up")
                        {
                            upRooms.Add(room);
                        }
                        if (door.props["direction"] == "down")
                        {
                            downRooms.Add(room);
                        }
                    }
                }
            }

        }

        void player_destroyMe(WorldObject sender,Boolean dyrmi)
        {
            if (dyrmi)
            {
              Console.WriteLine("DEAAAAAAAAAAAAAAAAAD");
            }
        }

        public void Update(GameTime gameTime)
        {
            Console.WriteLine(player.transition);

            DoorObject previousDoor = null;
            DoorObject newDoor = null;

            Room previousRoom = currentRoom;

            
            String directionFrom = player.transition;
            String directionTo = "";

            List<Room> fromRooms = null;
            List<Room> toRooms = null;

            if (directionFrom != "none")
            {

                if (directionFrom == "right")
                {
                    directionTo = "left";
                    fromRooms = rightRooms;
                    toRooms = leftRooms;
                }
                if (directionFrom == "left")
                {
                    directionTo = "right";
                    fromRooms = leftRooms;
                    toRooms = rightRooms;
                }
                if (directionFrom == "up")
                {
                    directionTo = "down";
                    fromRooms = upRooms;
                    toRooms = downRooms;
                }
                if (directionFrom == "down")
                {
                    directionTo = "up";
                    fromRooms = downRooms;
                    toRooms = upRooms;
                }

                //We need a new link   
                player.onDestroyMe();


                foreach (WorldObject worldObject in previousRoom.objects)
                {
                    if (worldObject.type == "Door")
                    {
                        DoorObject door = (DoorObject)worldObject;
                        if (door.props["direction"] == directionFrom)
                        {
                            previousDoor = door;

                            if (doorLinks.ContainsKey(previousDoor))
                            {
                                newDoor = doorLinks[previousDoor];
                                currentRoom = newDoor.room;
                                player.position = newDoor.position;
                            }
                            else
                            {
                                int random = rand.Next(toRooms.Count);
                                currentRoom = toRooms[random];
                                toRooms.Remove(currentRoom);
                                fromRooms.Remove(previousRoom);

                                foreach (WorldObject anotherWorldObject in currentRoom.objects)
                                {
                                    if (anotherWorldObject.type == "Door")
                                    {
                                        DoorObject anotherDoor = (DoorObject)anotherWorldObject;
                                        if (anotherDoor.props["direction"] == directionTo)
                                        {
                                            newDoor = anotherDoor;
                                            player.position = anotherDoor.position;
                                            doorLinks.Add(previousDoor, newDoor);
                                            doorLinks.Add(newDoor, previousDoor);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    
                }

                currentRoom.addWO(player);
        
            }
           

            currentRoom.Update(gameTime);
        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            currentRoom.Render(spriteBatch, offset, tint);
        }
    }
}
