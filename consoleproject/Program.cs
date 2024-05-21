using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Schema;

namespace EBMS
{
    class Program
    {
        static List<Seller> sellers = new List<Seller>();
        static List<Buyer> buyers = new List<Buyer>();
        static Admin admin = new Admin();
        public static String mobregx = "(0/91)?[7-9][0-9]{9}";

        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\t\t\t\tWelcome to Electronic Bidding Management System");
            Console.WriteLine("\t\t\t\t\t\t\t\t-----------------------------------------------------");

            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("\t\t\t\t\t\t\t\t-----------------------------------------------------");
                Console.WriteLine("\t\t\t\t\t\t\t\tAre you a \n\n\t\t\t\t\t\t\t\t1.Seller       2.Buyer         3.Admin         4.Exit");
                int choi = Convert.ToInt32(Console.ReadLine());
                if (choi == 1 || choi == 2)
                {
                    Console.WriteLine("\t\t\t\t\t\t\t\tAre You a New User:\n\n\t\t\t\t\t\t\t\t1.Yes         2.No");
                    Console.WriteLine();
                    int chu = Convert.ToInt32(Console.ReadLine());
                    if (chu == 1)
                    {
                        if (choi == 1)
                        {
                            SignUp('s');
                        }
                        else
                        if (choi == 2)
                        {
                            SignUp('b');
                        }
                    }
                    else
                    {
                        if (chu == 2)
                        {
                            if (choi == 1)
                            {
                                LoginAsSeller();

                            }
                            else
                                if (choi == 2)
                            {
                                LoginAsBuyer();
                            }
                        }
                    }
                }
                else if (choi == 3)
                {
                    AdminLogin();
                }
                else
                {
                    exit = true;
                }


            }
        }


        static void LoginAsSeller()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t\t\t\t\t\t\tEnter your username: ");
            string username = Console.ReadLine();
            Console.Write("\t\t\t\t\t\t\t\tEnter your password: ");
            string password = Console.ReadLine();

            Seller seller = sellers.Find(s => s.Username == username && s.Password == password);
            if (seller != null)
            {
                Console.WriteLine($"\t\t\t\t\t\t\t\tWelcome, {seller.Name}!");
                Console.WriteLine();
                Console.WriteLine($"\t\t\t\t\t\t\t\tMobile Number: {seller.Mobile}!");
                Console.WriteLine();
                Console.WriteLine($"\t\t\t\t\t\t\t\tE-mail: {seller.Email}!");
                Console.WriteLine();
                Console.WriteLine($"\t\t\t\t\t\t\t\tAddress: {seller.Add}!");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\tDo You Want update your profile\n\t\t\t\t\t\t\t\t1.Yes           2.No?");
                int up = Convert.ToInt32(Console.ReadLine());
                bool exit = false;
                if (up == 1)
                {
                    while (!exit)
                    {




                        Console.WriteLine("\t\t\t\t\t\t\t\tChoose the below details to be updated");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t1.Name:             {seller.Name}");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t2.Mobile Number:    {seller.Mobile}");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t3.E-Mail:           {seller.Email}");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t4.Address:          {seller.Add}");
                        Console.WriteLine("\t\t\t\t\t\t\t\t5.Exit");
                        int up1 = Convert.ToInt32(Console.ReadLine());
                        switch (up1)
                        {
                            case 1:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your Name to be updated");
                                    seller.Name = Console.ReadLine();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour name had been updated successfully");
                                }
                                break;
                            case 2:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your Mobile Number to be updated");
                                    seller.Mobile = mobileFormatCheck();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour Mobile Number had been updated successfully");
                                }
                                break;
                            case 3:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your E-mail to be updated");
                                    seller.Email = Console.ReadLine();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour E-mail had been updated successfully");
                                }
                                break;
                            case 4:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your Address to be updated");
                                    seller.Add = Console.ReadLine();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour Address had been updated successfully");

                                }
                                break;
                            case 5: exit = true; break;
                            default:
                                Console.WriteLine("\t\t\t\t\t\t\t\tInvalid Choice");
                                break;


                        }
                    }//while loop ending for crud operations

                }//CRUD opertions for the user



                Console.WriteLine("\t\t\t\t\t\t\t\tDo you wanna bid ?\n\t\t\t\t\t\t\t\t1.Yes        2.No");
                int cho = Convert.ToInt32(Console.ReadLine());
                if (seller.bid == 0)
                {
                    if (cho == 1)
                    {


                        foreach (Buyer buyer in buyers)
                        {
                            if (buyer.maxval != 0)
                            {
                                // Perform actions for each buyer here
                                Console.WriteLine($"\t\t\t\t\t\t\t\tBuyer Name:                {buyer.Name}");
                                Console.WriteLine();
                                Console.WriteLine($"\t\t\t\t\t\t\t\tBuyer BID ITEMS:           {buyer.Biditems}");
                                Console.WriteLine();
                                Console.WriteLine($"\t\t\t\t\t\t\t\tBase valu for above items: {buyer.maxval}");
                                Console.WriteLine();
                            }
                        }
                        int co = 0;
                        Console.Write("\t\t\t\t\t\t\t\tEnter Name Of the buyer you want to bid:");
                        seller.uname = Console.ReadLine();
                        Console.Write("\t\t\t\t\t\t\t\tEnter your bid value:");
                        seller.bid = Convert.ToInt32(Console.ReadLine());
                        foreach (Buyer buyer in buyers)
                        {
                            if (seller.uname == buyer.Name && seller.bid < buyer.maxval)
                            {
                                co = co + 1;
                            }

                        }
                        if (co >= 1)
                        {
                            Console.WriteLine("\t\t\t\t\t\t\t\tYour Bid has be taken successfully");
                        }
                        else
                        {
                            Console.WriteLine("\t\t\t\t\t\t\t\tEnter valid Buyer Name");
                            seller.bid = 0;
                            seller.uname = "";
                        }


                    }
                }
                else
                {
                    Console.WriteLine("\t\t\t\t\t\t\t\tYou have Exixsting bid");
                }
                // Implemented seller functionalities here
            }
            else
            {
                Console.WriteLine("\t\t\t\t\t\t\t\tInvalid username or password.");
            }
        }

        public static void LoginAsBuyer()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\t\t\t\t\t\t\t\tEnter your username: ");
            string username = Console.ReadLine();
            Console.Write("\t\t\t\t\t\t\t\tEnter your password: ");
            string password = Console.ReadLine();

            Buyer buyer = buyers.Find(b => b.Username == username && b.Password == password);
            if (buyer != null)
            {
                Console.WriteLine($"\t\t\t\t\t\t\t\tWelcome, {buyer.Name}!");
                Console.WriteLine();
                Console.WriteLine($"\t\t\t\t\t\t\t\tMobile Number: {buyer.Mobile}!");
                Console.WriteLine();
                Console.WriteLine($"\t\t\t\t\t\t\t\tE-mail:        {buyer.Email}!");
                Console.WriteLine();
                Console.WriteLine($"\t\t\t\t\t\t\t\tAddress:       {buyer.Add}!");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\tDo You Want update your profile\n\t\t\t\t\t\t\t\t1.Yes           2.No?");
                int up = Convert.ToInt32(Console.ReadLine());
                if (up == 1)
                {

                    bool exit = false;
                    while (!exit)
                    {



                        Console.WriteLine("\t\t\t\t\t\t\t\tChoose the below details to be updated");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t1.Name:             {buyer.Name}");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t2.Mobile Number:    {buyer.Mobile}");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t3.E-Mail:           {buyer.Email}");
                        Console.WriteLine($"\t\t\t\t\t\t\t\t4.Address:          {buyer.Add}");
                        Console.WriteLine("\t\t\t\t\t\t\t\t5.Exit");
                        int up1 = Convert.ToInt32(Console.ReadLine());
                        switch (up1)
                        {
                            case 1:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your Name to be updated");
                                    buyer.Name = Console.ReadLine();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour name had been updated successfully");
                                }
                                break;
                            case 2:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your Mobile Number to be updated");
                                    buyer.Mobile = mobileFormatCheck();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour Mobile Number had been updated successfully");
                                }
                                break;
                            case 3:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your E-mail to be updated");
                                    buyer.Email = Console.ReadLine();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour E-mail had been updated successfully");
                                }
                                break;
                            case 4:
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Your Address to be updated");
                                    buyer.Add = Console.ReadLine();
                                    Console.WriteLine("\t\t\t\t\t\t\t\tYour Address had been updated successfully");

                                }
                                break;
                            case 5: exit = true; break;
                            default:
                                Console.WriteLine("\t\t\t\t\t\t\t\tInvalid Choice");
                                break;


                        }
                    }//while loop ending for crud operations

                }//CRUD opertions for the user






                Console.WriteLine("\t\t\t\t\t\t\t\tDo You wanna add your Bid List!\n\t\t\t\t\t\t\t\t1.Yes           2.No");
                int cho = Convert.ToInt32(Console.ReadLine());
                if (cho == 1)
                {
                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter your bid items:");
                    buyer.Biditems = Console.ReadLine();
                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter Base Value for the above BID");
                    buyer.maxval = Convert.ToInt32(Console.ReadLine());



                }
                else
                {
                    Console.WriteLine("\t\t\t\t\t\t\t\tThank You");

                }
                int min = buyer.maxval;

                Console.WriteLine("\t\t\t\t\t\t\t\tDo You Want to close the bid\n1.Yes\n2.No");

                int chh = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (chh == 1)
                {
                    foreach (Seller bu in sellers)
                    {

                        if (String.Equals(bu.uname, buyer.Name))
                        {

                            if (min > bu.bid)
                            {
                                min = bu.bid;
                                buyer.bidname = bu.Name;

                            }
                        }

                    }
                    Console.WriteLine($"\t\t\t\t\t\t\t\tYour bid is finilised to:{buyer.bidname}");
                    Console.WriteLine("\t\t\t\t\t\t\t\tYour Bid is Closed!!");
                    buyer.maxval = 0;
                    buyer.Biditems = "";
                }
                else
                {
                    Console.WriteLine("\t\t\t\t\t\t\t\tYour items bid is continued....");
                }


                // Buyer functionalities are here!!
            }
            else
            {
                Console.WriteLine("\t\t\t\t\t\t\t\tInvalid username or password.");
            }
        }
        public static long mobileFormatCheck()
        {

            long mob = Convert.ToInt64(Console.ReadLine());
            string mobileNumber = mob.ToString();
            var match = Regex.Match(mobileNumber, mobregx);
            if (match.Success)
            {
                return mob;
            }
            else
            {
                Console.Write("\t\t\t\t\t\t\t\tEnter valid mobile number:");
                mobileFormatCheck();
                return 0;
            }


        }

        static void SignUp(char t)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t\t\t\t\t\t\tName               :");
            string name = Console.ReadLine();
            Console.Write("\t\t\t\t\t\t\t\tMobile Number      :");
            long mob = mobileFormatCheck();
            Console.Write("\t\t\t\t\t\t\t\tE-mail             :");
            string email = Console.ReadLine();
            Console.Write("\t\t\t\t\t\t\t\tAddress            :");
            string add = Console.ReadLine();
            Console.Write("\t\t\t\t\t\t\t\tEnter your username: ");
            string username = Console.ReadLine();
            Console.Write("\t\t\t\t\t\t\t\tEnter your password: ");
            string password = Console.ReadLine();

            char type = t;

            if (type == 's')
            {
                sellers.Add(new Seller(name, mob, email, add, username, password));
                Console.WriteLine("\t\t\t\t\t\t\t\tSeller account created successfully.");
            }
            else if (type == 'b')
            {
                buyers.Add(new Buyer(name, mob, email, add, username, password));
                Console.WriteLine("\t\t\t\t\t\t\t\tBuyer account created successfully.");
            }
            else
            {
                Console.WriteLine("\t\t\t\t\t\t\t\tInvalid choice.");
            }
        }

        static void AdminLogin()
        {
            Console.Write("\t\t\t\t\t\t\t\tEnter admin password: ");
            string password = Console.ReadLine();

            if (admin.Password == password)
            {
                Console.WriteLine("\t\t\t\t\t\t\t\tWelcome, Admin!");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\tTotal list of buyers:");
                Console.WriteLine();

                foreach (Buyer buyer in buyers)
                {
                    // Perform actions for each buyer here
                    Console.WriteLine($"\t\t\t\t\t\t\t\tBuyer Name: {buyer.Name}");
                    Console.WriteLine();

                }
                Console.WriteLine("\t\t\t\t\t\t\t\tTotal list of Sellers:");
                Console.WriteLine();
                foreach (Seller seller in sellers)
                {
                    // Perform actions for each buyer here
                    Console.WriteLine($"\t\t\t\t\t\t\t\tBuyer Name: {seller.Name}");
                    Console.WriteLine();

                }

                // Implement admin functionalities here
            }
            else
            {
                Console.WriteLine("\t\t\t\t\t\t\t\tInvalid password.");
            }
        }
    }

    class Seller
    {
        public string Username { get; }
        public string Password { get; }

        public string Name { get; set; }
        public long Mobile { get; set; }
        public string Email { get; set; }
        public string Add { get; set; }
        public int bid { get; set; }
        public string uname { get; set; }

        public Seller(string name, long mobile, string email, string add, string username, string password)
        {
            Name = name;
            Mobile = mobile;
            Email = email;
            Add = add;
            Username = username;
            Password = password;
            bid = 0;
            uname = "";
        }
    }

    class Buyer
    {
        public string Username { get; }
        public string Password { get; }
        public string Name { get; set; }
        public long Mobile { get; set; }
        public string Email { get; set; }
        public string Add { get; set; }
        public string Biditems { get; set; }
        public int maxval { get; set; }
        public string bidname { get; set; }

        public Buyer(string name, long mobile, string email, string add, string username, string password)
        {
            Name = name;
            Mobile = mobile;
            Email = email;
            Add = add;
            Username = username;
            Password = password;
            Biditems = "";
            maxval = 0;
            bidname = "";
        }
    }

    class Admin
    {
        public string Password { get; } = "admin123"; // Change this password as needed
    }
}
