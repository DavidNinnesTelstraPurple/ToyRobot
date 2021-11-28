using System;
using System.Text.RegularExpressions;

namespace ToyRobot
{
    ///<summary>
    /// The direction the robot is facing. Initialised to none.
    ///</summary> 
    public enum FacingDirection
    {
        none, north, south, east, west
    };

    ///<summary>
    /// The position of the robot on the board.
    ///</summary> 
    public struct Position
    {
        public int x;
        public int y;
    }

    ///<summary>
    /// The direction the robot is facing and the position.
    ///</summary> 
    public struct RobotState
    {
        public FacingDirection direction; // defaults to none
        public Position position;
    }

    ///<summary>
    /// Class <c>ToyRobot</c> Models the Toy Robot and its journey in a Board of BoardsSize
    ///</summary> 
    public class ToyRobot
    {
        public RobotState robotState;
        public const int BoardSize = 5;

        public ToyRobot()
        {

        }

        ///<summary>
        /// Main Method called from the command line
        ///</summary> 
        public static void Main()
        {
            ToyRobot toyRobot = new ToyRobot();

            Console.WriteLine("Toy Robot Command Processor.");
            Console.WriteLine("Hit return on an empty line to exit.\r\n");

            while (true)
            {
                Console.Write("> ");
                var command = Console.ReadLine();
                if (string.IsNullOrEmpty(command))
                {
                    return;
                }
                toyRobot.DoCommand(command);
            }
        }

        ///<summary>
        /// Return true if a PLACE command has been executed.
        ///</summary> 
        public bool isPlaced()
        {
            return robotState.direction != FacingDirection.none;
        }

        ///<summary>
        /// Set the initial position and direction of the robot.
        ///</summary> 
        ///<param name="direction">The direction the robot is facing</param>
        ///<param name="position">The position of the robot</param>
        public void Place(FacingDirection direction, Position position)
        {
            robotState = new RobotState { direction = direction, position = position };
        }

        ///<summary>
        /// Moves robot one unit in direction faced. Limit the x and y co-ordinates between 0 and BoardSize.
        ///</summary> 
        public void Move()
        {
            switch (robotState.direction)
            {
                case FacingDirection.north:
                    robotState.position.y = Math.Min(robotState.position.y + 1, BoardSize);
                    break;
                case FacingDirection.south:
                    robotState.position.y = Math.Max(robotState.position.y - 1, 0);
                    break;
                case FacingDirection.east:
                    robotState.position.x = Math.Min(robotState.position.x + 1, BoardSize);
                    break;
                case FacingDirection.west:
                    robotState.position.x = Math.Max(robotState.position.x - 1, 0);
                    break;
            }
        }

        ///<summary>
        /// Turn the robot direction to the left
        ///</summary> 
        public void TurnLeft()
        {
            switch (robotState.direction)
            {
                case FacingDirection.north:
                    robotState.direction = FacingDirection.west;
                    break;
                case FacingDirection.south:
                    robotState.direction = FacingDirection.east;
                    break;
                case FacingDirection.east:
                    robotState.direction = FacingDirection.north;
                    break;
                case FacingDirection.west:
                    robotState.direction = FacingDirection.south;
                    break;
            }
        }

        ///<summary>
        /// Turn the robot direction to the right.
        ///</summary> 
        public void TurnRight()
        {
            switch (robotState.direction)
            {
                case FacingDirection.north:
                    robotState.direction = FacingDirection.east;
                    break;
                case FacingDirection.south:
                    robotState.direction = FacingDirection.west;
                    break;
                case FacingDirection.east:
                    robotState.direction = FacingDirection.south;
                    break;
                case FacingDirection.west:
                    robotState.direction = FacingDirection.north;
                    break;
            }
        }

        ///<summary>
        /// Report the robot direction to the console and return the displayed string.
        ///</summary> 
        ///<returns>Returns the output string</returns>
        public string Report() 
        {
            string output = $"Output: {robotState.position.x},{robotState.position.y},{robotState.direction.ToString().ToUpper()}";

            Console.WriteLine(output);

            return output;
        }

        ///<summary>
        /// Execute a command for the robot.
        /// Commands are:
        ///     PLACE X, Y, DIRECTION - DIRECTION is optional
        ///     MOVE
        ///     LEFT
        ///     RIGHT
        ///     REPORT
        ///</summary> 
        ///<param name="command">The command to execute</param>
        public void DoCommand(string command)
        {
            if (command.StartsWith("PLACE"))
            {
                var rest = command.Substring(6);
                var parts = rest.Split(",");


                Position position = new Position { x = int.Parse(parts[0]), y = int.Parse(parts[1]) };

                // the facingDirection is set to the current value in case its not input
                FacingDirection facingDirection = robotState.direction;

                // The direction parameter is optional               
                if (parts.Length == 3)
                {
                    switch (parts[2])
                    {
                        case "NORTH":
                            facingDirection = FacingDirection.north;
                            break;
                        case "SOUTH":
                            facingDirection = FacingDirection.south;
                            break;
                        case "EAST":
                            facingDirection = FacingDirection.east;
                            break;
                        case "WEST":
                            facingDirection = FacingDirection.west;
                            break;
                        default:
                            // invalid command - fail silently 
                            break;         
                    }
                }
                else
                {
                    if (!isPlaced())
                    {
                        // if a direction is not yet set then ignore this command
                        return;
                    }
                }
                Place(facingDirection, position);
            }
            else
            {
                // check a valid PLACE command was made before calling any of these
                if (!isPlaced())
                {
                    // if a direction and position is not yet set then ignore this command
                    return;
                }
                if (command == "MOVE")
                {
                    Move();
                }
                else if (command == "LEFT")
                {
                    TurnLeft();
                }
                else if (command == "RIGHT")
                {
                    TurnRight();
                }
                else if (command == "REPORT")
                {
                    Report();
                }
            }
        }
    }
}
