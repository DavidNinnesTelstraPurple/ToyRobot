using NUnit.Framework;

namespace ToyRobot
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
            
        }
        
        [Test]
        ///<summary>
        /// The supplied example 1.
        ///</summary> 
        public void Example1()
        {
            ToyRobot toyRobot = new ToyRobot();
            toyRobot.DoCommand("PLACE 0,0,NORTH");
            toyRobot.DoCommand("MOVE");
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.north);
            Assert.AreEqual(toyRobot.robotState.position.x, 0);
            Assert.AreEqual(toyRobot.robotState.position.y, 1);

            var output = toyRobot.Report();
            Assert.AreEqual("Output: 0,1,NORTH", output);
        }

        [Test]
        ///<summary>
        /// The supplied example 2.
        ///</summary> 
        public void Example2()
        {
            ToyRobot toyRobot = new ToyRobot();
            toyRobot.DoCommand("PLACE 0,0,NORTH");
            toyRobot.DoCommand("LEFT");
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.west);
            Assert.AreEqual(toyRobot.robotState.position.x, 0);
            Assert.AreEqual(toyRobot.robotState.position.y, 0);

            var output = toyRobot.Report();
            Assert.AreEqual("Output: 0,0,WEST", output);
        }

        [Test]
        ///<summary>
        /// The supplied example 3.
        ///</summary> 
        public void Example3()
        {
            ToyRobot toyRobot = new ToyRobot();
            toyRobot.DoCommand("PLACE 1,2,EAST");
            toyRobot.DoCommand("MOVE");
            toyRobot.DoCommand("MOVE");
            toyRobot.DoCommand("LEFT");
            toyRobot.DoCommand("MOVE");
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.north);
            Assert.AreEqual(toyRobot.robotState.position.x, 3);
            Assert.AreEqual(toyRobot.robotState.position.y, 3);

            var output = toyRobot.Report();
            Assert.AreEqual("Output: 3,3,NORTH", output);
        }

        [Test]
        ///<summary>
        /// The supplied example 4.
        ///</summary> 
        public void Example4()
        {
            ToyRobot toyRobot = new ToyRobot();
            toyRobot.DoCommand("PLACE 1,2,EAST");
            toyRobot.DoCommand("MOVE");
            toyRobot.DoCommand("LEFT");
            toyRobot.DoCommand("MOVE");
            toyRobot.DoCommand("PLACE 3,1");
            toyRobot.DoCommand("MOVE");
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.north);
            Assert.AreEqual(toyRobot.robotState.position.x, 3);
            Assert.AreEqual(toyRobot.robotState.position.y, 2);

            var output = toyRobot.Report();
            Assert.AreEqual("Output: 3,2,NORTH", output);
        }
        
        

        [Test]
        ///<summary>
        /// Check the commands do nothing because a place is not yet issued.
        ///</summary> 
        public void checkCommandsBeforePlace()
        {
            ToyRobot toyRobot = new ToyRobot();

      
            toyRobot.DoCommand("MOVE");
            toyRobot.DoCommand("LEFT");
            toyRobot.DoCommand("RIGHT");
            toyRobot.DoCommand("REPORT");
            toyRobot.DoCommand("PLACE 3,1");
          
            
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.none);
            Assert.AreEqual(toyRobot.robotState.position.x, 0);
            Assert.AreEqual(toyRobot.robotState.position.y, 0);

            
        }
        
        [Test]
        ///<summary>
        /// Check that a move in all directions stays within the board bounds even with extra MOVE commands.
        ///</summary> 
        public void checkBounds()
        {
            ToyRobot toyRobot = new ToyRobot();
            toyRobot.DoCommand("PLACE 3,2,EAST");

            for (var i = 0; i < ToyRobot.BoardSize + 1; i++)
            {
                toyRobot.DoCommand("MOVE");
            }
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.east);
            Assert.AreEqual(toyRobot.robotState.position.x, 5);
            Assert.AreEqual(toyRobot.robotState.position.y, 2);

            var output = toyRobot.Report();
            Assert.AreEqual("Output: 5,2,EAST", output);

            toyRobot.DoCommand("LEFT");
            for (var i = 0; i < ToyRobot.BoardSize + 1; i++)
            {
                toyRobot.DoCommand("MOVE");
            }
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.north);
            Assert.AreEqual(toyRobot.robotState.position.x, 5);
            Assert.AreEqual(toyRobot.robotState.position.y, 5);

            output = toyRobot.Report();
            Assert.AreEqual("Output: 5,5,NORTH", output);

            toyRobot.DoCommand("LEFT");
            for (var i = 0; i < ToyRobot.BoardSize + 1; i++)
            {
                toyRobot.DoCommand("MOVE");
            }
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.west);
            Assert.AreEqual(toyRobot.robotState.position.x, 0);
            Assert.AreEqual(toyRobot.robotState.position.y, 5);

            output = toyRobot.Report();
            Assert.AreEqual("Output: 0,5,WEST", output);

            toyRobot.DoCommand("LEFT");
            for (var i = 0; i < ToyRobot.BoardSize + 1; i++)
            {
                toyRobot.DoCommand("MOVE");
            }
            Assert.AreEqual(toyRobot.robotState.direction, FacingDirection.south);
            Assert.AreEqual(toyRobot.robotState.position.x, 0);
            Assert.AreEqual(toyRobot.robotState.position.y, 0);

            output = toyRobot.Report();
            Assert.AreEqual("Output: 0,0,SOUTH", output);
        }
        
    }


}
