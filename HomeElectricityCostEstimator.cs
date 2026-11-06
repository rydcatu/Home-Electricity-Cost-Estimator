using System;

class Program
{
    // Student: Catu, Ryan David C.
    // Section: BSIT-IT301-3B
    // Date: 2026-10-17
    // Project: Home Electricity Cost Estimator
    // This program estimates the electricity cost of home appliances.

    // Requirement 1: Constants, variables, and data types
    // This constant sets the maximum number of appliances.
    const int MaxAppliances = 20;

    // Requirement 2: Arrays
    // These arrays store appliance names, wattage, and daily usage hours.
    // The same index belongs to the same appliance.
    static string[] names = new string[MaxAppliances];
    static int[] watts = new int[MaxAppliances];
    static double[] hours = new double[MaxAppliances];

    // These variables store the appliance count and billing information.
    static int count = 0;
    static int billingDays = 0;
    static double rate = 12.1188;
    static double budget = 8000.00;

    // This variable controls when the menu should stop.
    static bool exit = false;

    static void Main()
    {
        // Requirement 7: Exception handling
        // The try block contains the main program operations.
        try
        {
            Console.WriteLine("==================================");
            Console.WriteLine(" HOME ELECTRICITY COST ESTIMATOR");
            Console.WriteLine("==================================");
           
            // Gets a valid number of billing days from the user.
            billingDays = ReadIntInRange(
                "Enter billing days (1-31): ", 1, 31);

            // Requirement 3: Loops and selection statements
            // The do-while loop keeps showing the menu until Exit is selected.
            do
            {
                Console.WriteLine("\n========== MAIN MENU ==========");
                Console.WriteLine("1. Add Appliance");
                Console.WriteLine("2. View Appliances");
                Console.WriteLine("3. Calculate Electricity Cost");
                Console.WriteLine("4. Search Appliance");
                Console.WriteLine("5. Update Appliance");
                Console.WriteLine("6. Remove Appliance");
                Console.WriteLine("7. Find Highest Cost");
                Console.WriteLine("8. Update Electricity Rate");
                Console.WriteLine("9. Update Monthly Budget");
                Console.WriteLine("10. Exit");

                int choice = ReadIntInRange(
                    "Choose an option (1-10): ", 1, 10);

                // The switch statement runs the selected menu option.
                switch (choice)
                {
                    case 1:
                        AddAppliance();
                        break;

                    case 2:
                        ViewAppliances();
                        break;

                    case 3:
                        CalculateCost();
                        break;

                    case 4: // Menu 4: Search Appliance
                    case 5: // Menu 5: Update Appliance
                    case 6: // Menu 6: Remove Appliance
                    case 7: // Menu 7: Find Highest Cost
                    case 8: // Menu 8: Update Electricity Rate
                    case 9: // Menu 9: Update Monthly Budget
                        // Note: Menu options 4 to 9 are still in progress
                        // and need more code to make them work properly.
                        try
                        {
                            throw new NotImplementedException();
                        }
                        catch (NotImplementedException ex)
                        {
                            Console.WriteLine(
                                "Unexpected error: " + ex.Message);
                        }
                        break;

                    case 10:
                        exit = true;
                        Console.WriteLine(
                            "Thank you for using the program!");
                        break;

                    default:
                        Console.WriteLine(
                            "Please choose from 1 to 10.");
                        break;
                }

            } while (!exit);
        }
        // Handles a number that is too large or too small.
        catch (OverflowException)
        {
            Console.WriteLine(
                "Error: A number is too large or too small.");
        }
        // Handles an invalid array index.
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine(
                "Error: An array index is outside the valid range.");
        }
        // Handles other exceptions not caught above.
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
        // Runs when the try-catch section finishes.
        finally
        {
            Console.WriteLine("\nProgram session ended.");
        }
    }

    // Requirement 6: Input validation
    // Reads the user's input and checks if input is available.
    static string ReadInput(string prompt)
    {
        Console.Write(prompt);

        string input = Console.ReadLine();

        if (input == null)
        {
            throw new InvalidOperationException(
                "No more input is available. Program stopped.");
        }

        return input.Trim();
    }

    // Checks if the input is a whole number within the allowed range.
    // TryParse prevents invalid text from being accepted as a number.
    static int ReadIntInRange(string prompt, int min, int max)
    {
        int value;

        while (true)
        {
            string input = ReadInput(prompt);

            if (int.TryParse(input, out value)
                && value >= min
                && value <= max)
            {
                return value;
            }

            Console.WriteLine(
                "Invalid input. Enter a whole number from "
                + min + " to " + max + ".");
        }
    }

    // Checks if the input is a valid decimal number within the range.
    // It also rejects NaN and Infinity.
    static double ReadDoubleInRange(
        string prompt, double min, double max)
    {
        double value;

        while (true)
        {
            string input = ReadInput(prompt);

            if (double.TryParse(input, out value)
                && !double.IsNaN(value)
                && !double.IsInfinity(value)
                && value >= min
                && value <= max)
            {
                return value;
            }

            Console.WriteLine(
                "Invalid input. Enter a number from "
                + min + " to " + max + ".");
        }
    }

    // Makes sure the user enters a name instead of leaving it blank.
    static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            string input = ReadInput(prompt);

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Console.WriteLine(
                "Input cannot be empty. Please try again.");
        }
    }

    // Allows the user to add an appliance.
    // It validates the input, saves the details in the arrays,
    // and increases the appliance count.
    static void AddAppliance()
    {
        if (count >= MaxAppliances)
        {
            Console.WriteLine(
                "Appliance list is full. Maximum is "
                + MaxAppliances + ".");
            return;
        }

        Console.WriteLine("\n--- ADD APPLIANCE ---");

        string name = ReadNonEmptyString(
            "Enter appliance name: ");

        int wattage = ReadIntInRange(
            "Enter wattage in watts (1-10000): ",
            1, 10000);

        double usageHours = ReadDoubleInRange(
            "Enter hours used per day (0.01-24): ",
            0.01, 24);

        names[count] = name;
        watts[count] = wattage;
        hours[count] = usageHours;

        // Increments the number of saved appliances.
        count++;

        Console.WriteLine("Appliance added successfully!");
    }

    // Displays all saved appliances.
    // The while loop continues until every appliance is displayed.
    static void ViewAppliances()
    {
        Console.WriteLine("\n--- APPLIANCE LIST ---");

        if (count == 0)
        {
            Console.WriteLine("No appliances have been added.");
            return;
        }

        Console.WriteLine(
            "{0,-5} {1,-20} {2,-12} {3,-12}",
            "No.", "Name", "Watts", "Hours/Day");

        Console.WriteLine(
            "------------------------------------------------");

        int remainingAppliances = count;
        int index = 0;

        while (remainingAppliances > 0)
        {
            Console.WriteLine(
                "{0,-5} {1,-20} {2,-12} {3,-12:F2}",
                index + 1,
                names[index],
                watts[index],
                hours[index]);

            index++;
            remainingAppliances--;
        }

        Console.WriteLine("\nTotal appliances: " + count);
    }

    // Requirement 4: Arithmetic operators
    // Calculates the energy consumption and estimated cost
    // of every appliance for the selected billing period.
    static void CalculateCost()
    {
        Console.WriteLine("\n--- ELECTRICITY COST ESTIMATE ---");

        if (count == 0 || billingDays <= 0)
        {
            Console.WriteLine(
                "Add at least one appliance and check the billing days.");
            return;
        }

        double totalKwh = 0;
        double totalCost = 0;

        Console.WriteLine(
            "{0,-18} {1,-12} {2,-14} {3,-12}",
            "Appliance", "Daily kWh", "Monthly kWh", "Cost");

        Console.WriteLine(
            "----------------------------------------------------------");

        for (int i = 0; i < count; i++)
        {
            // Converts watts into kilowatts and calculates daily usage.
            double dailyKwh =
                (watts[i] * hours[i]) / 1000.0;

            // Calculates the energy consumption for the billing period.
            double monthlyKwh = dailyKwh;
            monthlyKwh *= billingDays;

            // Calculates the estimated cost using the electricity rate.
            double cost = monthlyKwh * rate;

            Console.WriteLine(
                "{0,-18} {1,-12:F2} {2,-14:F2} PHP {3:F2}",
                names[i],
                dailyKwh,
                monthlyKwh,
                cost);

            // Accumulates the monthly usage and cost.
            totalKwh += monthlyKwh;
            totalCost += cost;
        }

        Console.WriteLine(
            "----------------------------------------------------------");

        Console.WriteLine("Billing days: " + billingDays);

        Console.WriteLine(
            "Electricity rate: PHP " + rate.ToString("F4")
            + " per kWh");

        Console.WriteLine(
            "Total electricity usage: "
            + totalKwh.ToString("F2") + " kWh");

        Console.WriteLine(
            "Estimated total cost: PHP "
            + totalCost.ToString("F2"));

        // Deducts the estimated cost from the budget.
        double remainingBudget = budget;
        remainingBudget -= totalCost;

        if (totalCost > budget)
        {
            double overBudget = totalCost - budget;

            Console.WriteLine(
                "Status: OVER BUDGET by PHP "
                + overBudget.ToString("F2"));
        }
        else if (totalCost == budget)
        {
            Console.WriteLine(
                "Status: Your estimated cost equals your budget.");
        }
        else
        {
            Console.WriteLine("Status: WITHIN BUDGET.");

            Console.WriteLine(
                "Remaining budget: PHP "
                + remainingBudget.ToString("F2"));
        }

        // Calculates the average cost per billing day.
        double averageDailyCost = totalCost;
        averageDailyCost /= billingDays;

        Console.WriteLine(
            "Average daily cost: PHP "
            + averageDailyCost.ToString("F2"));

        // Calculates the days left after full weeks.
        int remainingDays = billingDays % 7;

        Console.WriteLine(
            "Extra days after full weeks: " + remainingDays);

        // Relational and logical operators check calculation results.
        if (count > 0 && billingDays >= 7)
        {
            Console.WriteLine(
                "A valid appliance list and billing period were used.");
        }

        if (totalCost < 0 || totalKwh < 0)
        {
            Console.WriteLine(
                "Warning: Check the calculated values.");
        }

        if (count != 0 && !(totalCost < 0))
        {
            Console.WriteLine("Calculation completed.");
        }
    }
    
}






