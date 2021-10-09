using System;
using System.Collections.Generic;

namespace KhylsWanderingRobot
{

    class Program
    {
        
        static void Main()
        {
            Console.WriteLine("To use this application type out commands sequentially in appropriate order");
            Console.WriteLine("Commands are as follows:");
            Console.WriteLine("PLACE X Y DIRECTION - PLACE will initiate place a robot. X and Y are the coordinates they will be place. DIRECTION should be one of either NORTH, SOUTH, EAST, WEST");
            Console.WriteLine("MOVE - will move the selected robot forwards once in its current direction");
            Console.WriteLine("LEFT - will turn the robot left");
            Console.WriteLine("RIGHT - will turn the robot right");
            Console.WriteLine("REPORT - will read out amount of robots aswell as each robot's name, location and direction");
            Console.WriteLine("ROBOT X - ROBOT will initiate selecting a robot. X will be the number of a which robot you want selected");
            Console.WriteLine("If a robot tries to move either outside of the 5x5 area or into another robot the command will be ignored");
            
            //Instatntiating list of robots
            List<Robot> robotList = new List<Robot>();
            List<String> instructionList;
            string newCurrentLine;
            
            newCurrentLine = Console.ReadLine();
            if (newCurrentLine.Length > 0)
            {
                instructionList = TakeInstructions(newCurrentLine);
                CommitInstructions(robotList, instructionList);
            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }

        static List<Robot> CommitInstructions(List<Robot> robotList, List<String> instructionList)
        {
            int robotCount = 0;
            int selectedRobot = 0;
            int xPlace, yPlace;
            bool placeFound = false;
            for (int i = 0; i < instructionList.Count; i++)
            {
                //Check to see if a robot has been place
                if (placeFound != true)
                {
                    //Check if following 2 instructions are x and y
                    if (instructionList[i] == "PLACE" && int.TryParse(instructionList[i+1], out xPlace) && int.TryParse(instructionList[i+2], out yPlace))
                    {
                        //Check to see if provided X and Y is a valid location on table
                        if (xPlace <= 4 && xPlace >= 0 && yPlace <= 4 && yPlace >= 0)
                        {
                            //check if 4th instruction is a direction
                            if (instructionList[i + 3] == "NORTH" || instructionList[i + 3] == "SOUTH" || instructionList[i + 3] == "EAST" || instructionList[i + 3] == "WEST")
                            {
                                robotCount += 1;
                                placeFound = true;
                                robotList.Add(CreateNewRobot(robotCount, xPlace, yPlace, instructionList[i + 3]));
                                i += 3;
                            } 
                        }
                    }
                }
                else
                {
                    if (instructionList[i] == "PLACE" && int.TryParse(instructionList[i + 1], out xPlace) && int.TryParse(instructionList[i + 2], out yPlace))
                    {
                        //Check to see if provided X and Y is a valid location on table
                        if (xPlace <= 4 && xPlace >= 0 && yPlace <= 4 && yPlace >= 0)
                        {
                            bool willCollide = false;
                            foreach (Robot tempRobot in robotList)
                            {
                                //Check if placing bot will clash with other bpt
                                if (xPlace == tempRobot.x && yPlace == tempRobot.y)
                                {
                                    willCollide = true;
                                }
                            }
                            //check if 4th instruction is a direction
                            if (instructionList[i + 3] == "NORTH" || instructionList[i + 3] == "SOUTH" || instructionList[i + 3] == "EAST" || instructionList[i + 3] == "WEST" && willCollide == false)
                            {
                                robotCount += 1;
                                placeFound = true;
                                robotList.Add(CreateNewRobot(robotCount, xPlace, yPlace, instructionList[i + 3]));
                                i += 3;
                            }
                        }
                    }
                    else if (instructionList[i] == "MOVE")
                    {
                        robotList[selectedRobot].RobotMove(robotList);
                    }

                    else if (instructionList[i] == "LEFT" || instructionList[i] == "RIGHT")
                    {
                        robotList[selectedRobot].RobotTurn(instructionList[i]);
                    }

                    else if (instructionList[i] == "REPORT")
                    {
                        ReportLocations(robotList);
                    }
                    else if (instructionList[i] == "ROBOT")
                    {
                        if (int.Parse(instructionList[i + 1]) <= robotList.Count && int.Parse(instructionList[i + 1]) >= 0)
                            {
                            int.TryParse(instructionList[i + 1], out selectedRobot);
                            selectedRobot -= 1;
                        }
                        i ++;

                    }
                }
                
            }
            return robotList;
        }

        //Takes input from consoles and turns each word into a seperate string in a list
        static List<string> TakeInstructions(string instructions)
        {            
            List<string> instructionList = new List<string>();
            string currentInstruction = "";

            foreach (char letter in instructions)
            {
                if (Char.IsWhiteSpace(letter) == false)
                {
                    currentInstruction += letter;
                }
                else if (currentInstruction.Length > 0)
                {
                    instructionList.Add(currentInstruction);
                    currentInstruction = "";
                }
            }
            if (currentInstruction.Length > 0)
            {
                instructionList.Add(currentInstruction);
            }
            return instructionList;
        }

        //Creates a new robot to be put into the list.
        static Robot CreateNewRobot(int robotCount, int x, int y, string direction)
        {
            Robot newRobot = new Robot();
            newRobot.PlaceRobot(robotCount, x, y, direction);
            return newRobot;
        }
        //Reports the number of robots and each robot's name, x, y, and direction when called
        static void ReportLocations(List<Robot> robotList)
        {
            Console.WriteLine("There are " + robotList.Count.ToString() + "robots");
            foreach (Robot robot in robotList)
            {
                
                Console.WriteLine(robot.name + "'s x is: " + robot.x);

                Console.WriteLine(robot.name + "'s y is: " + robot.y);

                Console.WriteLine(robot.name + " is facing " + robot.direction);
            }
        }

    }

