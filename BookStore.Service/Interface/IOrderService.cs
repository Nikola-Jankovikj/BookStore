using BookStore.Domain.Domain;
using BookStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersByUser(string userId);
        OrderDto GetDetailsForOrder(Guid id);
    }
}
