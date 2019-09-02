namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Contains properties of category
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="code">Used for category code</param>
        public Category(string code)
        {
            Code = code;
            Desc = code;
            ParentCategoryCode = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">Used for category code</param>
        /// <param name="desc">Used for category description</param>
        /// <param name="parentCategoryCode">Used for parent category code</param>
        public Category(string code, string desc, string parentCategoryCode = null)
        {
            Code = code;
            Desc = desc;
            ParentCategoryCode = parentCategoryCode;
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor without description
        /// </summary>
        /// <param name="code">Used for category code</param>
        /// <param name="parentCategoryCode">Used for parent category code</param>
        public Category(string code, string parentCategoryCode)
            : this(code, code, parentCategoryCode) { }

        /// <inheritdoc />
        /// <summary>
        /// Constructor with category object
        /// </summary>
        /// <param name="code">Used for category code</param>
        /// <param name="parentCategory">Used for parent category</param>
        public Category(string code, Category parentCategory)
            : this(code, code, parentCategory.Code) { }

        public string Code { get; set; }
        public string Desc { get; set; }
        public string ParentCategoryCode { get; set; }
    }
}