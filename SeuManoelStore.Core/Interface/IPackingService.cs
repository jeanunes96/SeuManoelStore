﻿using SeuManoelStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeuManoelStore.Core.Interface
{
    public interface IPackingService
    {
        OrderResponse PackOrders(OrderRequest orderRequest);
    }
}
