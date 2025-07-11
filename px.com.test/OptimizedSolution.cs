﻿using System;
using System.Collections.Generic;

namespace px.com.test;

public class OptimizedSolution
{
    public class Vendor
    {
        public string Name { get; set; }
        public int Leads { get; set; }
    }

    public class Client
    {
        public string Name { get; set; }
        public int Budget { get; set; }
        public List<string> VendorNames { get; set; } = [];
    }

    public void Execute()
    {
        var vendors = new List<Vendor>
        {
            new() { Name = "Vendor 1", Leads = 30 },
            new() { Name = "Vendor 2", Leads = 30 },
            new() { Name = "Vendor 3", Leads = 40 }
        };

        var clients = new List<Client>
        {
            new() { Name = "Client 1", Budget = 50, },
            new() { Name = "Client 2", Budget = 50, }
        };

        MapVendorsToClients(clients, vendors);
    }

    // Переделал алгорит еще раз. Теперь точно обходит клиента и вендоров один раз, в стэк возвращаю обратно только в том случае,
    // если остались лиды
    private static List<Client> MapVendorsToClients(List<Client> clients, List<Vendor> vendors)
    {
        // Создаем стэк только для вендоров. Переделал на стэк, чтобы быстрее избавлять от вендоров, у которых закончились лиды.
        var vendorStack = new Stack<Vendor>(vendors);

        foreach (var client in clients)
        {
            while (client.Budget > 0 && vendorStack.Count > 0)
            {
                var vendor = vendorStack.Pop();

                // Проверяю, есть ли у Вендора лиды, иначе нет смысла идти дальше
                if (vendor.Leads <= 0)
                    continue;

                var resourcesToAllocate = Math.Min(client.Budget, vendor.Leads);

                client.Budget -= resourcesToAllocate;
                vendor.Leads -= resourcesToAllocate;

                client.VendorNames.Add(vendor.Name);

                // Если у вендора еще остались лиды, а клиент уже возсопльзовал все деньги, то возвращаем вендора обратно в стэк
                if (vendor.Leads > 0)
                    vendorStack.Push(vendor);
            }
        }

        return clients;
    }
}