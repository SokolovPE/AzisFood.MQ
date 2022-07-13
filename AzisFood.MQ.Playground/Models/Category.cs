using AzisFood.MQ.Abstractions.Attributes;

namespace Catalog.DataAccess.Models
{
    /// <summary>
    /// Model of product category
    /// </summary>
    [BusTopic(Name = "category")]
    public class Category
    {
        protected Category()
        {
            Id = Guid.NewGuid();
        }
        
        /// <summary>
        ///     Identifier
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Title of category
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Possible subcategories
        /// </summary>
        public Guid[] SubCategories { get; set; }
        
        /// <summary>
        /// Order of category
        /// </summary>
        public int Order { get; set; }
    }
}