/*
========================================================
       HOME ELECTRICITY COST ESTIMATOR
                  TEST-CASE LOG
========================================================

Name: Catu, Ryan David C.
Section: BSIT-IT301-3B
Date: October 17, 2026

TEST CASE 1: Valid Billing Days
Input: 31
Expected Result: Accepts 31 billing days.
Actual Result: PASSED.
==================================
 HOME ELECTRICITY COST ESTIMATOR
==================================
Enter billing days (1-31): 31

TEST CASE 2: Invalid Billing Days
Input: abc, 0, 32, then 30
Expected Result: Rejects invalid values and asks again.
Actual Result: PASSED.
==================================
 HOME ELECTRICITY COST ESTIMATOR
==================================
Enter billing days (1-31): abc
Invalid input. Enter a whole number from 1 to 31.
Enter billing days (1-31): 0
Invalid input. Enter a whole number from 1 to 31.
Enter billing days (1-31): 32
Invalid input. Enter a whole number from 1 to 31.
Enter billing days (1-31): 30

========== MAIN MENU ==========
1. Add Appliance
2. View Appliances
3. Calculate Electricity Cost
4. Search Appliance
5. Update Appliance
6. Remove Appliance
7. Find Highest Cost
8. Update Electricity Rate
9. Update Monthly Budget
10. Exit
Choose an option (1-10): 

TEST CASE 3: Add an Appliance
Input:
Name: Electric Fan
Wattage: 75
Hours per Day: 8
Expected Result: Saves the appliance information.
Actual Result: PASSED.
--- ADD APPLIANCE ---
Enter appliance name: Electric Fan
Enter wattage in watts (1-10000): 75
Enter hours used per day (0.01-24): 8
Appliance added successfully!

TEST CASE 4: Invalid Appliance Input
Input: Blank name, 0 watts, 10001 watts, or 25 hours.
Expected Result: Rejects invalid values and asks again.
Actual Result: PASSED.
--- ADD APPLIANCE ---
Enter appliance name: 
Input cannot be empty. Please try again.
Enter appliance name: Aircon
Enter wattage in watts (1-10000): 10001
Invalid input. Enter a whole number from 1 to 10000.
Enter wattage in watts (1-10000): 0   
Invalid input. Enter a whole number from 1 to 10000.
Enter wattage in watts (1-10000): 10000
Enter hours used per day (0.01-24): 25
Invalid input. Enter a number from 0.01 to 24.
Enter hours used per day (0.01-24): 24
Appliance added successfully!


TEST CASE 5: View Appliances
Action: Select menu option 2.
Expected Result: Displays saved appliance records.
Actual Result: PASSED.
--- APPLIANCE LIST ---
No.   Name                 Watts        Hours/Day   
------------------------------------------------
1     Electric Fan         75           8.00        
2     Aircon               10000        24.00  

TEST CASE 6: Calculate Electricity Cost
Input:
Wattage: 75 watts
Hours per Day: 8
Billing Days: 30
Electricity Rate: PHP 12.1188 per kWh

Expected Result:
Daily Consumption: 0.6 kWh
Monthly Consumption: 18 kWh
Estimated Cost: PHP 218.14

Actual Result: PASSED.
--- ELECTRICITY COST ESTIMATE ---
Appliance          Daily kWh    Monthly kWh    Cost        
----------------------------------------------------------
Electric Fan       0.60         18.00          PHP 218.14
----------------------------------------------------------
Billing days: 30
Electricity rate: PHP 12.1188 per kWh
Total electricity usage: 18.00 kWh
Estimated total cost: PHP 218.14
Status: WITHIN BUDGET.
Remaining budget: PHP 7781.86
Average daily cost: PHP 7.27
Extra days after full weeks: 2
A valid appliance list and billing period were used.
Calculation completed.

TEST CASE 7: Invalid Menu Selection
Input: abc or a number outside 1-10.
Expected Result: Rejects invalid menu choices.
Actual Result: PASSED.
Choose an option (1-10): abc
Invalid input. Enter a whole number from 1 to 10.
Choose an option (1-10): 11
Invalid input. Enter a whole number from 1 to 10.
Choose an option (1-10): 

TEST CASE 8: Exception Handling
Action: Test an appropriate exception using a
controlled test case.
Expected Result: The appropriate catch block handles
the exception and displays an error message.
Actual Result: PASSED.
==================================
 HOME ELECTRICITY COST ESTIMATOR
==================================
Unexpected error: Test exception handling

Program session ended.
PS E:\Code Files\IT301\C#> 

TEST CASE 9: Exit Program
Action: Select menu option 10.
Expected Result: Ends the menu loop and displays
"Program session ended."
Actual PASSED.
========== MAIN MENU ==========
1. Add Appliance
2. View Appliances
3. Calculate Electricity Cost
4. Search Appliance
5. Update Appliance
6. Remove Appliance
7. Find Highest Cost
8. Update Electricity Rate
9. Update Monthly Budget
10. Exit
Choose an option (1-10): 10
Thank you for using the program!

Program session ended.

========================================================
TESTING REMINDER:
Update the actual results and status after running
each test. Mark a test as Passed only if the observed
result matches the expected result.

// Note: Menu options 4 to 9 are not implemented.
// and not the priority for the Activity
// just for extra temporary menu list that can implement soon as the project approved.
========================================================
*/