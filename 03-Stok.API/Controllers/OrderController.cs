using _01_Stok.Entities.Models.Concrete;
using _01_Stok.Entities.Models.Enums;
using _02_Stok.Repositories.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _03_Stok.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericRepo<Order> _oRepo;
        private readonly IGenericRepo<Product> _pRepo;
        private readonly IGenericRepo<OrderDetails> _odRepo;

        public OrderController(IGenericRepo<Order> oRepo, IGenericRepo<Product> pRepo, IGenericRepo<OrderDetails> odRepo)
        {
            _oRepo = oRepo;
            _pRepo = pRepo;
            _odRepo = odRepo;
        }


        //sipariş oluşturalım
        //sipariş oluşacak ve detaylarını da oluşturacak
        [HttpPost]
        public IActionResult AddOrder(int userId, [FromQuery] int[] productIds, [FromQuery] int[] quantities)
        {
            Order order = new Order();
            order.UserID = userId;
            order.Status = Status.Pending;
            order.IsActive = true;
            _oRepo.Add(order); //db ye eklenecek id verilecek
            //buraya bir if check yapılır
            for (int i = 0; i < productIds.Length; i++)
            {
                OrderDetails od = new OrderDetails();
                od.OrderID = order.ID;
                od.ProductID = productIds[i];
                od.Quantity = quantities[i];
                od.IsActive = true;
                od.UnitPrice = _pRepo.GetById(productIds[i]).Price * quantities[i]; //yukarı pRepo ekledik 
                _odRepo.Add(od); //yukarı odRepo ekledik
            }
            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            //todo: product ? çekilebilir mi? product icin Include ve ThenInclude ekledik
            return Ok(_oRepo.GetAll(a => a.User, b => b.OrderDetails).Include(a=>a.OrderDetails).ThenInclude(a=>a.Product));
        }

        //aktifleri getirelim, id bilgisi ile (herşeyini) getirelim, sipariş detaylarını(orderdetail) getirelim(order id almş olsun)
        // Bütün aktif siparişleri getiren metot
        [HttpGet]
        public IActionResult GetAllActiveOrders()
        {
            return Ok(_oRepo.GetActive());
        }

        // ID bilgisi ile siparişi getiren metot
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _oRepo.GetById(id, o => o.User, o => o.OrderDetails);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // Order ID ile sipariş detaylarını getiren metot
        [HttpGet("{id}/details")]
        public IActionResult GetOrderDetailsByOrderId(int id)
        {
            var orderDetails = _odRepo.GetAll(od => od.OrderID == id, y=>y.Product);
            return Ok(orderDetails);
        }



        //OrderController için bekleyen, onaylanan, reddedilen siparişleri getiren 3 ayrı get metodu yazalım

        // Bekleyen siparişleri getiren metot
        [HttpGet]
        public IActionResult GetPendingOrders() //Pending beklemede, askıda
        {
            var pendingOrders = _oRepo.GetAll(o => o.Status == Status.Pending);
            return Ok(pendingOrders);
        }

        // Onaylanan siparişleri getiren metot
        [HttpGet]
        public IActionResult GetApprovedOrders()
        {
            var approvedOrders = _oRepo.GetDefault(o => o.Status == Status.Confirm);
            return Ok(approvedOrders);
        }

        // Reddedilen siparişleri getiren metot
        [HttpGet]
        public IActionResult GetRejectedOrders()
        {
            var rejectedOrders = _oRepo.GetDefault(o => o.Status == Status.Cancelled);
            return Ok(rejectedOrders);
        }


        [HttpGet("{userId}")]
        public IActionResult GetOrdersByUserId(int userId)
        {

            var orders = _oRepo.GetAll(o => o.UserID == userId, o=>o.User, o => o.OrderDetails).Include(o => o.OrderDetails).ThenInclude(o => o.Product).ToList();
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }


        //Delete nasıl yapalım
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _oRepo.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            // Sipariş detaylarını da silmemiz gerekiyor
            var orderDetails = _odRepo.GetDefault(od => od.OrderID == id);
            foreach (var detail in orderDetails)
            {
                _odRepo.Remove(detail);
            }
            
            _oRepo.Remove(order); // Siparişi sil

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            var orderDetail = _odRepo.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            _odRepo.Remove(orderDetail);    // Sipariş detayını sil

            return Ok();
        }





    }
}

