    //Robot Class
    public class Robot
    {
        public string name, direction;
        public int x, y;

        //Sets the robots position
        public void PlaceRobot(int robotAmount,  int newX, int newY, string newDirection)
        {
            name = "robot " + robotAmount.ToString();
            x = newX;
            y = newY;
            direction = newDirection;
        }


        //Makes the robot move if it is able and not blocked
        public void RobotMove(List<Robot> robotList)
        {
            switch (direction)
            {
                case "NORTH":
                    foreach(Robot tempRobot in robotList)
                    {
                        if (tempRobot.x == x && tempRobot.y == y+1 || y + 1 > 4)
                        {
                            goto End;   
                        }
                    }
                    y++;
                    break; 

                case "EAST":
                    foreach (Robot tempRobot in robotList)
                    {
                        if (tempRobot.x == x + 1 && tempRobot.y == y || x + 1 > 4)
                        {
                            goto End;
                        }
                    }
                    x++;
                    break;

                case "SOUTH":
                    foreach (Robot tempRobot in robotList)
                    {
                        if (tempRobot.x == x && tempRobot.y == y - 1 || y - 1 < 0)
                        {
                            goto End;
                        }
                    }
                    y--;
                    break;

                case "WEST":
                    foreach (Robot tempRobot in robotList)
                    {
                        if (tempRobot.x - 1 == x && tempRobot.y == y || x - 1 < 0)
                        {
                            goto End;
                        }
                    }
                    x--;
                    End:
                    break;
            }
                
        }

        //Turns the robot in the appropriate direction
        public void RobotTurn(string turningDirection)
        {
            if (turningDirection == "LEFT")
            {
                switch (direction)
                {
                    case "NORTH":
                        direction = "WEST";
                        break;
                    case "EAST":
                        direction = "NORTH";
                        break;
                    case "SOUTH":
                        direction = "EAST";
                        break;
                    case "WEST":
                        direction = "SOUTH";
                        break;
                }
            }
            else if (turningDirection == "RIGHT")
                switch (direction)
                {
                    case "NORTH":
                        direction = "EAST";
                        break;
                    case "EAST":
                        direction = "SOUTH";
                        break;
                    case "SOUTH":
                        direction = "WEST";
                        break;
                    case "WEST":
                        direction = "NORTH";
                        break;
                }
        }

    }

}
