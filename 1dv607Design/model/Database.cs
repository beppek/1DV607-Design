using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace _1dv607Design.model
{
    public class Database
    {
        private readonly List<Member> _members;

        /// <summary>
        /// Read in text file
        /// </summary>
        public Database()
        {
            string json;
            using (var reader = new StreamReader(@"..\..\Data\data.json"))
            {
                json = reader.ReadToEnd();
            }

            _members = JsonConvert.DeserializeObject<List<Member>>(json);

        }

        /// <summary>
        /// Adds member to in-memory database
        /// </summary>
        /// <param name="member">member of Member object</param>
        public void Add(Member member)
        {
            _members.Add(member);
            Save();
        }

        /// <summary>
        /// Delete member with corresponding id from in-memory db
        /// </summary>
        /// <param name="id">id of member to be deleted</param>
        public void Delete(int id)
        {
            foreach (var member in _members.Reverse<Member>())
            {
                if (member.Id == id)
                {
                    _members.Remove(member);
                }
            }
            Save();
        }

        /// <summary>
        /// Retrieve All members in registry
        /// </summary>
        /// <returns>List of Members</returns>
        public List<Member> RetrieveAll()
        {
            return _members;
        }

        /// <summary>
        /// Retrieve Member with specific id
        /// </summary>
        /// <param name="id">id of member to be returned</param>
        /// <returns>Member</returns>
        public Member Retrieve(int id)
        {
            return _members.FirstOrDefault(member => member.Id == id);
        }

        /// <summary>
        /// Gets unique id by looping through registry to find highest used id and adds 1
        /// </summary>
        /// <returns>highest Id of registry + 1</returns>
        public int GetUniqueId()
        {
            var highestId = 0;
            foreach (var member in _members)
            {
                if (member.Id > highestId)
                {
                    highestId = member.Id;
                }
            }
            return highestId + 1;
        }

        /// <summary>
        /// Save in-memory db to JSON file
        /// </summary>
        public void Save()
        {
            Console.WriteLine("Saving data");
            var json = JsonConvert.SerializeObject(_members, Formatting.Indented);
            File.WriteAllText(@"../../Data/data.json", json);
        }
    }
}