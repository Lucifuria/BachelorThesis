namespace Visualization
{
    /// <summary>
    /// Interface for all geometric objects (point, line, line segment, circle, angle).
    /// </summary>
    public interface GeometricObject
    {
        /// <summary>
        /// Returns the first name of object.
        /// </summary>
        /// <returns>First name of object.</returns>
        string GetFirstName();

        /// <summary>
        /// Returns the second name of object.
        /// </summary>
        /// <returns>Second name of object.</returns>
        string GetSecondName();
    }
}
