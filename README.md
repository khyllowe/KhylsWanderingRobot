# KhylsWanderingRobot

A small project for job application.

it is a simple project that allows the user to create robots on a 5x5 grid (spanning from x:0 y:0 to x:4 y:4)

by typing in lines in the correct order you can create robots, move the robots and turn them.

Up to 25 robots can be place on the board (one for each x/y coordinate) and can all move spot in the direction they are facing at a time as long the space
they are moving towards is free and not outside of the 5x5 grid.

the following commands are:

PLACE X Y DIRECTION - PLACE will initiate placing a robot. X and Y are the coordinates they will be placed. DIRECTION should be one of either NORTH, SOUTH, EAST, WEST

MOVE - will move the selected robot forwards once in its current direction. If a robot tries to move either outside of the 5x5 area or into another robot the command will be ignored

LEFT - will turn the robot left

RIGHT - will turn the robot right

REPORT - will read out amount of robots aswell as each robot's name, location and direction

ROBOT X - ROBOT will initiate selecting a robot. X will be the number of a which robot you want selected (starting from 1)


There are no graphics to this, although using the REPORT command will allow you to see the attributes of everthing that could be displayed.

Thank you for looking at my project and while simple, it was a lot of fun to make. - Khyl
