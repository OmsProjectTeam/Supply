

using Microsoft.EntityFrameworkCore;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        IIOrder iOrder;
        IIBondType iBondType;
        IIMerchants iMerchants;
        IIProductCategory iProductCategory;
        IITypesProduct iTypesProduct;
        IIProductInformation iProductInformation;
        IIWareHouse iWareHouse;
        IIWareHouseBranch iWareHouseBranch;
        MasterDbcontext dbcontext;

        public OrderController(IIOrder iOrder1, IIBondType iBondType1, IIMerchants iMerchants1, IIProductCategory iProductCategory1, IITypesProduct iTypesProduct1, IIProductInformation iProductInformation1, IIWareHouse iWareHouse1, IIWareHouseBranch iWareHouseBranch1, MasterDbcontext dbcontext1)
        {
            iOrder = iOrder1;
            iBondType = iBondType1;
            iMerchants = iMerchants1;
            iProductCategory = iProductCategory1;
            iTypesProduct = iTypesProduct1;
            iProductInformation = iProductInformation1;
            iWareHouse = iWareHouse1;
            iWareHouseBranch = iWareHouseBranch1;
            dbcontext = dbcontext1;
        }
        public IActionResult MyOrder()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewOrder = iOrder.GetAll();
            return View(vmodel);
        }
        public IActionResult AddOrder(int? IdPurchaseOrder)
        {
            ViewBag.BondType = iBondType.GetAll();
            ViewBag.Merchants = iMerchants.GetAll();
            ViewBag.ProductCategory = iProductCategory.GetAll();
            ViewBag.TypesProduct = iTypesProduct.GetAll();
            ViewBag.ProductInformation = iProductInformation.GetAll();
            ViewBag.WareHouse = iWareHouse.GetAll();
            ViewBag.WareHouseBranch = iWareHouseBranch.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewOrder = iOrder.GetAll();
            if (IdPurchaseOrder != null)
            {
                vmodel.Order = iOrder.GetById(Convert.ToInt32(IdPurchaseOrder));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBOrder slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdPurchaseOrder = model.Order.IdPurchaseOrder;
                slider.IdBondType = model.Order.IdBondType;
                slider.IdMerchants = model.Order.IdMerchants;
                slider.IdProductCategory = model.Order.IdProductCategory;
                slider.IdTypesProduct = model.Order.IdTypesProduct;
                slider.IdProductInformation = model.Order.IdProductInformation;
                slider.IdBWareHouse = model.Order.IdBWareHouse;
                slider.IdBWareHouseBranch = model.Order.IdBWareHouseBranch;
                slider.PurchaseAuotNoumber = model.Order.PurchaseAuotNoumber;
                slider.PurchaseOrderNoumber = model.Order.PurchaseOrderNoumber;
                slider.PurchasePrice = model.Order.PurchasePrice;
                slider.sellingPrice = model.Order.sellingPrice;
                slider.GlobalPrice = model.Order.GlobalPrice;
                slider.SpecialSalePrice = model.Order.SpecialSalePrice;
                slider.QuantityIn = model.Order.QuantityIn;
                slider.QuantityOute = model.Order.QuantityOute;
                slider.Qrcode = model.Order.Qrcode;
                slider.DataEntry = model.Order.DataEntry;
                slider.DateTimeEntry = model.Order.DateTimeEntry;
                slider.CurrentState = model.Order.CurrentState;
                //Conditions
                var maxPurchaseAutoNumber = dbcontext.TBOrders.Max(o => (int?)o.PurchaseAuotNoumber) ?? 0;
                slider.PurchaseAuotNoumber = maxPurchaseAutoNumber + 1;
                if (slider.GlobalPrice == null)
                    slider.GlobalPrice = 0;
                if (slider.SpecialSalePrice == null)
                    slider.SpecialSalePrice = 0;
                if (slider.QuantityOute == null)
                    slider.QuantityOute = 0;

                if (slider.IdPurchaseOrder == 0 || slider.IdPurchaseOrder == null)
                {
                    var reqwest = iOrder.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyOrder");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iOrder.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyOrder");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return Redirect(returnUrl);
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect(returnUrl);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdPurchaseOrder)
        {
            var reqwistDelete = iOrder.deleteData(IdPurchaseOrder);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyOrder");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyOrder");

            }



        }
    }
}
