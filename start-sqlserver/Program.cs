using start_sqlserver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace start_sqlserver
{
    class Program
    {
        private static List<Service> serviceList;
        static void Main(string[] args)
        {
            string id = args.First();

            InitService();
            StartStopServices(id);
        }

        static void StartStopServices(string serviceIDToStart)
        {
            //stop all services
            foreach(Service service in serviceList)
            {
                if (ServiceController.GetServices().Any(s => s.ServiceName == service.Name))
                {
                    ServiceController serviceController = new ServiceController(service.Name);
                    if (service.ID != serviceIDToStart)
                    {
                        if (serviceController.Status != ServiceControllerStatus.Stopped)
                        {
                            serviceController.Stop();
                            Console.WriteLine("Service " + service.Name + " has been stopped.");
                        }
                        else
                        {
                            Console.WriteLine("Service " + service.Name + " already stopped.");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Service " + service.Name + " can not be found.");
                }
                    
            }

            //startservice
            string serviceNameToStart = serviceList.Where(s => s.ID == serviceIDToStart).Select(s=>s.Name).Single();
            if(ServiceController.GetServices().Any(s=>s.ServiceName == serviceNameToStart))
            {
                ServiceController serviceController = new ServiceController(serviceNameToStart);
                   
                if(serviceController.Status != ServiceControllerStatus.Running)
                {
                    serviceController.Start();
                    Console.WriteLine("Service " + serviceNameToStart + " has been started.");
                }
                else
                {
                    Console.WriteLine("Service " + serviceNameToStart + " already started.");
                }
            }
            else
            {
                Console.WriteLine("Service " + serviceNameToStart + " can not be found.");
            }
        }

        static void InitService()
        {
            serviceList = new List<Service>
            {
                new Service{
                    ID = "2008",
                    Name = "MSSQL$SQLEXPRESS2008"
                },
                new Service
                {
                    ID="2012",
                    Name="MSSQL$SQLEXPRESS2012"
                },
                new Service{
                    ID="2014",
                    Name="MSSQLSERVER"
                }
            };
        }
    }
}
