namespace px.com.test;

public class InterviewSolution
{
    public class Vendor
    {
        public string Name { get; set; }
        public int Leads { get; init; }
        public int LeadsAssignedToClient { get; set; }
    }

    public class Client
    {
        public string Name { get; set; }
        public int Budget { get; init; }
        public int AvailableBudget { get; set; }

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
            new() { Name = "Client 1", Budget = 50, AvailableBudget = 50 },
            new() { Name = "Client 2", Budget = 50, AvailableBudget = 50 }
        };

        var happyClients = MapVendorsToClients(clients, vendors);
    }
    
    private static List<Client> MapVendorsToClients(List<Client> clients, List<Vendor> vendors)
    {
        foreach (var client in clients)
        {
            // Давай на самом старте убедимся, что у клиента вообще остался бюджет
            if(client.AvailableBudget <= 0)
                continue;
            
            
            // Обойдем вендоров, чтобы глянуть, кто может дать лиды клиенту
            foreach (var vendor in vendors)
            {
                // Если вендор использовал все свои лиды, нет смысла его смотреть
                var availableLeads = vendor.Leads - vendor.LeadsAssignedToClient;
                
                if (availableLeads <= 0)
                    continue;

                for (var i = 0; i < availableLeads; i++)
                {
                    if (client.AvailableBudget > 0)
                    {
                        client.AvailableBudget--;
                        vendor.LeadsAssignedToClient++;

                        if (!client.VendorNames.Contains(vendor.Name))
                            client.VendorNames.Add(vendor.Name);
                    }
                }
            }
        }

        return clients;
    }
}