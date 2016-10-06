using System;
using System.Collections.Generic;

namespace _1dv607Design.view
{
    public class RegistryView
    {

        /// <summary>
        /// Display welcome message
        /// </summary>
        public void RenderWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine(
                "Welcome to the boat club member registry. " +
                "\nSelect from the menu below by inputting corresponding number." +
                "\n1 List Members" +
                "\n2 Add New Member" +
                "\n3 Exit Program"
              );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberList"></param>
        public void RenderMembersCompact(List<string> memberList)
        {
            Console.Clear();
            Console.WriteLine($"{"|Name|", 15} {"|ID|", 21} {"|Number of Boats|", 24}");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var member in memberList)
            {
                Console.WriteLine($"{member}\n");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("Input member id to view info or press enter to return to menu.");
        }

        public void RenderMembersVerbose(List<string> memberList)
        {
            Console.Clear();
            foreach (var member in memberList)
            {
                Console.WriteLine($"{member}\n");
                Console.WriteLine("*************************\n");
            }
            
            Console.WriteLine("Input member id to view info or press enter to return to menu.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        public void RenderMemberInfo(string member)
        {
            Console.Clear();
            Console.WriteLine($"{"|Name|",15} {"|Personal Number|",25} {"|ID|",10} {"|Boats|",10}");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(member);
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(
                "1 Register Boat to Member" +
                "\n2 Edit Boat" +
                "\n3 Delete Boat" +
                "\n4 Edit Member Info" +
                "\n5 Delete Member" +
                "\n6 Return to List" +
                "\n7 Return to Main Menu"
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boat"></param>
        public void RenderBoatInfo(string boat)
        {
            Console.WriteLine(boat);
        }

        /// <summary>
        /// Render view to Create new member
        /// </summary>
        public void RenderCreateMember()
        {
            Console.Clear();
            Console.WriteLine(
                "Create New Member" +
                "\nInput member name and personal number below"
                );
        }

        public void RenderWrongInput()
        {
            Console.WriteLine("Wrong input, try again...");
        }

        public void RenderRegisterBoat()
        {
            Console.Clear();
            Console.WriteLine(
                "Select boat type and length (in meters)" +
                "\nStart with the type" +
                "\n1 Sailboat" +
                "\n2 Motorsailer" +
                "\n3 Kayak/Canoe" +
                "\n4 Other"
                );
        }

        public void RenderBoats(List<string> boats)
        {
            
            var i = 1;
            foreach (var boat in boats)
            {
                Console.WriteLine($"{i} {boat}");
                i += 1;
            }
        }

        public void RenderEditMember()
        {
            Console.WriteLine("Input new information. " +
                              "If you don't want to change a field just leave it blank and hit enter."
                              );
        }
    }
}