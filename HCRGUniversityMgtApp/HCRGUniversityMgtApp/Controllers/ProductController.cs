using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Product;
//using HCRGUniversityMgtApp.Domain.Models.Product;
using HCRGUniversityMgtApp.Domain.Models.ProductModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using HCRGUniversityMgtApp.NEPService.CommonService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class ProductController : BaseController
    {
        private readonly NEPService.ProductService.IProductService _productservice;
        private readonly IStorage _storageService;
        private readonly ICommonService _commonService;
        // GET: Product
        public ProductController(NEPService.ProductService.IProductService productservice, ICommonService commonService, IStorage storageService)
        {
            _productservice = productservice;
            _commonService = commonService;
            _storageService = storageService;
        }
        [HttpGet]
        public JsonResult getAllState()
        {
            return Json(_commonService.getAllState(), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            PagedProduct ProductModel = new PagedProduct();
            ProductModel = Mapper.Map<PagedProduct>(_productservice.GetPagedProductByProductName("", GlobalConst.Records.Skip, GlobalConst.Records.Take));
            ProductModel.PagedRecords = GlobalConst.Records.LandingTake;
            return View(ProductModel); 
        }

        #region StorPurchase
        [HttpGet]
        public ActionResult ProductPurchase()
        {
            PagedProductPurchasesRecord model = new PagedProductPurchasesRecord();
            model = Mapper.Map<PagedProductPurchasesRecord>(_productservice.getProductPurchaseDetail(GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID));
            return View(model);
            //return View();
        }
       
        public ActionResult ProductPurchase(int? skip)
        {
            PagedProductPurchasesRecord pagedProductPurchase = new PagedProductPurchasesRecord();
            if (skip == null)
            {
                var ProductPurchaseList = _productservice.getProductPurchaseDetail(GlobalConst.Records.Skip, GlobalConst.Records.Take, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]));
                pagedProductPurchase.ProductPurchasesRecords = Mapper.Map<IEnumerable<ProductPurchase>>(ProductPurchaseList.ProductPurchasesRecords);
                pagedProductPurchase.TotalCount = ProductPurchaseList.TotalCount;
                return View(pagedProductPurchase);
            }
            else
            {
                var ProductPurchaseList = _productservice.getProductPurchaseDetail(skip.Value, GlobalConst.Records.Take, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]));
               pagedProductPurchase.ProductPurchasesRecords = Mapper.Map<IEnumerable<ProductPurchase>>(ProductPurchaseList.ProductPurchasesRecords);
               pagedProductPurchase.TotalCount = ProductPurchaseList.TotalCount;
               return Json(pagedProductPurchase, GlobalConst.Message.text_html);
            }
        }
        [HttpPost]
        public ActionResult GetProductPurchaseDetailByID(int shippingpaymentID, int? skip)
        {
            try
            {
                if (skip == null)
                {
                    PagedProductPurchase Model = new PagedProductPurchase();
                    Model = Mapper.Map<PagedProductPurchase>(_productservice.getProductPurchaseDetailByID(shippingpaymentID, GlobalConst.Records.Skip, GlobalConst.Records.Take5));
                    return Json(Model, GlobalConst.Message.text_html);
                }
                else 
                {
                    PagedProductPurchase Model = new PagedProductPurchase();
                    Model = Mapper.Map<PagedProductPurchase>(_productservice.getProductPurchaseDetailByID(shippingpaymentID, skip.Value, GlobalConst.Records.Take5));
                    return Json(Model, GlobalConst.Message.text_html);
                }
            }
            catch 
            {
                return Json("");
            }

        }

        [HttpPost]
        public ActionResult GetProductShippingAddressDetailByID(int shippingpaymentID)
        {
            try 
            {
                ProductShippingAddressDetailByID ProductModel = new ProductShippingAddressDetailByID();
                ProductModel = Mapper.Map<ProductShippingAddressDetailByID>(_productservice.getProductShippingAddressDetailByID(shippingpaymentID));
                ProductModel.ShippingPaymentID = shippingpaymentID;
                var stateNameList = _commonService.getAllState();
                var stateName = stateNameList.Where(st => st.StateID == ProductModel.SAStateID).Select(st => new { st.StateName }).SingleOrDefault();
                string _st = stateName.StateName;
                ProductModel.StateName = _st;
                return Json(ProductModel, GlobalConst.Message.text_html);
            }
            catch  
            {
                return Json("");
            }

        }
       [HttpPost]
        public ActionResult GetProductShippingDetail(int? Skip, int ShippingPaymentID)
        {
           try
            {
                if (Skip == null)
                {
                    PagedProductShippingDetail modelproductshippingDetail = new PagedProductShippingDetail();
                    var productDetail = _productservice.getProductShippingDetail(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
                    var _productShoppingDetails = Mapper.Map<IEnumerable<ProductShippingDetail>>(productDetail.ProductShippingDetails);
                    var ModelShipping = _productShoppingDetails.Where(pro => pro.ShippingPaymentID == ShippingPaymentID).Select(pro => new { pro.ProductShippingDetailID, pro.ProductShippedOn, pro.ShippingPaymentID }).Distinct();
                   // Request["ProductShippedOn"] = _productShoppingDetails.ProductShippedOn;
                    return Json(ModelShipping);
                }
                else
                {
                    PagedProductShippingDetail modelproductshippingDetail = new PagedProductShippingDetail();
                    var productDetail = _productservice.getProductShippingDetail(Skip.Value, GlobalConst.Records.LandingTake);
                    var _productShoppingDetails =  Mapper.Map<IEnumerable<ProductShippingDetail>>(productDetail.ProductShippingDetails);
                    var ModelShipping = _productShoppingDetails.Where(pro => pro.ShippingPaymentID == ShippingPaymentID).Select(pro => new { pro.ProductShippingDetailID, pro.ProductShippedOn, pro.ShippingPaymentID }).Distinct();                     
                    return Json(ModelShipping);
                }
            }
            catch  
            {
              return Json("");
            }
         }       
        [HttpPost]
        public ActionResult Addproductshippingdetail(ProductShippingDetail productshippingdetail)
        {
            productshippingdetail.CreatedBy = HCRGCLIENT.ClientID;
            productshippingdetail.CreatedOn = DateTime.Now;                  
            try
            {
                productshippingdetail.ProductShippingDetailID = _productservice.addProductShippingDetail(Mapper.Map<HCRGUniversityMgtApp.NEPService.ProductService.ProductShippingDetail>(productshippingdetail));
                productshippingdetail.Message = "Successfully Saved";                
            }
            catch  
            { 
            
            }
            return Json(productshippingdetail);
        }
        #endregion
        public ActionResult Add(int? productID)
        {
            Product product = new Product();
            if (productID != null)
            {
                product = Mapper.Map<Product>(_productservice.GetProductByID(Convert.ToInt32(productID)));
                var _res = _productservice.GetProductShoppingAllByProductID(Convert.ToInt32(productID));

                 int ProductPaidCount =0;
                foreach (var res in _res)
                    ProductPaidCount += res.Quantity;
                product.ProductPaidCount = ProductPaidCount;
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            //  var message = "";
            int _prTotalQty = 0;
            Product1 pro = new Product1();
            if (product.ProductImageFile != null)
                product.ProductImage = UploadProductImage(product.ProductImageFile);

            if (product.ProductFile_File != null)
                product.ProductFile = UploadProductFile(product.ProductFile_File);


            _prTotalQty = product.ProductTotalQuantity.Value;

            if (product.ProductID == 0)
            {
                if (_prTotalQty == 0)
                    product.ProductCurrentBalance = -2;                   
                product.ClientID = HCRGCLIENT.ClientID; 
                product.ProductID = _productservice.AddProduct(Mapper.Map<HCRGUniversityMgtApp.NEPService.ProductService.Product>(product));
                ProductQuantity productQuantity = new ProductQuantity();

                productQuantity.ProductID = product.ProductID;
                productQuantity.ProdQuantity = _prTotalQty;
                productQuantity.ProdQuantityDate = DateTime.Now;
                productQuantity.UserID = HCRGCLIENT.ClientID;
                productQuantity.Mode = "Add";
                if (productQuantity.ProdQuantity != 0)
                _productservice.addProductQuantity(Mapper.Map<HCRGUniversityMgtApp.NEPService.ProductService.ProductQuantity>(productQuantity));

                product.Message = "Successfully Saved";
            }
            else
            {
                if (_prTotalQty == 0)
                {
                    product.ProductCurrentBalance = -2;
                }    
                Product _ProductDb = Mapper.Map<Product>(_productservice.GetProductByID(product.ProductID));
                _productservice.UpdateProduct(Mapper.Map<HCRGUniversityMgtApp.NEPService.ProductService.Product>(product));

                ProductQuantity productQuantity = new ProductQuantity();

                int Qty = 0;

                if (product.ProductTotalQuantity.Value > _ProductDb.ProductTotalQuantity.Value)
                {
                    //product.ProductTotalQuantity = product.ProductTotalQuantity.Value - _ProductDb.ProductTotalQuantity.Value; // latest product total qty

                    Qty = product.ProductTotalQuantity.Value - _ProductDb.ProductTotalQuantity.Value; // latest product total qty
                    //product.ProductCurrentBalance = product.ProductCurrentBalance.Value - _ProductDb.ProductCurrentBalance.Value; // latest product total qty
                    productQuantity.Mode = "Add";
                }
                else
                {
                    //product.ProductTotalQuantity = _ProductDb.ProductTotalQuantity.Value - product.ProductTotalQuantity.Value;
                    Qty = _ProductDb.ProductTotalQuantity.Value - product.ProductTotalQuantity.Value;
                    //product.ProductCurrentBalance = _ProductDb.ProductCurrentBalance.Value - product.ProductCurrentBalance.Value;
                    productQuantity.Mode = "Sub";
                }
                
                
                productQuantity.ProductID = product.ProductID;
                productQuantity.ProdQuantity = Qty;
                productQuantity.ProdQuantityDate = DateTime.Now;
                productQuantity.UserID = HCRGCLIENT.ClientID;

                if (productQuantity.ProdQuantity != 0)
                _productservice.addProductQuantity(Mapper.Map<HCRGUniversityMgtApp.NEPService.ProductService.ProductQuantity>(productQuantity));

                product.Message = "Successfully Updated";
            }
            pro.ProductDesc = product.ProductDesc;
            pro.ProductFile = product.ProductFile;
            pro.ProductImage = product.ProductImage;
            pro.ProductPrice = product.ProductPrice;
            pro.ProductType = product.ProductType;
            pro.ProductID = product.ProductID;
            pro.Message = product.Message;
            pro.ProductName = product.ProductName;
            pro.ProductCurrentBalance = product.ProductCurrentBalance;
            pro.ProductTotalQuantity = product.ProductTotalQuantity;
            return Json(pro);
        }

        [HttpPost]
        public ActionResult SearchProduct(string searchText, int? skip)
        {
            PagedProduct ProductModel = new PagedProduct();
            if (searchText == null)
                ProductModel = Mapper.Map<PagedProduct>(_productservice.GetPagedProductByProductName("", skip.Value, GlobalConst.Records.Take));
            else
                ProductModel = Mapper.Map<PagedProduct>(_productservice.GetPagedProductByProductName(searchText, GlobalConst.Records.Skip, GlobalConst.Records.Take));
            ProductModel.PagedRecords = GlobalConst.Records.LandingTake;
            return Json(ProductModel, GlobalConst.Message.text_html);
        }


        private string UploadProductFile(HttpPostedFileBase file)
        {
            var path = _storageService.SetProductFileStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + HCRGCLIENT.ClientID), file.FileName);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }


        private string UploadProductImage(HttpPostedFileBase file)
        {
            var path = _storageService.SetProductImageStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + HCRGCLIENT.ClientID), file.FileName);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }
    }
}