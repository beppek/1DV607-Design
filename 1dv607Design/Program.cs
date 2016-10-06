
using System;
using _1dv607Design.controller;
using _1dv607Design.model;
using _1dv607Design.view;

namespace _1dv607Design
{
    public class Program
    {
        private static MemberController _memberCtrl;
        private static RegistryView _view;

        /// <summary>
        /// Main method
        /// </summary>
        private static void Main()
        {
            Console.Title = "Member Registry";

            _memberCtrl = new MemberController();
            _view = new RegistryView();

            MainMenu();
            
        }

        /// <summary>
        /// Main Menu of application
        /// </summary>
        private static void MainMenu()
        {
            _view.RenderWelcomeMessage();

            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) || input > 3 || input < 1)
            {
                _view.RenderWrongInput();
                key = Console.ReadLine();
                _view.RenderWelcomeMessage();
            }
            while (input != 3)
            {
                switch (input)
                {
                    case 1:
                        ListMembers();
                        break;
                    case 2:
                        AddMember();
                        break;
                }

                _view.RenderWelcomeMessage();
                key = Console.ReadLine();
                while (!int.TryParse(key, out input) || input > 3 || input < 1)
                {
                    _view.RenderWrongInput();
                    key = Console.ReadLine();
                    _view.RenderWelcomeMessage();
                }

            }

