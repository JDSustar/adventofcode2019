using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day6
    {
        public static void ExecuteStarOne(string fileLocation = "Day6/Day6.txt")
        {
            string[] lines = File.ReadAllLines(fileLocation);

            var objectMap = new UniversalOrbitMap(lines);
            
            Logger.LogMessage(LogLevel.ANSWER, "6A: Total Orbits: " + objectMap.GetTotalOrbits());
        }
    }

    public class UniversalOrbitMap
    {
        public Dictionary<string, OrbitObject> ObjectsInSpace = new Dictionary<string, OrbitObject>();

        public int GetTotalOrbits()
        {
            int totalOrbits = 0;

            foreach (var orbitObject in this.ObjectsInSpace.Values)
            {
                totalOrbits += orbitObject.GetOrbits();
            }

            return totalOrbits;
        }

        public int OrbitTransfersRequired(string start, string end)
        {
            HashSet<OrbitObject> visited = new HashSet<OrbitObject>();

            OrbitObject startOrbitObject = ObjectsInSpace[start].ParentOrbitObject;
            OrbitObject endOrbitObject = ObjectsInSpace[end].ParentOrbitObject;

            visited.Add(startOrbitObject);
            Queue<OrbitObject> objectsToVisit = new Queue<OrbitObject>();
            objectsToVisit.Enqueue(startOrbitObject.ParentOrbitObject);
            startOrbitObject.Moons.ForEach(m => objectsToVisit.Enqueue(m));

            while (objectsToVisit.Count > 0)
            {
                OrbitObject currentOrbitObject = objectsToVisit.Dequeue();

                if (currentOrbitObject == endOrbitObject)
                {
                    return 1;
                }

                visited.Add(currentOrbitObject);

                objectsToVisit.Enqueue(currentOrbitObject.ParentOrbitObject);
                currentOrbitObject.Moons.ForEach(m => objectsToVisit.Enqueue(m));
            }

            return 0;
        }

        public UniversalOrbitMap(string[] mapStrings)
        {
            foreach (var mapString in mapStrings)
            {
                string centerOfMass = mapString.Split(')')[0];
                string orbitObject = mapString.Split(')')[1];

                if (!ObjectsInSpace.ContainsKey(centerOfMass))
                {
                    ObjectsInSpace.Add(centerOfMass, new OrbitObject(centerOfMass, null));
                }

                if (ObjectsInSpace.ContainsKey(orbitObject))
                {
                    ObjectsInSpace[orbitObject].ParentOrbitObject = ObjectsInSpace[centerOfMass];
                }
                else
                {
                    ObjectsInSpace.Add(orbitObject, new OrbitObject(orbitObject, ObjectsInSpace[centerOfMass]));
                }

                ObjectsInSpace[centerOfMass].AddMoon(ObjectsInSpace[orbitObject]);
            }
        }
    }

    public class OrbitObject
    {
        public string Name { get; private set; }

        public List<OrbitObject> Moons { get; private set; }

        public OrbitObject ParentOrbitObject;

        public OrbitObject(string name, OrbitObject parentOrbitObject)
        {
            this.Name = name;
            this.ParentOrbitObject = parentOrbitObject;
            Moons = new List<OrbitObject>();
        }

        public void AddMoon(OrbitObject o)
        {
            Moons.Add(o);
        }

        public int GetOrbits()
        {
            if (ParentOrbitObject == null)
            {
                return 0;
            }
            else
            {
                return 1 + ParentOrbitObject.GetOrbits();
            }
        }
    }
}
