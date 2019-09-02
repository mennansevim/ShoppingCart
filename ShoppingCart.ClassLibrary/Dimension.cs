namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Size of product
    /// </summary>
    /// <remarks>
    /// Example: 50x40x10
    /// </remarks>
    public class Dimension
    {
        private readonly float _heigth;
        private readonly float _lenght;
        private readonly float _width;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <remarks>
        /// Output format HxLxW => 50x40x20
        /// </remarks>
        /// <param name="heigth">Heigth of product</param>
        /// <param name="lenght">Length of product</param>
        /// <param name="width">Width of product</param>
        public Dimension(float heigth = 0, float lenght = 0, float width = 0)
        {
            _heigth = heigth;
            _lenght = lenght;
            _width = width;
        }

        public float Heigth
        {
            get { return _heigth; }
        }

        public float Lenght
        {
            get { return _lenght; }
        }

        public float Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Converts to string dimension of product
        /// </summary>
        /// <returns></returns>
        public string GetDimensionAsString()
        {
            return string.Format("{0}x{1}x{2}", Heigth, Lenght, Width);
        }
    }
}