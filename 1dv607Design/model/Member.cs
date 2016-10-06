using System;
using System.Collections.Generic;
using System.Linq;

namespace _1dv607Design.model
{
    public class Member
    {
        //Make array of boats
        private long _personalNumber;
        private string _name;

        /// <summary>
        /// Constructor for member
        /// </summary>
        /// <param name="name"></param>
        /// <param name="personalNumber"></param>
        /// <param name="id"></param>
        public Member(string name, long personalNumber, int id)
        {
            Name = name;
            PersonalNumber = personalNumber;
            Id = id;
            BoatsOwned = new List<Boat>();
        }

        /// <summary>
        /// Auto property for Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                _name = value;
            }
        }

        /// <summary>
        /// Property for name
        /// </summary>
        public long PersonalNumber
        {
            get { return _personalNumber; }

            set
            {
                if (value.ToString().Length != 10)
                {
                    throw new ArgumentException("Personal Number needs 10 digits");
                }
                _personalNumber = value;
            }
        }

        /// <summary>
        /// Auto property for Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Make array of boats
        /// </summary>
        public List<Boat> BoatsOwned { get; set; }

        /// <summary>
        /// Update the member
        /// </summary>
        /// <param name="name"></param>
        /// <param name="personalNumber"></param>
        public void Update(string name, long personalNumber)
        {
            Name = name;
            PersonalNumber = personalNumber;
        }

        /// <summary>
        /// Register boat to user
        /// </summary>
        /// <param name="boat"></param>
        public void RegisterBoat(Boat boat)
        {
            BoatsOwned.Add(boat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void DeleteBoat(int index)
        {
            BoatsOwned.RemoveAt(index);
        }

        /// <summary>
        /// Update a boat's information
        /// </summary>
        /// <param name="index">index of the boat in the List</param>
        /// <param name="boatType">BoatType - Sailboat, Motorsailer, Kayak, Other</param>
        /// <param name="length">Double - length in meters</param>
        public void UpdateBoat(int index, BoatType boatType, double length)
        {
            var boat = BoatsOwned.ElementAt(index);
            boat.Update(boatType, length);
        }

        /// <summary>
        /// Writes string of member information based on list type
        /// </summary>
        /// <param name="listType">ListType - Verbose or Compact</param>
        /// <returns>string representing the member</returns>
        public string ToString(ListType listType)
        {
            if (listType == ListType.Compact)
            {
                return $"{Name, 20} {Id, 15} {BoatsOwned.Count, 14}";
            }
            var boats = string.Join("\n\t", BoatsToString());
            return $"Name: {Name}\n" +
                   $"Personal Number: {PersonalNumber}\n" +
                   $"ID: {Id}\n" +
                   $"Boats: \t{boats}";

        }

        /// <summary>
        /// Writes string representation of the boat(s) owned by the member
        /// </summary>
        /// <returns></returns>
        public List<string> BoatsToString()
        {
            return BoatsOwned.Select(boat => boat.ToString()).ToList();
        }
    }
}