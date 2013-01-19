using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;
using TiledLib;
using System;

namespace SpaceGameContentPipeline
{
    [ContentSerializerRuntimeType("SpaceGame.Room, SpaceGame")]
    public class Room
    {
        public Map map;
        public List<WorldObject> objects;
    }

    [ContentSerializerRuntimeType("SpaceGame.Map, SpaceGame")]
    public class Map
    {
        public int width;
        public int height;
        public Tile[] Tiles;
        public int tileSize = 32;
       
    }

    [ContentSerializerRuntimeType("SpaceGame.Tile, SpaceGame")]
    public class Tile
    {
        public int walkCost;
        //public ExternalReference<Texture2DContent> tex;
        public ExternalReference<Texture2DContent> tex;
        public Rectangle sourceRect;
    }

    [ContentSerializerRuntimeType("SpaceGame.WorldObject, SpaceGame")]
    public class WorldObject
    {

        //public Vector2 position;
        //direction faces;
        //AnimatedTexture2D texture;
    }

    /*
    [ContentSerializerRuntimeType("SpaceGame.AnimatedTexture2D, SpaceGame")]
    public class AnimatedTexture2D
    {
        Texture2D spriteSheet;
        int width;
    }
     */

    /*
    // Each tile has a texture, source rect, and sprite effects.
    [ContentSerializerRuntimeType("HauntedHouse.Tile, HauntedHouse")]
    public class HauntedHouseMapTileContent
    {
        public String TileSource;
        //public ExternalReference<Texture2DContent> Texture;
        public Rectangle SourceRectangle;
        public SpriteEffects SpriteEffects;
        public bool IsShadowCaster;
        public bool Exists;
        public Vector2 Position;
        public Vector2 GridSize;
    }

    [ContentSerializerRuntimeType("HauntedHouse.Entity, HauntedHouse")]
    public class HauntedHouseMapObjectContent
    {
        public String EntityType;
        public String EntityName;
        public Rectangle EntityBounds;
        public Dictionary<String, String> Properties;
    }

    // For each layer, we store the size of the layer and the tiles.
    [ContentSerializerRuntimeType("HauntedHouse.TileLayer, HauntedHouse")]
    public class HauntedHouseTileLayerContent
    {
        public String LayerName;
        public int Width;
        public int Height;
        public HauntedHouseMapTileContent[] Tiles;
    }

    [ContentSerializerRuntimeType("HauntedHouse.EntityLayer, HauntedHouse")]
    public class HauntedHouseObjectLayerContent
    {
        public String LayerName;
        public int Width;
        public int Height;
        public HauntedHouseMapObjectContent[] Objects;
    }

    // For the map itself, we just store the size, tile size, and a list of layers.
    [ContentSerializerRuntimeType("HauntedHouse.Level, HauntedHouse")]
    public class HauntedHouseMapContent
    {
        public int TileWidth;
        public int TileHeight;
        public List<HauntedHouseObjectLayerContent> ObjectLayers = new List<HauntedHouseObjectLayerContent>();
        public List<HauntedHouseTileLayerContent> TileLayers = new List<HauntedHouseTileLayerContent>();
    }
    */

    [ContentProcessor(DisplayName = "TMX Processor - SpaceGame")]
    public class MapProcessor : ContentProcessor<MapContent, Room>
    {
        public override Room Process(MapContent input, ContentProcessorContext context)
        {

           // System.Diagnostics.Debugger.Launch();


            TiledHelpers.BuildTileSetTextures(input, context);
            TiledHelpers.GenerateTileSourceRectangles(input);

           
            Map map = new Map();

            foreach (LayerContent layer in input.Layers)
            {
                TileLayerContent tileLayerContent = layer as TileLayerContent;
                //MapObjectLayerContent mapObjectLayerContent = layer as MapObjectLayerContent;


                if (tileLayerContent != null)
                {

                    Tile[] tiles = new Tile[tileLayerContent.Data.Length];

                    for (int i = 0; i < tileLayerContent.Data.Length; i++)
                    {

                        map.width = tileLayerContent.Width;
                        map.height = tileLayerContent.Height;

                        // get the ID of the tile
                        uint tileID = tileLayerContent.Data[i];

                        // use that to get the actual index as well as the SpriteEffects
                        int tileIndex;
                        SpriteEffects spriteEffects;
                        TiledHelpers.DecodeTileID(tileID, out tileIndex, out spriteEffects);

                        // figure out which tile set has this tile index in it and grab
                        // the texture reference and source rectangle.

                        ExternalReference<Texture2DContent> textureContent = null;

                        Rectangle sourceRect = new Rectangle();
                        String imageSource = "NotDefined";
                        Boolean exists = true;
                        int walkCost = 1;

                        // iterate all the tile sets
                        foreach (var tileSet in input.TileSets)
                        {
                            if (tileIndex != 0)
                            {
                                // if our tile index is in this set
                                if (tileIndex - tileSet.FirstId < tileSet.Tiles.Count)
                                {
                                    imageSource = tileSet.Image;
                                    // store the texture content and source rectangle
                                    textureContent = tileSet.Texture;

                                    sourceRect = tileSet.Tiles[(int)(tileIndex - tileSet.FirstId)].Source;

                                    if (tileSet.Tiles[(int)(tileIndex - tileSet.FirstId)].Properties.ContainsKey("walkCost"))
                                    {
                                        walkCost = Convert.ToInt16(tileSet.Tiles[(int)(tileIndex - tileSet.FirstId)].Properties["walkCost"]);
                                    }
                                    // and break out of the foreach loop
                                    break;
                                }
                            }
                            else
                            {
                                exists = false;
                            }
                        }

                        //now insert the tile into our output
                        tiles[i] = new Tile
                        {
                            walkCost = walkCost,
                            tex = textureContent,
                            sourceRect = sourceRect
                            //GridSize = new Vector2(output.TileWidth, output.TileHeight),
                            //Position = new Vector2((float)i % tileLayerContent.Width * output.TileWidth, (float)((Math.Floor((double)i / tileLayerContent.Width) * output.TileHeight)))
                        };
                    }

                    map.Tiles = tiles;
                    map.tileSize = 64; //TODO get it

                }
            }

            Room output = new Room { 
            map = map,
            objects = new List<WorldObject>()
            };

            return output;
        }
    }

}

