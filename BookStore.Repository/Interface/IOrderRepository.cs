﻿using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersByUser(string userId);
        Order GetDetailsForOrder(Guid? id);
    }
}
