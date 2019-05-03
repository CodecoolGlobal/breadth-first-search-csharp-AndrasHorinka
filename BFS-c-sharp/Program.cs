using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;

namespace BFS_c_sharp
{
    class Program
    {
        static RandomDataGenerator generator = new RandomDataGenerator();
        static List<UserNode> users = new List<UserNode>();

        static void Main(string[] args)
        {
            users = generator.Generate();

            foreach (var user in users)
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static void FriendsOfFriendsDistance(UserNode startingFriend, UserNode destinationFriend)
        {
            Dictionary<UserNode, int> FriendsToBeExtended = new Dictionary<UserNode, int>();
            FriendsToBeExtended[startingFriend] = 0;

            AddFriendsAtShortestDistance(startingFriend, ref FriendsToBeExtended, 0);

            Console.WriteLine($"The shortest distance between {startingFriend.FirstName} and {destinationFriend.FirstName} is {FriendsToBeExtended[destinationFriend]}");
        }

        private static void AddFriendsAtShortestDistance(UserNode friend, ref Dictionary<UserNode, int> FriendsToBeExtended, int distance)
        {
            foreach (var f in friend.Friends)
            {
                if (!FriendsToBeExtended.ContainsKey(f))
                {
                    FriendsToBeExtended[friend] = distance + 1;
                }
                else
                {
                    FriendsToBeExtended[friend] = Math.Min(distance + 1, FriendsToBeExtended[friend]);
                }

                AddFriendsAtShortestDistance(f, ref FriendsToBeExtended, distance + 1);
            }
        }


        public static void FriendsAtGivenDistance(UserNode startingFriend, int maxDistance)
        {
            Dictionary<UserNode, int> FriendsToBeExtended = new Dictionary<UserNode, int>();

            AddFriendsAtShortestDistance(startingFriend, ref FriendsToBeExtended, 1, maxDistance);

            Console.WriteLine($"The list of Friends within {maxDistance} distance: ");
            foreach (KeyValuePair<UserNode, int> pair in FriendsToBeExtended)
            {
                Console.WriteLine($"{pair.Key} at distance: {pair.Value}");
            }
        }

        private static void AddFriendsAtShortestDistance(UserNode friend, ref Dictionary<UserNode, int> FriendsToBeExtended, int distance, int maxDistance)
        {
            foreach (var f in friend.Friends)
            {
                if (!FriendsToBeExtended.ContainsKey(f))
                {
                    FriendsToBeExtended[friend] = distance + 1;
                }
                else
                {
                    FriendsToBeExtended[friend] = Math.Min(distance + 1, FriendsToBeExtended[friend]);
                }

                if (maxDistance > distance + 1)
                {
                    AddFriendsAtShortestDistance(f, ref FriendsToBeExtended, distance + 1);
                }
            }
        }
    }

}