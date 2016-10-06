using System;

namespace _1dv607Design.model
{
    public class Boat
    {
        private double _length;

        /// <summary>
        /// Cosntructor for Boat
        /// </summary>
        /// <param name="boatType">Enum BoatType - Sailboat, Motorsailer, Kayak, Other</param>
        /// <param name="length"></param>
        public Boat(BoatType boatType, double length)
        {
            Type = boatType;
            Length = length;
        }

        /// <summary>
        /// Property for the length
        /// </summary>
        public double Length
        {
            get { return _length; }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _length = value;
            }
        }

        /// <summary>
        /// Property for the type of boat
        /// </summary>
        public BoatType Type
        { get; set; }

        /// <summary>
        /// Method to update the boat
        /// </summary>
        /// <param name="boatType"></param>
        /// <param name="length"></param>
        public void Update(BoatType boatType, double length)
        {
            Type = boatType;
            Length = length;
        }

        /// <summary>
        /// Override ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Type: {Type}, Length: {Length} meters";
        }
    }
}