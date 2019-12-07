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

        public static void ExecuteStarTwo(string fileLocation = "Day6/Day6.txt")
        {
            string[] lines = File.ReadAllLines(fileLocation);

            var objectMap = new UniversalOrbitMap(lines);

            Logger.LogMessage(LogLevel.ANSWER, "6A: Shortest Path: " + objectMap.OrbitTransfersRequired("YOU", "SAN"));
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

            Queue<Tuple<OrbitObject, int>> objectsToVisit = new Queue<Tuple<OrbitObject, int>>();
            objectsToVisit.Enqueue(new Tuple<OrbitObject, int>(startOrbitObject.ParentOrbitObject, 1));
            startOrbitObject.Moons.ForEach(m => objectsToVisit.Enqueue(new Tuple<OrbitObject, int>(m.ParentOrbitObject, 1)));

            while (objectsToVisit.Count > 0)
            {
                var currentOrbitObjectAndPathLength = objectsToVisit.Dequeue();
                var currentOrbitObject = currentOrbitObjectAndPathLength.Item1;
                var pathLength = currentOrbitObjectAndPathLength.Item2;

                if (currentOrbitObject == endOrbitObject)
                {
                    return pathLength;
                }

                visited.Add(currentOrbitObject);

                if (!visited.Contains(currentOrbitObject.ParentOrbitObject) && currentOrbitObject.ParentOrbitObject != null)
                {
                    objectsToVisit.Enqueue(new Tuple<OrbitObject, int>(currentOrbitObject.ParentOrbitObject, pathLength + 1));
                }


                currentOrbitObject.Moons.ForEach(m =>
                {
                    if (!visited.Contains(m))
                    {
                        objectsToVisit.Enqueue(new Tuple<OrbitObject, int>(m, pathLength + 1));
                    }
                });
            }

            throw new Exception("Path from " + start + " to " + end + " not found.");
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
