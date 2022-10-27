Random random = new Random();

//Array initialization
string[,] array = new string[3, 3] { 
                                   { "1", "2", "3" }, 
                                   { "4", "5", "6" }, 
                                   { "7", "8", "9" } 
                                   };
    
//Setting the zero coordinates of console
int positionX = 40; int positionY = 5; 
Console.Clear();

#region Game
//Looped execution
do
{
    //Displaying the table
    PrintArray(array);

    //Player move
    ChangeArray(array, PlayerMove(), "X");

    PrintArray(array);

    //Check for win by the player
    if (Verify(array, "X")) { Win("Player"); break; }

    //Computer move
    if (!VerifyTable(array)) ChangeArray(array, ComputerMove(array), "O");

    PrintArray(array);

    //Check for win by the computer
    if (Verify(array, "O")) { Win("Computer"); break; }
    
    //Game continuation check
    if (VerifyTable(array)) break;

    //Checking for a win or a limit of moves
} while (!Verify(array, "X") || !Verify(array, "O") || !VerifyTable(array));
#endregion

#region Methodes
/// <summary>
/// Cell player selection
/// </summary>
/// <remarks>
/// The player selects the cell where he wants to set his flag. 
/// If the cell is already includes "X" or "O", a message is displayed about the incorrect cell selection. 
/// The method is executed until a positive selection.
/// </remarks>
/// <returns>Number of sell</returns>
int PlayerMove()
{
    int choise = 0;
    bool done = false;

    do
    {
        Console.SetCursorPosition(positionX - 5, positionY + 15);
        Console.Write("                                          ");
        Console.SetCursorPosition(positionX - 5, positionY + 15); ColorLight();
        Console.Write("Enter number of field (1-9): ");
        string str = Console.ReadLine();

        //If the entered value is parsed and is between 1 and 9
        if (str != null && double.TryParse(str, out _) && int.Parse(str) > 0 && int.Parse(str) < 10)
        {
            choise = int.Parse(str);

            //Cell validation
            if (VerifyXO(choise))
                return choise;
            else 
            {
                //Show invalid move message
                WrongChoise();

                //Removing a string from a user-selection
                Console.SetCursorPosition(positionX - 5, positionY + 15);
                Console.Write("                                          ");

                //Removing a string from a wrong message
                Console.SetCursorPosition(positionX - 5, positionY + 16);
                Console.Write("                                          ");
                
                //Returned Cursor position
                Console.SetCursorPosition(positionX - 5, positionY + 15);

                done = false;
            }
        }
        else //Show invalid move message
        {
            WrongChoise();
            Console.SetCursorPosition(positionX - 5, positionY + 16);
            Console.Write("                                          ");

            Console.SetCursorPosition(positionX - 5, positionY + 15);
            Console.Write("                                          ");
            Console.SetCursorPosition(positionX - 5, positionY + 15);

            done = false; 
        }
    } while (!done);
    
    return choise;
}

/// <summary>
/// Checking a cell for a move
/// </summary>
/// <remarks>
/// If the cell already contains "X" or "O", the check fails.
/// If the cell does not include X or O, then the selected character is assigned to the cell value
/// </remarks>
/// <returns>The test result</returns>
bool VerifyXO(int field)
{
    bool verufy = false;

    switch (field)
    {
        case 1:
            if (array[0, 0] != "X" && array[0, 0] != "O") { array[0, 0] = "X"; verufy = true; break; }
            break;
        case 2:
            if (array[0, 1] != "X" && array[0, 1] != "O") { array[0, 1] = "X"; verufy = true; break; }
            break;
        case 3:
            if (array[0, 2] != "X" && array[0, 2] != "O") { array[0, 2] = "X"; verufy = true; break; }
            break;
        case 4:
            if (array[1, 0] != "X" && array[1, 0] != "O") { array[1, 0] = "X"; verufy = true; break; }
            break;
        case 5:
            if (array[1, 1] != "X" && array[1, 1] != "O") { array[1, 1] = "X"; verufy = true; break; }
            break;
        case 6:
            if (array[1, 2] != "X" && array[1, 2] != "O") { array[1, 2] = "X"; verufy = true; break; }
            break;
        case 7:
            if (array[2, 0] != "X" && array[2, 0] != "O") { array[2, 0] = "X"; verufy = true; break; }
            break;
        case 8:
            if (array[2, 1] != "X" && array[2, 1] != "O") { array[2, 1] = "X"; verufy = true; break; }
            break;
        case 9:
            if (array[2, 2] != "X" && array[2, 2] != "O") { array[2, 2] = "X"; verufy = true; break; }
            break;
        default:
            verufy = false;
            break;
    }

    return verufy;
}

/// <summary>
/// Display wrong message
/// </summary>
/// <remarks>
/// Just displays an error message
/// </remarks>
void WrongChoise()
{
    Console.SetCursorPosition(positionX - 5, positionY + 16);
    Console.BackgroundColor = ConsoleColor.Red;
    Console.WriteLine("Enter invalid number");
    Console.ResetColor();
    Thread.Sleep(200);
    Console.SetCursorPosition(positionX - 5, positionY + 16);
    Console.WriteLine("Enter invalid number");
    Console.SetCursorPosition(positionX - 5, positionY + 15);
    Console.Write("                                          ");
}

