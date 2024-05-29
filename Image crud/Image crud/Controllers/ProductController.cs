using Image_crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Image_crud.Controllers
{
    public class ProductController : Controller
    {
        ImageContext context;
        IWebHostEnvironment env;
        public ProductController(ImageContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult editProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel prod)
        {
            string fileName = "";
            if (prod.photo != null)
            {
                var ext = Path.GetExtension(prod.photo.FileName);
                var size = prod.photo.Length;
                if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
                {
                    if (size <= 1000000)
                    {

                        string folder = Path.Combine(env.WebRootPath, "images");


                        fileName = Guid.NewGuid().ToString() + "_" + prod.photo.FileName;
                        string filepath = Path.Combine(folder, fileName);
                        prod.photo.CopyTo(new FileStream(filepath, FileMode.Create));

                        Product p = new Product()
                        {
                            Name = prod.Name,
                            Price = prod.Price,
                            ImagePath = fileName
                        };


                        context.Products.Add(p);
                        context.SaveChanges();
                        TempData["Success"] = "Product added...";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Size-Error"] = "Image Must be less then 1 MB";
                    }
                }
                else
                {
                    TempData["Ext_Error"] = "only PNG, JPG, JPEG, Images Allowed.";
                }
            }
           
                return View();
            }
        }
    }

