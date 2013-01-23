using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame
{
    public static class FileNames
    {
        //folder structure!
        //L1
        public const String AmbientFolder = "Ambient/";
        public const String LevelsFolder = "Levels/";
        public const String PickupsFolder = "Pickups/";
        public const String SoundsFolder = "Sounds/";
        public const String TileSetsFolder = "TileSets/";
        public const String TrapsFolder = "Traps/";
        public const String TrapSpawnsFolder = "TrapSpawns/";
        public const String UIFolder = "UI/";
        //L2
        public const String GasesFolder = TrapSpawnsFolder + "Gases/";
        public const String ExplosionParticlesFolder = TrapSpawnsFolder + "ExplosionParticles/";

        //file names
        public const String deathSplash = UIFolder + "deathsplash";
        public const String pauseSplash = UIFolder + "pausesplash";

        public const String laserBeam = TrapSpawnsFolder + "laserbeam";
        public const String laserSection = TrapSpawnsFolder + "lasersection";
        public const String projectile1 = TrapSpawnsFolder + "projectile1";
        public const String gas1 = GasesFolder + "Gas1";
        public const String gas2 = GasesFolder + "Gas2";
        public const String gas3 = GasesFolder + "Gas3";
        public const String shrapnal1 = ExplosionParticlesFolder + "ExplosionParticle1";
        public const String shrapnal2 = ExplosionParticlesFolder + "ExplosionParticle2";
        public const String shrapnal3 = ExplosionParticlesFolder + "ExplosionParticle3";

        public const String turret1 = TrapsFolder + "turret1";
        public const String turret2 = TrapsFolder + "Turret2";
        public const String proximityMine = TrapsFolder + "proximitymine";
        public const String laserDish = TrapsFolder + "laserdish";
        public const String gasVent = TrapsFolder + "gasvent";
        public const String controls = TrapsFolder + "controls";

        public const String tileSet = TileSetsFolder + "tileset";

        public const String phase1 = SoundsFolder + "phase1";
        public const String phase2 = SoundsFolder + "phase2";
        public const String phase3 = SoundsFolder + "phase3";
        public const String blackHoleDeath = SoundsFolder + "blackholedeath";

        public const String jewelBlue = PickupsFolder + "bluejewel";
        public const String jewelGreen = PickupsFolder + "greenjewel";
        public const String jewelOrange = PickupsFolder + "orangejewel";
        public const String jewelPurple = PickupsFolder + "purplejewel";
        public const String jewelRed = PickupsFolder + "redjewel";


    }
}