/// <summary>
/// Computer move
/// </summary>
/// <remarks>
/// A cell from 1 to 9 is randomly selected. If the cell is occupied, 
/// then the random selection is repeated. Until a positive result.
/// </remarks>
/// <returns>The number of the cell</returns>
int ComputerMove(string[,] array)
{
    bool done = false;
    int choise;

    do
    {
        choise = random.Next(1, 9);
        if (VerifyXO(choise))
            return choise;

    } while (!done);

    return choise;
}

/// <summary>
/// Display of the game to the console
/// </summary>
/// <remarks>
/// Displaying an array according to the contents of the cell
/// </remarks>
void PrintArray(string[,] array)
{
    int curX = positionX;
    int curY = positionY;

    #region Building a table
    Console.ForegroundColor = ConsoleColor.Green;
    for (int j = 0; j < 3; j++)
    {
        curY++;
        Console.SetCursorPosition(curX, curY); Console.Write("+-----+-----+-----+");
        for (int i = 0; i < 3; i++)
        {
            curY++;
            Console.SetCursorPosition(curX, curY);
            Console.Write("|     |     |     |");
        }
    }
    Console.SetCursorPosition(curX, curY++); Console.Write("|     |     |     |");
    Console.SetCursorPosition(curX, curY++); Console.Write("+-----+-----+-----+");
    Console.ResetColor();
    #endregion

    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            curX = positionX + 3;
            curY = positionY + 3;
            curX += 6*j;
            curY += 4*i;

            //If the data is not "X" or "O", then the value of the array cell is displayed in dark color
            if (array[i, j] != "X" && array[i, j] != "O")
            {
                Console.SetCursorPosition(curX, curY);
                ColorDark();
                Console.WriteLine(array[i, j]);
            }
            else //Otherwise, the "X" or "O" value is displayed in bright color.
            {
                Console.SetCursorPosition(curX, curY);
                ColorLight();
                Console.WriteLine(array[i, j]);
            }
        }
    }
}

/// <summary>
/// Set a dark font
/// </summary>
void ColorDark() => Console.ForegroundColor = ConsoleColor.DarkGray;

/// <summary>
/// Set a light font
/// </summary>
void ColorLight() => Console.ForegroundColor = ConsoleColor.White;

/// <summary>
/// Replacing the contents of the cell with the flag of the player
/// </summary>
/// <remarks>
/// Depending on which player is currently walking, the contents of the cell are replaced with "X" or "O"
/// </remarks>
/// <param name="move">The selected cell</param>
/// <param name="symbol">The selected flag</param>
void ChangeArray(string[,] array, int move, string symbol)
{
    switch (move)
    {
        case 1:
            array[0, 0] = symbol;
            break;
        case 2:
            array[0, 1] = symbol;
            break;
        case 3:
            array[0, 2] = symbol;
            break;
        case 4:
            array[1, 0] = symbol;
            break;
        case 5:
            array[1, 1] = symbol;
            break;
        case 6:
            array[1, 2] = symbol;
            break;
        case 7:
            array[2, 0] = symbol;
            break;
        case 8:
            array[2, 1] = symbol;
            break;
        case 9:
            array[2, 2] = symbol;
            break;

        default:
            break;
    }
}

/// <summary>
/// Checking the gaming table for winning conditions
/// </summary>
/// <remarks>
/// The method step by step checks all horizontal, vertical 
/// and diagonal lines for compliance with the selected checkbox.
/// </remarks>
/// <returns>If there is a match, return true, otherwise false</returns>
bool Verify(string[,] array, string symbol)
{
    //Checking horizontal lines
    for (int i = 0; i < array.GetLength(0); i++)
        if (array[i, 0] == symbol && array[i, 1] == symbol && array[i, 2] == symbol) 
            return true;

    //Cheking vertikal lines
    for (int i = 0; i < array.GetLength(0); i++)
        if (array[0, i] == symbol && array[1, i] == symbol && array[2, i] == symbol)
            return true;

    //Cheking 1st diagonal line
    if (array[0,0] == symbol && array[1,1] ==symbol && array[2,2]==symbol)
        return true;

    //Cheking 2st diagonal line
    if (array[2, 0] == symbol && array[1, 1] == symbol && array[0, 2] == symbol)
        return true;

    return false;
}

/// <summary>
/// Shows win text
/// </summary>
/// <remarks>
/// Depending on the player's name, shows the text about the player's victory
/// </remarks>
/// <param name="player">The selected player</param>
void Win(string player)
{
    Console.SetCursorPosition(positionX, positionY);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("*** " + player + " Win! ***");
    Console.ResetColor();
    Console.SetCursorPosition(0, 23);
    Console.ReadKey();
}

/// <summary>
/// Checking the array for the possibility of continuing the game
/// </summary>
/// <remarks>
/// By counting the cells of the array without flags, it is concluded that the game can be continued.
/// If there are no empty cells left, then a message about the absence of a winner is displayed
/// </remarks>
/// <returns>Test result</returns>
bool VerifyTable(string[,] array)
{
    int count = 0;
    for (int i = 0; i < array.GetLength(0); i++)
        for (int j = 0; j < array.GetLength(1); j++)
            if (array[i, j] == "X" || array[i, j] == "O")
                count++;

    if (count == 9)
    {
        Console.SetCursorPosition(positionX, positionY);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("*** No winner! ***");
        Console.ResetColor();
        Console.SetCursorPosition(0, 23);
        Console.ReadKey();
        return true;
    }
    else return false;
}
#endregion