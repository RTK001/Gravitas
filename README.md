## Synopsis 
A simple game that takes gravity seriously. 
It consists of C# scripts built into the Unity game engine.

## Code Example



public void CalculatePoint()
    {   // This method calculates the position, velocity and acceleration of the trajectory point.
        if (prevPoint != null)  // Do not do this calculation on the zero point, as the calculation requires a previous point to project forward.
        {
            projectedTime = prevPoint.projectedTime + timeIncrement;        // Get the most up-to-date world time to update to

            for (int j = 0; j < numberOfIterations; j++)            // THis is an iterative calculation that successively improves accuracy.
            {
                // Set the New velocity to be the previous velocity + the mean(average) acceleration * Time
                Velocity = prevPoint.GetVelocity() + (prevPoint.GetAcceleration() + Acceleration) * 0.5f * timeIncrement;
                // Set the New position to be the previous position + the mean(average) velocity * Time
                Position = prevPoint.GetPosition() + (prevPoint.GetVelocity() + Velocity) * 0.5f * timeIncrement;
                // calculate the acceleration due to gravity at the newly calculated position.
                Acceleration = gravMan.getGravPotentialAtPoint(prevPoint.GetPosition(), projectedTime);
            }
        }
    }

## Motivation


Primarily an experiment into C# and the Unity Engine, but with an insight into orbital mechanics and perhaps a flavour of realism.
## Installation

Coming soon...
## Contributors


Currently only myself.
## License


As an eductional "just for fun" project, there is no strong requirement for a license.