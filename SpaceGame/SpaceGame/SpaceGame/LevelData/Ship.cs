using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SpaceGame
{

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

        private ContentManager content;

        private PlayerObject player;

        public Boolean defeat;

        public Boolean foundBridge = false;

        public Boolean alarm;

        public Random rand;
        //protected Door mainEntry;

        public Ship(List<Room> rooms)
        {
            this.rooms = rooms;

            leftRooms = new List<Room>();
            rightRooms = new List<Room>();
            upRooms = new List<Room>();
            downRooms = new List<Room>();

            rand = new Random();

            doorLinks = new Dictionary<DoorObject, DoorObject>();
        }

        public void Init(ContentManager content)
        {
            rand = new Random();

            this.content = content;

            currentRoom = rooms[0];

            player = new PlayerObject();
            player.destroyMe += new destroyMeEventHandler(player_destroyMe);

            player.position = new Vector2(300, 400);

            player.position = new Vector2(400, 400);


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

        void player_destroyMe(WorldObject sender, Boolean dyrmi)
        {
            if (dyrmi)
            {
                Console.WriteLine("DEAAAAAAAAAAAAAAAAAD");
                this.defeat = true;
            }
        }

        public void Update(GameTime gameTime)
        {

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

                                if (toRooms.Count != 0)
                                {
                                    while (true)
                                    {

                                        List<Room> possibleRooms = toRooms;

                                        int index = (int)rand.Next(toRooms.Count);
                                        Room possibleRoom = possibleRooms[index];

                                        if (possibleRoom != previousRoom)
                                        {
                                            currentRoom = createRoom(possibleRoom);
                                            toRooms.Remove(currentRoom);
                                            fromRooms.Remove(previousRoom);
                                            break;
                                        }
                                        else
                                        {
                                            possibleRooms.Remove(possibleRoom);
                                        }

                                        if (possibleRooms.Count == 0)
                                        {
                                            if (toRooms.Count + fromRooms.Count >= 4)
                                            {
                                                currentRoom = createRoom(loadDeadend(directionTo));
                                                fromRooms.Remove(previousRoom);
                                                break;
                                            }
                                            else if (!foundBridge)
                                            {
                                                currentRoom = createRoom(loadBridge(directionTo));
                                                currentRoom.redButtonEvent += new RedButtonEventDelegate(currentRoom_redButtonEvent);
                                                fromRooms.Remove(previousRoom);
                                                foundBridge = true;
                                                break;
                                            }
                                        }
                                    }

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

                }
                currentRoom.addWO(player);
            }
            currentRoom.Update(gameTime);
        }

        public Room createRoom(Room room)
        {
            room.lootEvent += new lootEvenDelegate(room_lootEvent);
            return room;
        }

        void room_lootEvent(int amount)
        {
               throw new NotImplementedException();
        }

        void currentRoom_redButtonEvent()
        {
            alarm = true;
        }

        public Room loadDeadend(String direction)
        {
            Room room = null;
            if (direction == "left")
            {
                room = content.Load<Room>("Levels/L1");
            }
            if (direction == "right")
            {
                room = content.Load<Room>("Levels/R1");
            }
            if (direction == "up")
            {
                room = content.Load<Room>("Levels/U1");
            }
            if (direction == "down")
            {
                room = content.Load<Room>("Levels/D1");
            }
            room = createRoom(room);
            room.Init(content);

            return room;
        }

        public Room loadBridge(String direction)
        {
            Room room = null;
            if (direction == "left")
            {
                room = content.Load<Room>("Levels/BridgeL");
            }
            if (direction == "right")
            {
                room = content.Load<Room>("Levels/BridgeR");
            }
            if (direction == "up")
            {
                room = content.Load<Room>("Levels/BridgeU");
            }
            if (direction == "down")
            {
                room = content.Load<Room>("Levels/BridgeD");
            }
            room = createRoom(room);
            room.Init(content);

            return room;
        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            currentRoom.Render(spriteBatch, offset, tint);
        }
    }
}