            Environment.Exit(0);
        }
        
        /// <summary>
        /// List the members in the registry and handle interaction with the list
        /// </summary>
        private static void ListMembers()
        {
            Console.Clear();
            Console.WriteLine(
                    "How do you want to display the members? " +
                    "\n(V)erbose or (C)ompact?"
                    );
            var typeInput = Console.ReadLine();
            while (typeInput == null || (typeInput.ToLower() != "v" && typeInput.ToLower() != "c"))
            {
                _view.RenderWrongInput();
                typeInput = Console.ReadLine();
            }
            var listType = typeInput.ToLower() == "v" ? ListType.Verbose : ListType.Compact;
            var members = _memberCtrl.RetrieveAll(listType);
            if (listType == ListType.Compact)
            {
                _view.RenderMembersCompact(members);
            }
            else
            {
                _view.RenderMembersVerbose(members);
            }

            //Select member to view
            var idInput = Console.ReadLine();
            int id;
            while (!string.IsNullOrWhiteSpace(idInput) && (!int.TryParse(idInput, out id)))
            {
                _view.RenderWrongInput();
                idInput = Console.ReadLine();
            }

            if (int.TryParse(idInput, out id))
            {
                try
                {
                    ViewMember(id);
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine(
                        "Something went wrong while retrieving the member." +
                        "\nHit Enter to return to Main Menu..."
                        );
                    Console.ReadLine();
                    MainMenu();
                }
                
            }
            
        }

        /// <summary>
        /// Add member
        /// </summary>
        private static void AddMember()
        {
            _view.RenderCreateMember();
            Console.WriteLine("Full Name (Firstname Lastname):");
            var name = Console.ReadLine();
            Console.WriteLine("Personal Number (10 digits):");
            var numberInput = Console.ReadLine();
            long personalNumber;
            while (!long.TryParse(numberInput, out personalNumber) || personalNumber.ToString().Length != 10)
            {
                Console.WriteLine("No good, personal number must be a number with 10 digits!");
                numberInput = Console.ReadLine();
                Console.Clear();
            }

            try
            {
                _memberCtrl.Create(name, personalNumber);
            }
            catch (Exception)
            {
                Console.WriteLine(
                    "Something went wrong while creating the user! " +
                    "Return to (m)ain menu or try (a)gain?"
                    );
                var input = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(input) || (input.ToLower() != "m" && input.ToLower() != "a"))
                {
                    _view.RenderWrongInput();
                    input = Console.ReadLine();
                }
                if (input.ToLower() == "m")
                {
                    MainMenu();
                }
                else
                {
                    AddMember();
                }
            }
            
            Console.Clear();
            Console.WriteLine("Member successfully created!");
            Console.WriteLine("Hit any key to go back to the menu...");
            Console.ReadLine();
        }

        /// <summary>
        /// Handle interaction with selected member
        /// </summary>
        /// <param name="id">member id of member to be viewed</param>
        private static void ViewMember(int id)
        {
            var member = _memberCtrl.Retrieve(id);
            _view.RenderMemberInfo(member.ToString(ListType.Verbose));

            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) || input > 7 || input < 1)
            {
                _view.RenderWrongInput();
                key = Console.ReadLine();
            }
            switch (input)
            {
                case 1:
                    AddBoat(member);
                    break;
                case 2:
                    EditBoat(member);
                    break;
                case 3:
                    DeleteBoat(member);
                    break;
                case 4:
                    EditMember(member);
                    break;
                case 5:
                    _memberCtrl.Delete(member.Id);
                    break;
                case 6:
                    ListMembers();
                    break;
                case 7:
                    MainMenu();
                    break;
            }
        }

        /// <summary>
        /// Add boat
        /// </summary>
        /// <param name="member"></param>
        private static void AddBoat(Member member)
        {
            _view.RenderRegisterBoat();
            var boatType = GetBoatType();
            var boatLength = GetBoatLength();
            _memberCtrl.RegisterBoat(boatType, boatLength, member);

        }

        /// <summary>
        /// Delete Boat
        /// </summary>
        /// <param name="member">Owner of the boat</param>
        private static void DeleteBoat(Member member)
        {
            var boats = member.BoatsToString();
            Console.Clear();
            Console.WriteLine("Which boat would you like to delete?");
            _view.RenderBoats(boats);
            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) && (input > member.BoatsOwned.Count || input < 1))
            {
                _view.RenderWrongInput();
                key = Console.ReadLine();
            }
            try
            {
                _memberCtrl.DeleteBoat(input - 1, member);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to delete boat...");
                Console.WriteLine("Hit Enter to return to menu...");
                Console.ReadLine();
                MainMenu();
            }
            
        }

        /// <summary>
        /// Edit boat
        /// </summary>
        /// <param name="member">Owner of the boat</param>
        private static void EditBoat(Member member)
        {
            var boats = member.BoatsToString();
            Console.Clear();
            Console.WriteLine("Which boat would you like to edit?");
            _view.RenderBoats(boats);
            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) && (input > member.BoatsOwned.Count || input < 1))
            {
                _view.RenderWrongInput();
                key = Console.ReadLine();
            }

            var index = input - 1;

            _view.RenderRegisterBoat();
            var boatType = GetBoatType();
            var boatLength = GetBoatLength();
            _memberCtrl.UpdateBoat(index, member, boatType, boatLength);
        }

        /// <summary>
        /// Lets user select boat type
        /// </summary>
        /// <returns></returns>
        private static BoatType GetBoatType()
        {
            BoatType boatType;
            var key = Console.ReadLine();
            int input;
            while (!int.TryParse(key, out input) || input > 4 || input < 1)
            {
                _view.RenderWrongInput();
                key = Console.ReadLine();
            }

            switch (input)
            {
                case 1:
                    boatType = BoatType.Sailboat;
                    break;
                case 2:
                    boatType = BoatType.Motorsailer;
                    break;
                case 3:
                    boatType = BoatType.Kayak;
                    break;
                case 4:
                    boatType = BoatType.Other;
                    break;
                default:
                    boatType = BoatType.Other;
                    break;
            }

            return boatType;
        }

        /// <summary>
        /// Lets user input boat length
        /// </summary>
        /// <returns></returns>
        private static double GetBoatLength()
        {
            Console.WriteLine("Now we need the length of the boat in metres. " +
                              "\nExample input: 3, 4.2, 5.75");
            var key = Console.ReadLine();
            double length;
            while (!double.TryParse(key, out length) || length <= 0)
            {
                _view.RenderWrongInput();
                key = Console.ReadLine();
            }
            return length;
        }

        /// <summary>
        /// Edit the member
        /// </summary>
        /// <param name="member">Member to be edited</param>
        private static void EditMember(Member member)
        {
            Console.Clear();
            _view.RenderEditMember();
            Console.WriteLine("Full Name (Firstname Lastname):");
            var name = Console.ReadLine();

            //Assign original name if nothing new
            if (string.IsNullOrWhiteSpace(name))
            {
                name = member.Name;
            }

            Console.WriteLine("Personal Number (10 digits):");
            var numberInput = Console.ReadLine();
            long personalNumber;

            //Assign original personal number if nothing new
            if (string.IsNullOrWhiteSpace(numberInput))
            {
                personalNumber = member.PersonalNumber;
            }
            else
            {
                while (!long.TryParse(numberInput, out personalNumber))
                {
                    Console.WriteLine("No good, personal number must be a number!");
                    numberInput = Console.ReadLine();
                    Console.Clear();
                }
            }
            
            _memberCtrl.Update(name, personalNumber, member);
            Console.Clear();
            Console.WriteLine("Member successfully updated!");
            Console.WriteLine("Hit any key to go back to the menu...");
            Console.ReadLine();
        }
    }
}
