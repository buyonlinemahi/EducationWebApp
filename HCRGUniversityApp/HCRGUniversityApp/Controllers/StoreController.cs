using AutoMapper;
using HCRGUniversityApp.Domain.Models.Product;
using HCRGUniversityApp.Domain.Models.ProductModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class StoreController : BaseController
    {
        // GET: Store
        private readonly NEPService.ProductService.IProductService _productservice;
        private readonly NEPService.NewsService.INewsService _newsService;
        public readonly IEncryption _encryptionService;
        public StoreController(NEPService.ProductService.IProductService productservice, IEncryption encryptionService, NEPService.NewsService.INewsService newsService)
        {
            _productservice = productservice;
            _encryptionService = encryptionService;
            _newsService = newsService;

        }

        public ActionResult Index()
        {
            PagedProduct ProductModel = new PagedProduct();

            var pro = _productservice.GetPagedProduct(GlobalConst.Records.Skip, GlobalConst.Records.Take20);

            ProductModel.Products = Mapper.Map<IEnumerable<Product>>(pro.Products);
            ProductModel.TotalCount = pro.TotalCount;
            return View(ProductModel);
        }

        [HttpPost]
        public ActionResult getProducts(int skip)
        {
            PagedProduct ProModel = new PagedProduct();
            var pro = _productservice.GetPagedProduct(skip, GlobalConst.Records.Take20);
            ProModel.Products = Mapper.Map<IEnumerable<Product>>(pro.Products);
            ProModel.TotalCount = pro.TotalCount;
            return Json(ProModel, GlobalConst.Message.text_html);
        }


        public ActionResult ProductDetail(string pid)
        {
            Session["MyprodcutID"] = pid;
            pid = _encryptionService.DecryptString2(pid);
            Product pro = new Product();
            pro = Mapper.Map<Product>(_productservice.GetProductByID(Convert.ToInt32(pid)));
            return View(pro);
        }




        [HttpPost]
        public ActionResult getDeliveryTermConditions()
        {
            DeliveryTerm _deliveryTermModel = new DeliveryTerm();
            _deliveryTermModel.DeliveryTermContents = _newsService.GetDeliveryTermAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))).DeliveryTermContents;
            string decodedHTML = HttpUtility.HtmlDecode(_deliveryTermModel.DeliveryTermContents);
            if (decodedHTML != null)
            {
                _deliveryTermModel.DeliveryTermContents = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                _deliveryTermModel.DeliveryTermContents = _deliveryTermModel.DeliveryTermContents.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                _deliveryTermModel.DeliveryTermContents = _deliveryTermModel.DeliveryTermContents.Replace("&nbsp;", "");
            }
            else
                _deliveryTermModel.DeliveryTermContents = _deliveryTermModel.DeliveryTermContents;
            return Json(_deliveryTermModel, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult getRetrunPolicys()
        {
            ReturnPolicy _returnPolicy = new ReturnPolicy();
            _returnPolicy.ReturnPolicyContent = _newsService.GetReturnPolicyAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))).ReturnPolicyContent;
            string decodedHTML = HttpUtility.HtmlDecode(_returnPolicy.ReturnPolicyContent);
            if (decodedHTML != null)
            {
                _returnPolicy.ReturnPolicyContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                _returnPolicy.ReturnPolicyContent = _returnPolicy.ReturnPolicyContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                _returnPolicy.ReturnPolicyContent = _returnPolicy.ReturnPolicyContent.Replace("&nbsp;", "");
            }
            else
                _returnPolicy.ReturnPolicyContent = _returnPolicy.ReturnPolicyContent;
            return Json(_returnPolicy, GlobalConst.Message.text_html);
        }
    }
}