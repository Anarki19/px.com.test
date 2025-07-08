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

    // Тут идея решения простая, но мне нравится оно больше, ибо мы не проходим каждый раз для всех клиентов всех вендоров. 
    // Клиенты и вендоры удаляются из очереди и добавляются обратно, только если остались либо денюжки у клиентов либо ресурсы
    // Тут сложно алгоритма не линейная O(n), а O(n + m) что отработает гораздо быстрее
    private static List<Client> MapVendorsToClients(List<Client> clients, List<Vendor> vendors)
    {
        // Создаем очереди только из клиентов и вендоров с ресурсами/бюджетом
        var clientQueue = new Queue<Client>(clients.Where(c => c.Budget > 0));
        var vendorQueue = new Queue<Vendor>(vendors.Where(v => v.Leads > 0));
        
        // Основной цикл распределения - O(n + m)
        while (clientQueue.Count > 0 && vendorQueue.Count > 0)
        {
            var client = clientQueue.Dequeue();
            var vendor = vendorQueue.Dequeue();
            
            // Распределяем ресурсы между текущим клиентом и вендором
            var resourcesToAllocate = Math.Min(client.Budget, vendor.Leads);
            
            client.Budget -= resourcesToAllocate;
            vendor.Leads -= resourcesToAllocate;
            
            // Добавляем вендора в список клиента
            client.VendorNames.Add(vendor.Name);
            
            // Если у клиента остался бюджет, возвращаем его в очередь
            if (client.Budget > 0)
                clientQueue.Enqueue(client);
            
            // Если у вендора остались ресурсы, возвращаем его в очередь
            if (vendor.Leads > 0)
                vendorQueue.Enqueue(vendor);
        }

        return clients;
    }
}