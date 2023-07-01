using System;
using BackendService.Data;
using BackendService.Models;

namespace BackendService
{
	public class Seed
	{
        private readonly DataContext _dataContext;

        public Seed(DataContext context)
        {
            _dataContext = context;
        }

        public DataContext Get_dataContext()
        {
            return _dataContext;
        }

        public void SeedDataContext(DataContext _dataContext)
        {
            if (!_dataContext.ProductVendors.Any())
            {


                var address1 = new ReviewerAddress()
                {
                    StreetNumber = "11",
                    Street = "Blue Jays Way",
                    PostalCode = "M6D 3V7",
                    City = "Toronto",
                    StateProvince = "Ontario",
                    Country = "Canada",
                    DefaultAddress = true,
                 
                };
     

                var address2 = new ReviewerAddress()
                {
                    StreetNumber = "23",
                    Street = "Saint Louis Ave",
                    PostalCode = "H9P 2C2",
                    City = "Montreal",
                    StateProvince = "Quebec",
                    Country = "Canada",
                    DefaultAddress = true,
 
                };


                var address3 = new ReviewerAddress()
                {
                    StreetNumber = "59",
                    Street = "Hayden Street",
                    PostalCode = "M4Y 2P2",
                    City = "Toronto",
                    StateProvince = "Ontario",
                    Country = "Canada",
                    DefaultAddress = true,
        
                };

                var address4 = new ReviewerAddress()
                {
                    StreetNumber = "59",
                    Street = "Tether Street",
                    PostalCode = "M1Y 6P2",
                    City = "Etobicoke",
                    StateProvince = "Ontario",
                    Country = "Canada",
                    DefaultAddress = false,
         
                };

                var reviewer1 = new Reviewer()
                {
                    FirstName = "Chris",
                    LastName = "Kocabas",
                    DefaultAddressId = 1,
                    ReviewerAddresses = new List<ReviewerAddress> { address1 }
                };

                var reviewer2 = new Reviewer()
                {
                    FirstName = "Taylor",
                    LastName = "Jones",
                    DefaultAddressId = 2,
                    ReviewerAddresses = new List<ReviewerAddress> { address2 }
                };

                var reviewer3 = new Reviewer()
                {
                    FirstName = "Ravinder",
                    LastName = "McGregor",
                    DefaultAddressId = 3,
                    ReviewerAddresses = new List<ReviewerAddress> { address3, address4 }
                };

                address1.ReviewerId = reviewer1.Id;
                address2.ReviewerId = reviewer2.Id;
                address3.ReviewerId = reviewer3.Id;
                address4.ReviewerId = reviewer3.Id;


                var productVendors = new List<ProductVendor>()
                {
                    new ProductVendor()
                    {
                        Product = new Product()
                        {
                            Name = "Macbook",
                            Price = 3200,
                            AddedDate = new DateTime(2023,1,1),
                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory { Category = new Category() { Name = "Electronics"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review() { Title="A bit expensive but...",Text = "I have to admit it is worth every penny!", Rating = 5,
                                Reviewer = reviewer1 },
                                new Review() { Title="Mixed feelings", Text = "I got this thing and now I cant play Call of Duty on it man...", Rating = 3,
                                Reviewer = reviewer2 },
                                new Review() { Title="Starbucks here I come...",Text = "I would always envy people who go to starbucks to read their emails, now I am one of them mwahahaha!", Rating = 5,
                                Reviewer = reviewer3 },
                            }
                        },
                        Vendor = new Vendor()
                        {
                            Name = "Apple",
                            JoinDate = new DateTime(1973,5,3),
                            Country = new Country()
                            {
                                Name = "USA"
                            }
                        }
                    },
                    new ProductVendor()
                    {
                        Product = new Product()
                        {
                            Name = "Standing Desk",
                            Price = 600,
                            AddedDate = new DateTime(2013,7,14),
                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory { Category = new Category() { Name = "Furniture"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review() { Title="Amazing",Text = "Iou can only sit for so long, this is a life saver (literally!)", Rating = 5, Reviewer = reviewer1},
                                new Review() { Title="Stay away! Came without the power cord!",Text = "I am trying to reach the seller buy they just wont respond. What kind of a customer service is this?", Rating = 2, Reviewer = reviewer2 },
                                new Review() { Title="No idea but seems good",Text = "I just got this to look cool to my flatmates. But it is holding up for now!", Rating = 5, Reviewer = reviewer3 },
                            }
                        },
                        Vendor = new Vendor()
                        {
                            Name = "Ikea",
                            JoinDate = new DateTime(1964,5,13),
                            Country = new Country()
                            {
                                Name = "Sweden"
                            }
                        }
                    },
                    new ProductVendor()
                    {
                        Product = new Product()
                        {
                            Name = "Nike Air 9300",
                            Price = 210,
                            AddedDate = new DateTime(2023,6,13),
                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory { Category = new Category() { Name = "Shoes"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review() { Title="Not bad",Text = "I am more of a New Balance guy but got this on sale for 130 and it seems comfy.", Rating = 4,
                                Reviewer = reviewer1 },
                                new Review() { Title="Color not so accurate", Text = "It seemed more like an Aliciablue but it is totally white, like snow white...", Rating = 2,
                                Reviewer = reviewer2 },
                                new Review() { Title="Good value",Text = "I got it for 100 bucks haha, gotta love Black Friday!", Rating = 5,
                                Reviewer = reviewer3 },
                            }
                        },
                        Vendor = new Vendor()
                        {
                            Name = "Nike",
                            JoinDate = new DateTime(1973,5,3),
                            Country = new Country()
                            {
                                Name = "France"
                            }
                        }
                    },
                };
                _dataContext.Reviewers.AddRange(reviewer1, reviewer2, reviewer3);
                _dataContext.ProductVendors.AddRange(productVendors);
                _dataContext.SaveChanges();
            }
        }
    }
}

