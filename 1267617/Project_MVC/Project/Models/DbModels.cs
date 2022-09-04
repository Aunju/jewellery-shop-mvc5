using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Category
    {
        public Category()
        {
            this.Items = new List<Items>();
        }
        public int CategoryId { get; set; }
        [Required, StringLength(50), Display(Name = "Category")]
        public string CategoryName { get; set; }
        //nev
        public virtual ICollection<Items> Items { get; set; }
        
    }
    public class Items
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string ItemName { get; set; }
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StoreDate { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        //fk
        [ForeignKey("Category"), Display(Name = "Category")]
        public int CategoryId { get; set; }
        public bool Available { get; set; }
        //nev
        public virtual Category Category { get; set; }
    }
    public class Employee
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Required, StringLength(40), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        //nev
        public virtual EmployeeAddress EmployeeAddress { get; set; }
        public virtual EmployeePhoto EmployeePhoto { get; set; }
    }
    public class EmployeeAddress
    {
        [Key, ForeignKey("Employee")]
        [Display(Name = "EmployeeId")]
        public int EmployeeId { get; set; }
        [Required, StringLength(150)]
        public string Address { get; set; }
        [Required, StringLength(4), Display(Name = "Post Code")]
        public string PostCode { get; set; }

        //nev
        public virtual Employee Employee { get; set; }
    }
    public class EmployeePhoto
    {
        [Key, ForeignKey("Employee")]
        [Display(Name = "EmployeeId")]
        public int EmployeeId { get; set; }
        public string Image { get; set; }

        //nev
        public virtual Employee Employee { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        [Required, StringLength(120)]
        public string UserId { get; set; }
        [StringLength(60)]
        public string CustomerName { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [CreditCard]
        public string CCNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CCExpire { get; set; }
        //Navigation
        public virtual IList<Order> Orders { get; set; }
    }
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ShippedDate { get; set; }
        //FK
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        //Navigation
        public virtual Customer Customer { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
    }
    public class OrderItem
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Items")]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }
        //Navligation
        public virtual Order Order { get; set; }
        public virtual Items Items { get; set; }
    }
    public class CartItem
    {
        public int CartItemId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
    public class JewelleryDbContext : DbContext
    {
        public JewelleryDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeePhoto> EmployeePhotos { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<JewelleryDbContext>
    {
        protected override void Seed(JewelleryDbContext context)
        {
            context.SaveChanges();
            base.Seed(context);
        }
    }
}