/*
// build the textures
//TiledHelpers.BuildTileSetTextures(input, context);

//Temporary variables:
//int mapWidth;
//int mapHeight;

// generate source rectangles

Room output = new Room();
Map map = new Map();

if (input != null)
{

    TiledHelpers.GenerateTileSourceRectangles(input);

                

    // iterate all the layers of the input
    foreach (LayerContent layer in input.Layers)
    {
        // we only care about tile layers in our demo

        TileLayerContent tileLayerContent = layer as TileLayerContent;
        //MapObjectLayerContent mapObjectLayerContent = layer as MapObjectLayerContent;

        //map = new Map
        //{
        //   width = input.Width,
        //   height = input.Height
        //};

        if (tileLayerContent != null)
        {

            // we need to build up our tile list now
            //Map.tiles = new HauntedHouseMapTileContent[tileLayerContent.Data.Length];


            /*

            for (int i = 0; i < tileLayerContent.Data.Length; i++)
            {
                // get the ID of the tile
                uint tileID = tileLayerContent.Data[i];

                // use that to get the actual index as well as the SpriteEffects
                int tileIndex;

                SpriteEffects spriteEffects;

                TiledHelpers.DecodeTileID(tileID, out tileIndex, out spriteEffects);

                // figure out which tile set has this tile index in it and grab
                // the texture reference and source rectangle.
                //ExternalReference<Texture2DContent> textureContent = null;
                Rectangle sourceRect = new Rectangle();
                String imageSource = "NotDefined";
                //Boolean exists = true;
                int walkCost = 1;

                // iterate all the tile sets
                foreach (var tileSet in input.TileSets)
                {
                    if (tileIndex != 0)
                    {
                        // if our tile index is in this set
                        if (tileIndex - tileSet.FirstId < tileSet.Tiles.Count)
                        {
                            imageSource = tileSet.Image;
                            // store the texture content and source rectangle
                            //textureContent = tileSet.Texture;
                            sourceRect = tileSet.Tiles[(int)(tileIndex - tileSet.FirstId)].Source;

                            if (tileSet.Tiles[(int)(tileIndex - tileSet.FirstId)].Properties.ContainsKey("walkCost"))
                            {
                                walkCost = Convert.ToInt16(tileSet.Tiles[(int)(tileIndex - tileSet.FirstId)].Properties["walkCost"]);
                            }
                            // and break out of the foreach loop
                            break;
                        }
                    }
                    // else
                    // {
                    //     exists = false;
                    // }
                }

                // now insert the tile into our output
                // Tile tile = new Tile
                //{
                //    walkCost = walkCost,
                //    texture = textureContent
                // GridSize = new Vector2(output.TileWidth, output.TileHeight),
                //Position = new Vector2((float)i % tileLayerContent.Width * output.TileWidth, (float)((Math.Floor((double)i / tileLayerContent.Width) * output.TileHeight)))
                //};
            }

            // return the output object. because we have ContentSerializerRuntimeType attributes on our
            // objects, we don't need a ContentTypeWriter and can just use the automatic serialization
            // now build our output, first by just copying over some data
            //  output = new Room
            //  {
            //     map = map,
            //     objects = null
            //};

            /*
            if (mapObjectLayerContent != null)
            {
                    

                //Variables
                Dictionary<String, String> properties = null;
                Rectangle bounds = new Rectangle();
                String name = null;
                String type = null;

                // we need to build up our tile list now
                //outLayer.Objects = new HauntedHouseMapObjectContent[mapObjectLayerContent.Objects.Count];
                MapObjectContent[] mapLayerObjects = mapObjectLayerContent.Objects.ToArray();

                for (int i = 0; i < mapLayerObjects.Length; i++)
                {
                    if (mapLayerObjects[i].Properties != null)
                    {
                        properties = new Dictionary<string, string>(mapLayerObjects[i].Properties);
                    }
                    bounds = mapLayerObjects[i].Bounds;
                    name = mapLayerObjects[i].Name;
                    type = mapLayerObjects[i].Type;

                    // now insert the tile into our output
                    output.objects[i] = new WorldObject(); //TODO
                   /
                    {
                        Properties = properties,
                        EntityName = name,
                        EntityType = type,
                        EntityBounds = bounds
                    };
                        
                       
                }

                //output.ObjectLayers.Add(outLayer);
                     
            }
                       

        }

        return output;
    }
}
}
}

*/




