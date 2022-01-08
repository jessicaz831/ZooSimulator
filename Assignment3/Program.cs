// Jessica Zhu & Narges Karimzad
// June 10, 2019
// This program is a zoo simulator, it allows the user to create animal exhibits and take care of the animals

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Program

    {
        /************************ JESSICA START *************************/

        // declare array to store the names of the animals, starting with a size of 0
        static string[] names = new string[0];
        // declare array to store the descriptions of the animals, starting with a size of 0
        static string[] descriptions = new string[0];
        // declare array to store the quantities of the animals, starting with a size of 0
        static int[] quantities = new int[0];        // declare array to store the amount of food for the animals, starting with a size of 0
        static int[] food = new int[0];
        // declare array to store the prices of the animals, starting with a size of 0
        static double[] prices = new double[0];
        // declare a random number generator
        static Random generator = new Random();

        static void Main(string[] args)
        {
            // declaring variables needed for parameters for the Menu subprogram
            // declare money variable, user starts with $7000
            double money = 7000;
            // declare a counter variable to count number of days the zoo has been open, startint at 1
            int dayCount = 1;
            // run CreaterStarterZoo subprogram
            CreateStarterZoo();
            // run Menu subprogram
            Menu(money, dayCount);
        }

        // Returns a string, displaying all existing habitats (name, description, quantity, food, and price) in the order they were created in
        static string CheckAllHabitats()
        {
            // declare a string variable to return later, set it as an empty string to avoid errors
            string message = "";

            // loop through 'names' array, to display all data
            for (int i = 0; i < names.Length; i++)
            {
                // adds data at i to the message variable using FormatAnimalData subprogram
                message = message + FormatAnimalData(i) + "\n\n";
            }

            // return the message
            return message;
        }

        // Starts the user with 3 habitats in their zoo
        static void CreateStarterZoo()
        {
            // declare int variable for money used to create 3 starter habitats so it doesn't take away from the user's $7000
            double money = 6000;

            // run BuildNewHabitat 3 times with parameters (user doesn't get to choose)
            BuildNewHabitat("Tiger", "Big kitty with black stripes", 200, ref money);
            BuildNewHabitat("Lion", "Big kitty with fluffy mane", 100, ref money);
            BuildNewHabitat("Panda", "Big black and white bear from China", 300, ref money);
        }

        // Tries to feed as many animals as possible with 'moneyAvailable', returns a string saying what animals were fed, 
        //what animals will starve, or if there are no animals and how much money the user has after feeding
        static string FeedAnimals(ref double moneyAvailable)
        {
            // declare a string variable to return later, set it as an empty string to avoid errors
            string message = "";

            // loop through 'names' array, see how many animals can be fed in each habitat
            for (int i = 0; i < names.Length; i++)
            {
                // only run the code if there are animals in the habitat at i
                if (quantities[i] > 0)
                {
                    // calculate how much it will cost to feed ALL animals in the habitat, how many animals x 0.25 x price of animal
                    double cost = quantities[i] * 0.25 * prices[i];

                    // only run code if user has enough money
                    if (moneyAvailable >= cost)
                    {
                        // all animals are fed so add however many animals there are in the habitat to the food array at i (1 food/animal)
                        food[i] = food[i] + quantities[i];
                        // deduct cost from user's money
                        moneyAvailable = moneyAvailable - cost;
                        // add to message that all the animals in this specific habitat at i will be fed today
                        message = message + "All " + names[i] + "s will be fed today.\n";
                    }

                    // run if user doesn't have enough money to feed ALL animals
                    else
                    {
                        // calculate cost for one unit of food, 0.25 x price of animal
                        cost = 0.25 * prices[i];
                        // declare int variable
                        // calculate how many animals can be fed by dividing user's money by cost of one unit of food
                        int animalsToFeed = (int)(moneyAvailable / cost);
                        // add however much food was bought to the food array at i
                        food[i] = food[i] + animalsToFeed;
                        // deduct one unit of food x how many animals were fed from user's money
                        moneyAvailable = moneyAvailable - cost * animalsToFeed;
                        // declare int variable
                        // calculate how many animals do not receive food by subtracting total quantity in that habitat by how many animals were fed
                        int animalsNotFed = quantities[i] - animalsToFeed;
                        // add to message how many animals and which animal will starve tomorrow
                        message = message + animalsNotFed + " " + names[i] + " will starve tomorrow.\n";
                    }
                }

                // run if the quantity is 0 at i
                else
                {
                    // add to message that there are no animals in the habitat at i, therefore cannot be fed
                    message = message + "\nThere are no animals in the " + names[i] + " habitat to feed.\n";
                }
            }

            // add at the end of the message how much money the user has after feeding as many animals as possible
            message = message + "You now have $" + moneyAvailable;
            // return the message
            return message;
        }

        // Returns a string that neatly displays the animal's data at 'index'
        static string FormatAnimalData(int index)
        {
            // return string showing name, description, price, quantity, and food of animal at given index
            return "Name: " + names[index] +
                "\nDescription: " + descriptions[index] +
                "\nPrice: " + prices[index] +
                "\nQuantity Owned: " + quantities[index] +
                "\nFood in Habitat: " + food[index];
        }

        // Returns a string that neatly displays the animal's data at 'index', used for sorting subprograms
        static string FormatAnimalData(int index, string[] sNames, string[] sDescriptions, double[] sPrices, int[] sQuantities, int[] sFood)
        {
            // return string showing name, description, price, quantity, and food of animal at given index
            return "Name: " + sNames[index] +
                "\nDescription: " + sDescriptions[index] +
                "\nPrice: " + sPrices[index] +
                "\nQuantity Owned: " + sQuantities[index] +
                "\nFood in Habitat: " + sFood[index];
        }

        // Menu system that runs different subprograms based on user input, also displays output to user
        static void Menu(double money, int dayCount)
        {
            // display the day the user is on and how much money they have using parameters 'money' and 'dayCount'
            // display all menu options
            Console.WriteLine("Day " + dayCount + "\nMoney: $" + money +
                "\n0) End the Day" +
                "\n1) Feed Animals" +
                "\n2) Buy Animal for Existing Habitat" +
                "\n3) Sell Animal for Existing Habitat" +
                "\n4) Build New Habitat for Animals ($2000)" +
                "\n5) Destroy Existing Habitat ($500)" +
                "\n6) Check All Habitats" +
                "\n7) Check Habitat by Quantity Search" +
                "\n8) Check Habitat by Animal Description Search" +
                "\n9) Show Animals by Ascending Quantities" +
                "\n10) Show Animals by Descending Price" +
                "\n11) Quit Zoo Program");
            // get user input with ReadLine and save to 'input' variable
            string input = Console.ReadLine();

            if (input == "0")
            {
                // ref 'money' and 'dayCount' variables into EndTurn subprogram
                // display return value from EndTurn subprogram
                Console.WriteLine(EndTurn(ref money, ref dayCount));

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "1")
            {
                // ref 'money' variable into FeedAnimals subprogram
                // display return value from FeedAnimals subprogram
                Console.WriteLine(FeedAnimals(ref money));

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "2")
            {
                // ask user what animal they want to buy
                Console.WriteLine("Name of animal to buy: ");
                // ref 'money' variable and pass user input (ReadLine) into BuyExistingAnimal subprogram
                // display return value from BuyExistingAnimal subprogram
                Console.WriteLine(BuyExistingAnimal(ref money, Console.ReadLine()));

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "3")
            {
                // ask user what animal they want to sell
                Console.WriteLine("Name of animal to sell: ");
                // ref 'money' variable and pass user input (ReadLine) into SellExistingAnimal subprogram
                // display return value from SellExistingAnimal subprogram
                Console.WriteLine(SellExistingAnimal(ref money, Console.ReadLine()));

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "4")
            {
                // display that a new habitat is being built
                Console.WriteLine("Building new habitat...");
                // pass prompt that asks for name of animal into InputNotEmpty
                // save InputNotEmpty's return value to 'name' variable
                string name = InputNotEmpty("Name of animal to build for: ");
                // pass prompt that asks for description of animal into InputNotEmpty
                // save InputNotEmpty's return value to 'description' variable
                string description = InputNotEmpty("Description of animal: ");
                // ask user to input a price between 50 and 500
                Console.WriteLine("Price of animal for which to build habitat: (Input a number between 50 and 500)");
                // pass 50 as a minimum and 500 as a maximum into InputNumber subprogram
                // save InputNumber's return value to 'price' variable
                double price = InputNumber(50, 500);

                // pass 'name', 'description', and 'price' variables collected earlier and ref 'money' variable into BuildNewHabitat subprogram
                // display return value from BuildNewHabitat subprogram
                Console.WriteLine(BuildNewHabitat(name, description, price, ref money));

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "5")
            {
                // ask user what habitat they want to remove
                Console.WriteLine("Input the name of the animal whose habitat is to be removed from the zoo: ");
                // ref 'money' variable and pass user input (ReadLine) into RemoveExistingHabitat subprogram
                // display return value from RemoveExistingHabitat subprogram
                Console.WriteLine(RemoveExistingHabitat(ref money, Console.ReadLine()));

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "6")
            {
                // display return value from CheckAllHabitats subprogram
                Console.WriteLine(CheckAllHabitats());

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "7")
            {
                // ask the user what minimum number to search for
                Console.WriteLine("Searching for animals by quantity... Input the minimum quantity of animals to search for: ");
                // convert user input (ReadLine) from string to int using TryParse, store in 'minimum' variable
                int.TryParse(Console.ReadLine(), out int minimum);
                // pass 'minimum' into SearchByQuantity subprogram
                // save returned array from SearchByQuantity into 'array'
                string[] array = SearchByQuantity(minimum);
                // loops through 'array' to display all info
                for (int i = 0; i < array.Length; i++)
                {
                    // display info stored in array at 'index'
                    Console.WriteLine(array[i] + "\n");
                }

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "8")
            {
                // ask user for keyword(s) to search for
                Console.Write("Searching for animals by description... Input the keyword(s) to search for: ");
                // pass user input (ReadLine) into SearchByDescription subprogram
                // save returned array from SearchByDescription into 'array'
                string[] array = SearchByDescription(Console.ReadLine());
                // goes through all indices in array
                for (int i = 0; i < array.Length; i++)
                {
                    // displays the info of the animal at index i from 'array'
                    Console.WriteLine(array[i] + "\n");
                }

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "9")
            {
                // save returned array from ShowAscendingQuantities into 'array'
                string[] array = ShowAscendingByQuantities();
                // loops through 'array' to display all info
                for (int i = 0; i < array.Length; i++)
                {
                    // displays the info of the animal at index i from 'array'
                    Console.WriteLine(array[i] + "\n");
                }

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "10")
            {
                // save returned array from ShowDescendingPrice into 'array'
                string[] array = ShowDescendingByPrice();
                // loop through 'array' to display all info
                for (int i = 0; i < array.Length; i++)
                {
                    // displays the info of the animal at index i from 'array'
                    Console.WriteLine(array[i] + "\n");
                }

                // display "Press any key to continue..."
                Console.WriteLine("Press any key to continue...");
                // wait for user to press a key
                Console.ReadKey();
                // clear console screen
                Console.Clear();
                // run Menu subprogram again with 'money' and 'dayCount' passed in
                Menu(money, dayCount);
            }
            else if (input == "11")
            {
                // closes console application
                Environment.Exit(0);
            }
        }

        // Deletes a habitat from the zoo, returns a string if the animal wasn't found, not enough money, or if the habitat was removed and how many animals were sold in the process, and how much money the user has
        static string RemoveExistingHabitat(ref double money, string name)
        {
            // pass 'names' array and 'name' parameter into ListFindExactIgnoreCase
            // save returned value from ListFindExactIgnoreCase to 'i' variable
            int i = ListFindExactIgnoreCase(names, name);
            // declare a string variable to return later, set it as an empty string to avoid errors
            string message = "";

            // if ListFindExactIgnoreCase returned -1, return a string saying the habitat couldn't be found
            if (i == -1)
            {
                message = "Animal habitat could not be found.";
                return message;
            }

            // if the user doesn't have enough money (< 500), return a string saying there isn't enough money
            if (money < 500)
            {
                message = "Not enough money to remove this habitat.";
                return message;
            }

            // deduct 500 from user's money
            money = money - 500;

            // if there are animals in the habitat, add to 'message' how many and what animals will be sold for how much
            if (quantities[i] > 0)
            {
                message = quantities[i] + " " + names[i] + " were sold for $" + (prices[i] * quantities[i]) + ".\n";
            }

            // add to message that the habitat was removed and how much money the user has
            message = message + "The " + names[i] + " habitat was removed successfully for $500. You now have $" + money;

            // if there are animals in the habitat, sell them using the SellExistingAnimal subprogram
            // keep running the subprogram until all animals are sold
            while (quantities[i] > 0)
            {
                // ref 'money' and pass 'name' variable into SellExistingAnimal
                SellExistingAnimal(ref money, name);
            }

            // run ListDelete subprogram for each array at 'i'
            ListDelete(ref names, i);
            ListDelete(ref descriptions, i);
            ListDelete(ref quantities, i);
            ListDelete(ref food, i);
            ListDelete(ref prices, i);

            // return the message
            return message;
        }

        // Searches for habitats that are greater or equal to 'minimum', returns a string array with all habitats that are greater or equal to 'minimum'
        static string[] SearchByQuantity(int minimum)
        {
            // pass 'quantities' array and 'minimum' parameter
            // save returned array from ShowAscendingQuantities into 'array'
            int[] array = ListFindAtLeast(quantities, minimum);
            // declare an array with same length as 'array' to store the data that fits the search
            string[] animalData = new string[array.Length];

            // loop through 'array' to save info into 'animalData' array
            for (int i = 0; i < array.Length; i++)
            {
                // pass 'array' at index 'i' into FormatAnimalData
                // save returned string from FormatAnimalData into 'animalData' at 'i'
                animalData[i] = FormatAnimalData(array[i]);
            }

            // return the 'animalData' array with all the data
            return animalData;
        }

        // Sells an animal in the habitat 'name', returns a string saying if the animal couldn't be found, if there are no animals in the habitat, or what animal was sold and for how much, and how much money the user has
        static string SellExistingAnimal(ref double moneyAvailable, string name)
        {
            // pass 'names' array and 'name' parameter into ListFindExactIgnoreCase
            // save returned value from ListFindExactIgnoreCase to 'i' variable
            int i = ListFindExactIgnoreCase(names, name);
            // declare a string variable to return later, set it as an empty string to avoid errors
            string message = "";

            // if ListFindExactIgnoreCase didn't return -1...
            if (i != -1)
            {
                // if there are animals in the habitat...
                if (quantities[i] > 0)
                {
                    // subtract 1 from the 'quantities' array
                    quantities[i] = quantities[i] - 1;
                    // add the price of the animal to the user's money
                    moneyAvailable = moneyAvailable + prices[i];
                    // add to message what animal was sold, for how much, and how much money the user has
                    message = names[i] + " was sold for $" + prices[i] + "You now have $" + moneyAvailable + ".";
                    // return the message
                    return message;
                }
                // if quantities at 'i' is not greater than 0, return that there are no animals
                message = "There are no animals available to be sold.";
                // return the message
                return message;
            }
            // if ListFindExactIgnoreCase returned -1, return that the animal couldn't be found
            message = "Animal could not be found.";
            // return the message
            return message;
        }

        // Returns an array with all the habitats sorted by quantity
        static string[] ShowAscendingByQuantities()
        {
            // declare a copy array with the same length as the original array for all the arrays
            string[] sNames = new string[names.Length];
            string[] sDescriptions = new string[descriptions.Length];
            double[] sPrices = new double[prices.Length];
            int[] sQuantities = new int[quantities.Length];
            int[] sFood = new int[food.Length];

            // loop through 'names' to copy info to copy array
            for (int i = 0; i < names.Length; i++)
            {
                // copy all info from original array to copy array for all the arrays
                sNames[i] = names[i];
                sDescriptions[i] = descriptions[i];
                sPrices[i] = prices[i];
                sQuantities[i] = quantities[i];
                sFood[i] = food[i];
            }

            // declare another array with the same length as the 'names' array (all arrays are the same size), this array will keep the data after being sorted
            string[] sortedData = new string[names.Length];

            // repeatedly uses the loop inside -- this means it repeatedly moves
            // the next biggest value to the end of the array
            // We can do "toSort.Length - 1" to stop looping without looking at
            // the first element. The first element will automatically be sorted
            // when the second element gets sorted.
            for (int k = 0; k < sQuantities.Length; k++)
            {
                // swap the next biggest item all the way to the end of the array
                // we stop looping at "toSort.Length - 1 - k" -- by doing the
                // minus k, each time that it loops we stop looping 1 element sooner.
                // We can do this because each time this loop finishes, one more
                // element in the array is already sorted -- we don't need to keep
                // sorting something that is already sorted
                for (int i = 0; i < sQuantities.Length - 1 - k; i++)
                {
                    // compare to side-by-side values -- if the left one is bigger
                    // then it should be swapped with the right one
                    if (sQuantities[i] > sQuantities[i + 1])
                    {
                        // temporarily store the left value
                        string temp = sNames[i];
                        // move the right value to the left element
                        sNames[i] = sNames[i + 1];
                        // move the left value into the right element
                        sNames[i + 1] = temp;

                        // temporarily store the left value
                        temp = sDescriptions[i];
                        // move the right value to the left element
                        sDescriptions[i] = sDescriptions[i + 1];
                        // move the left value into the right element
                        sDescriptions[i + 1] = temp;

                        // temporarily store the left value
                        double temp2 = sPrices[i];
                        // move the right value to the left element
                        sPrices[i] = sPrices[i + 1];
                        // move the left value into the right element
                        sPrices[i + 1] = temp2;

                        // temporarily store the left value
                        int temp3 = sQuantities[i];
                        // move the right value to the left element
                        sQuantities[i] = sQuantities[i + 1];
                        // move the left value into the right element
                        sQuantities[i + 1] = temp3;

                        // temporarily store the left value
                        temp3 = sFood[i];
                        // move the right value to the left element
                        sFood[i] = sFood[i + 1];
                        // move the left value into the right element
                        sFood[i + 1] = temp3;
                    }
                }
            }

            // loop through 'array' to save info into 'sortedData' array
            for (int i = 0; i < names.Length; i++)
            {
                // pass the index 'i' and all the sorted arrays into FormatAnimalData
                // save returned string from FormatAnimalData into 'sortedData' at 'i'
                sortedData[i] = FormatAnimalData(i, sNames, sDescriptions, sPrices, sQuantities, sFood);
            }
            // return the 'sortedData' array with all the sorted info
            return sortedData;
        }

        // Decreases the 'data' array size by 1 -- in doing so it also deletes
        // the last element in the data array
        static void DecreaseArraySize(ref double[] data)
        {
            // when we resize the array we need to save a copy of the data
            double[] copy = new double[data.Length - 1];

            // copy all the data over into the copy array
            for (int i = 0; i < copy.Length; i++)
            {
                copy[i] = data[i];
            }

            // set the data array to be the smaller copy
            data = copy;
        }

        // Decreases the 'data' array size by 1 -- in doing so it also deletes
        // the last element in the data array
        static void DecreaseArraySize(ref int[] data)
        {
            // when we resize the array we need to save a copy of the data
            int[] copy = new int[data.Length - 1];

            // copy all the data over into the copy array
            for (int i = 0; i < copy.Length; i++)
            {
                copy[i] = data[i];
            }

            // set the data array to be the smaller copy
            data = copy;
        }

        // Decreases the 'data' array size by 1 -- in doing so it also deletes
        // the last element in the data array
        static void DecreaseArraySize(ref string[] data)
        {
            // when we resize the array we need to save a copy of the data
            string[] copy = new string[data.Length - 1];

            // copy all the data over into the copy array
            for (int i = 0; i < copy.Length; i++)
            {
                copy[i] = data[i];
            }

            // set the data array to be the smaller copy
            data = copy;
        }

        // Deletes the element located at 'indexToDelete', and that element
        // is returned. The size of 'array' will decrease by 1. Information in
        // 'array' located after 'indexToDelete' will be shifted, so their
        // indexes will decrease by 1
        static double ListDelete(ref double[] array, int indexToDelete)
        {
            // check for invalid index
            if (indexToDelete < 0 || indexToDelete >= array.Length)
            {
                return -1; // could also return null;
            }

            // we need to return this
            double toBeDeleted = array[indexToDelete];

            // we shift information to the left to do the delete
            for (int i = indexToDelete; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            // run DecreaseArraySize, ref 'array'
            DecreaseArraySize(ref array);
            // return what was deleted
            return toBeDeleted;
        }

        // Deletes the element located at 'indexToDelete', and that element
        // is returned. The size of 'array' will decrease by 1. Information in
        // 'array' located after 'indexToDelete' will be shifted, so their
        // indexes will decrease by 1
        static int ListDelete(ref int[] array, int indexToDelete)
        {
            // check for invalid index
            if (indexToDelete < 0 || indexToDelete >= array.Length)
            {
                return -1; // could also return null;
            }

            // we need to return this
            int toBeDeleted = array[indexToDelete];

            // we shift information to the left to do the delete
            for (int i = indexToDelete; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            // run DecreaseArraySize, ref 'array'
            DecreaseArraySize(ref array);
            // return what was deleted
            return toBeDeleted;
        }

        // Deletes the element located at 'indexToDelete', and that element
        // is returned. The size of 'array' will decrease by 1. Information in
        // 'array' located after 'indexToDelete' will be shifted, so their
        // indexes will decrease by 1
        static string ListDelete(ref string[] array, int indexToDelete)
        {
            // check for invalid index
            if (indexToDelete < 0 || indexToDelete >= array.Length)
            {
                return ""; // could also return null;
            }

            // we need to return this
            string toBeDeleted = array[indexToDelete];

            // we shift information to the left to do the delete
            for (int i = indexToDelete; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            // run DecreaseArraySize, ref 'array'
            DecreaseArraySize(ref array);
            // return what was deleted
            return toBeDeleted;
        }

        // Finds all indexes that have data that's greater or equal to 'minimum', returns an array with all the indexes that are greater or equal to 'minimum'
        static int[] ListFindAtLeast(int[] array, int minimum)
        {
            // counts how many times the data at 'i' is greater or equal than 'minimum' 
            int count = 0;

            // loop through 'array' to find how many times the data at 'i' is greater or equal than 'minimum' 
            for (int i = 0; i < array.Length; i++)
            {
                // if found, increase the counter
                if (array[i] >= minimum)
                {
                    count++;
                }
            }

            // this array is sized to match how many indexes will be found
            int[] indexes = new int[count];
            // reset the count to be used as the index counter
            count = 0;

            // loop through 'array', this time saving any indexes that match
            for (int i = 0; i < array.Length; i++)
            {
                // if found, save the index
                if (array[i] >= minimum)
                {
                    // save the index
                    indexes[count] = i;
                    // increases the count of how many were found
                    count++;
                }
            }

            // return the array with the indexes of the data that is equal or greater than the minimum
            return indexes;
        }

        // Finds the index of 'toFind' in 'array' ignoring any capitalizing, returns the index of 'toFind' or -1 if 'toFind' wasn't found
        static int ListFindExactIgnoreCase(string[] array, string toFind)
        {
            // loop through 'array', looking for 'toFind'
            for (int i = 0; i < array.Length; i++)
            {
                // if found (ignoring capitals), return the index
                if (array[i].ToLower() == toFind.ToLower())
                {
                    return i;
                }
            }
            // if not found, return -1
            return -1;
        }

        /************************ JESSICA END *************************/

        /************************ NARGES START *************************/

        /* This function subprogram will increase the quantity of animals in a
    	particular habitat by chance. This subprogram returns a string.  */
        static string AnimalLove()
        {
            // integer variable to store the quantity of animals
            int randomInt;
            // Empty string variable
            string message = "";
            // return string: all the animals had an increase in population
            // If Randomint is less than the quantity of the animals, then 1 animal
            // will be added to the habitat.
            // loop through all the animals in the array
            for (int i = 0; i < quantities.Length; i++)
            {
                // the maximum number of animals is 10000 divided by price
                int maximumAnimals = 1000 / (int)prices[i];
                // number assigned to randomInt
                randomInt = generator.Next(1, 101);
                if (randomInt < 5 * quantities[i] && quantities[i] < maximumAnimals)
                {
                    quantities[i] = quantities[i] + 1;
                    // The message variable will store the information of the names 
                    // of the habitats that increased population and the new quantity of 
                    // animals now that the quantity increased
                    message = message + names[i] + " just increased population. You now have " + quantities[i] + "!\n";
                }
            }
            // returning the message variable
            return message;
        }
        /* This subprogram checks if there is enough food in the habitat for
    	the quantity of animals there. */
        static string AnimalsEat()
        {
            // creating variables that will be used in the subprogram
            string messageDead = "The animals ";
            string messageFed = "The number of animals that were fed is ";
            // the following are integer variables that are given a starting value
            int animalsFed = 0;
            int animalsUnfed = 0;
            int animalsDead = 0;
            // loop through quantities array and see if they are equal
            for (int i = 0; i < quantities.Length; i++)
            {
                // if there is enough food
                if (quantities[i] <= food[i])
                {
                    // it reduces the food in the habitat by the quantity of animals
                    // this is the number of animals that get fed
                    food[i] = food[i] - quantities[i];
                }

                // if there is not enough food
                else
                {
                    // take away all the food available
                    animalsUnfed = quantities[i] - food[i];
                    // the number of unfed animals get divided by 3
                    // this the number of animals that get eaten by their "friends" or die out of starvation
                    animalsDead = animalsUnfed / 3;
                    // the minimum number of dead animals must be greater than one
                    if (animalsDead < 1)
                    {
                        // one animal died
                        animalsDead = 1;
                    }
                    // The quantities of the animals must decrease now that animals died
                    quantities[i] = quantities[i] - animalsDead;
                    // there should be no more food left
                    food[i] = 0;
                    // this is the names of the animal that died
                    messageDead += names[i] + ", " + "have died due to starvation";
                }
            }
            // return that all the animals were fed
            return "All animals were fed.";
        }

        // This subprogram builds a new habitat. The subprogram will return a string.  
        // This subprogram takes the name, description, price and the money as parameters 
        // This is menu option 4
        static string BuildNewHabitat(string name, string description, double price, ref double money)
        {
            // string variable to store the messages
            string messages = "";
            // only 10 different animal types/habitats can be at the zoo
            // if there are more than 10 different habitats...
            if (quantities.Length >= 10)
            {
                // the message states that the zoo is full 
                messages = "The zoo is full. Only a maximum of 10 habitats can be at the zoo";
                // return the message
                return messages;
            }
            // if the user does not have enough money
            if (money < 2000)
            {
                // tell the user they did not have enough money to build the habitat
                return messages = "There was not enough money to build the habitat";
            }
            // decrease the amount of money that the user has by the price of the habitat
            money = money - 2000;
            // loop through the names array
            for (int i = 0; i < names.Length; i++)
            {
                // if the name of the animal already exists
                if (name == names[i])
                {
                    // the messages variable states that the name of the animal already exists
                    messages = "The name of the animal already exists";
                    // return the message that the user the animal already exists
                    return messages;
                }
            }

            // passing values through parameters
            // The ListAdd functions add to the array
            ListAdd(ref names, name);
            ListAdd(ref descriptions, description);
            ListAdd(ref prices, price);
            ListAdd(ref quantities, 0);
            ListAdd(ref food, 0);

            // if all the conditions are met and the animal was purchased,
            // return the name of the habitat that was added
            return "The habitat that was added was " + name;

        }
        // This subprogram allows the user to buy an existing animal. This subprogram returns a string 
        // and takes the double variable ‘moneyAvailable’ and strint ‘name’ as paramaters. 
        static string BuyExistingAnimal(ref double moneyAvailable, string name)
        {
            // declaring an empty string variable
            string note = "";
            // declaring an integer variable to store the the index of name
            int i = ListFindExactIgnoreCase(names, name);
            // if the index is not equal to -1
            if (i != -1)
            {
                // if there was not enough money
                if (moneyAvailable < prices[i])
                {
                    // it will print out that there was not enough money
                    note = "There is not enough money";
                    // returning the note that there was not enough money
                    return note;
                }

                // reduce the user's money by the price of that animal
                moneyAvailable = moneyAvailable - prices[i];
                // increase the quantity of that animal by 1
                quantities[i] = quantities[i] + 1;
                // The variable message states that the animal was bought successfully 
                // and stores the information of the amount of money available
                note = "The animal was bought succesfully! The amount of money you have reamaining is $" + moneyAvailable;
                // returning the note
                return note;
            }
            // if what the user writes does not match the index of the names array
            // if the animal does not exist

            // it will print that the animal does not exist
            note = "The animal does not exist";
            // returning the note
            return note;

        }
        /* This subprogram allows the user to earn money from all the animals
    	in the zoo. This subprogram returns a double */
        static double EarnMoney()
        {
            // declaring a double variable that stores the total money the user has
            double total = 0;
            // loop through the names array (for each animal type)
            for (int i = 0; i < names.Length; i++)
            {
                // using random numbers to choose a random price
                total = total + generator.NextDouble() * quantities[i] * prices[i] / 50.0;
            }
            // returning the total
            return total;
        }
        // This subprogram counts the number of days, it returns a string, and passes the values money and dayCount by reference
        // menu option 0
        static string EndTurn(ref double money, ref int dayCount)
        {
            // This string variable stores the message
            string startMessage;
            // increase the day counter by 1
            dayCount = dayCount + 1;
            // add money to the money earned
            money = money + EarnMoney();
            // The message is the amount of days the zoo has been open, if all the animals are or if
            // some died of starvation (AnimalsEat), the types of animals that were born (AnimalLove),
            // and how much money the user has at the start of the next day (EarnMoney)
            startMessage = "The zoo has been open for " + (dayCount - 1) + " days. " + "\n" + AnimalsEat() + "\n" + AnimalLove() + "\nYou now have $" + money;
            // returning the message (string variable) to the user
            return startMessage;
        }
        // This subprogram makes sure the user does not input an empty string, it returns a string 
        // and passes the string variable ‘prompt’ as parameters. 
        static string InputNotEmpty(string prompt)
        {
            // telling the user to write an input
            Console.WriteLine(prompt);
            // store the user input
            string userInput = Console.ReadLine();
            // make sure the username is not nothing/empty
            // when you use a local variable, assign it at the same time
            while (userInput == "")
            {
                // write the prompt
                Console.WriteLine(prompt);
                // store the user’s input
                userInput = Console.ReadLine();
            }
            // return the user's input
            return userInput;
        }
        // This subprogram allows the user to input a number
        static double InputNumber(int minimum, int maximum)
        {
            // creating a double variable to store the price
            double price;
            // loop through the prices array
            do
            {
                // the program tells the user to input a number between 50 and 500
                // the user assigns price a value
                Console.WriteLine("Input a number between 50 and 500: ");
                // data type conversion from a string to a double
                double.TryParse(Console.ReadLine(), out price);

                // while the price is less than or equal to or greater than or equal to
            } while (price <= minimum || price >= maximum);
            // return the price
            return price;
        }

        // This subprogram allows zookeepers to search for animals by their descriptions, returns a string array with
        // each animal's name, description, price, quantity, and food if they match the search criteria
        // This subprogram takes 'toFind' as a parameter, which is what is to be found in the descriptions.
        static string[] SearchByDescription(string toFind)
        {
            // pass information for 'descriptions' and the user input in parameter
            int[] array = ListFindPartial(descriptions, toFind);
            // declare a new array with he same length as 'array' to store the data as a result of the search
            string[] searchData = new string[array.Length];
            // loop through the array
            for (int i = 0; i < array.Length; i++)
            {
                // make the search data array equal to the index that animal data was found it
                searchData[i] = FormatAnimalData(array[i]);
            }
            // rreturn the data searched
            return searchData;
        }


        // This subprogram sorts prices in descending order and returns a string array
        static string[] ShowDescendingByPrice()
        {
            // creating new arrays and making them equal to the length of the array
            string[] sNames = new string[names.Length];
            string[] sDescriptions = new string[descriptions.Length];
            double[] sPrices = new double[prices.Length];
            int[] sQuantities = new int[quantities.Length];
            int[] sFood = new int[food.Length];

            // loop through the names array
            for (int i = 0; i < names.Length; i++)
            {
                // copy the information to the new arrays
                sNames[i] = names[i];
                sDescriptions[i] = descriptions[i];
                sPrices[i] = prices[i];
                sQuantities[i] = quantities[i];
                sFood[i] = food[i];
            }
            // make the sortedData array equal to the length of the names array (new string array)
            string[] sortedData = new string[names.Length];
            // repeatedly uses the loop inside -- this means it repeatedly moves
            // the next smallest value to the end of the array
            for (int k = 0; k < sPrices.Length; k++)
            {
                // swap the smallest item all the way to the end of the array
                // each time that it loops we stop looping one element sooner.
                for (int i = 0; i < sPrices.Length - 1 - k; i++)
                {
                    // compare to side-by-side values -- if the right one is bigger
                    // then it should be swapped with the left one
                    if (sPrices[i] < sPrices[i + 1])
                    {
                        // make a copy of each array so the original array does not chnagw
                        // temporarily store the left value
                        string temp = sNames[i];
                        // move the left element into the right element
                        sNames[i] = sNames[i + 1];
                        // move the right value into the left element
                        sNames[i + 1] = temp;
                        // temporarily store the left value
                        temp = sDescriptions[i];
                        // move the left element into the right element
                        sDescriptions[i] = sDescriptions[i + 1];
                        // move the right value into the left element
                        sDescriptions[i + 1] = temp;
                        // temporarily store the left value
                        double temp2 = sPrices[i];
                        // move the left element into the right element
                        sPrices[i] = sPrices[i + 1];
                        // move the right value into the left element
                        sPrices[i + 1] = temp2;
                        // temporarily store the left value
                        int temp3 = sQuantities[i];
                        // move the left element into the right element
                        sQuantities[i] = sQuantities[i + 1];
                        // move the right value into the left element
                        sQuantities[i + 1] = temp3;
                        // temporarily store the left value
                        temp3 = sFood[i];
                        // move the left element into the right element
                        sFood[i] = sFood[i + 1];
                        // move the right value into the left element
                        sFood[i + 1] = temp3;
                    }
                }
            }
            // loop through the array
            for (int i = 0; i < names.Length; i++)
            {
                // local variable to store the sorting information
                sortedData[i] = FormatAnimalData(i, sNames, sDescriptions, sPrices, sQuantities, sFood);
            }
            // return the prices in descending order
            return sortedData;

        }

        // This subprogram will increase the array size preventing any “index out of range” issues. 
        // This subprogram returns nothing and passes the ‘data’ array by reference.
        static void IncreaseArraySize(ref double[] data)
        {
            // Create a copy of the original array, but 1 size bigger
            double[] copy = new double[data.Length + 1];
            // transfer all data from the original array into the copy
            // This is the "ARRAY COPY' algorithm
            // Loop through the array
            for (int i = 0; i < data.Length; i++)
            {
                // make the indexes of the coy array and the data array equal to each other
                copy[i] = data[i];
            }
            // make ‘data’ equal to ‘copy’
            data = copy;
        }

        // This subprogram increases the array size, returns nothing, and passes the ‘data’ array by reference
        static void IncreaseArraySize(ref int[] data)
        {
            // Create a copy of the original array, but 1 size bigger
            int[] copy = new int[data.Length + 1];
            // Loop through the array
            for (int i = 0; i < data.Length; i++)
            {
                // copy the information into the new array
                // transfer all data from the original array into the copy
                copy[i] = data[i];
            }
            // make ‘data’ equal to ‘copy’
            data = copy;
        }

        // This subprogram does not return anything and passes the ‘data’ array in parameters.
        static void IncreaseArraySize(ref string[] data)
        {
            //  Create a copy of the original array, but 1 size bigger
            string[] copy = new string[data.Length + 1];
            // This is the "ARRAY COPY' algorithm
            for (int i = 0; i < data.Length; i++)
            {
                // copy the information into the new array
                // transfer all data from the original array into the copy
                copy[i] = data[i];
            }
            // make ‘data’ equal to ‘copy’
            data = copy;
        }
        // This subprogram adds to the array, returns nothing and passes the double ‘array’ by reference. 
        // This subprogram also takes toAdd as parameters
        static void ListAdd(ref double[] array, double toAdd)
        {
            // increasing the array size
            IncreaseArraySize(ref array);
            // adds to the end of the array
            array[array.Length - 1] = toAdd;
        }
        // this subprogram adds to the array, returns nothing and passes the int ‘array’ by reference
        // This subprogram takes the integer toAdd as parameters
        static void ListAdd(ref int[] array, int toAdd)
        {
            // increase the array size
            IncreaseArraySize(ref array);
            // adds to the end of the array
            array[array.Length - 1] = toAdd;
        }
        // this subprogram adds to the array, returns nothing and passes the string ‘array’ by reference
        static void ListAdd(ref string[] array, string toAdd)
        {
            // increase the array size
            IncreaseArraySize(ref array);
            // adds to the end of the array
            array[array.Length - 1] = toAdd;
        }
        // This subprogram allows the user to do a partial search, returns an int array, 
        // and takes the string array and the string toFind variable as parameters. 
        static int[] ListFindPartial(string[] array, string toFind)
        {
            // declare the array
            int[] found = new int[0];
            // loop through the array
            for (int i = 0; i < array.Length; i++)
            {
                // if found add to the array
                if (array[i].Contains(toFind))
                {
                    // adds to the found array
                    ListAdd(ref found, i);
                }
            }
            // return the array found
            return found;
        }

        /************************ NARGES END *************************/
    }

}


