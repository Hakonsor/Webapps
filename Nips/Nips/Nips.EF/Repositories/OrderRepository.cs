using System;
using System.Collections.Generic;
using System.Linq;
using Nips.Model.Entities;
using Nips.DAL.Entities;
using System.Diagnostics;
using System.IO;
using Nips.DAL.Repositories.Stud;

namespace Nips.DAL.Repositories
{
    public class OrderRepository : BaseRepository , IOrderRepository
    {
        public int checkout(Handlevogn vogn)
        {
            var newOrder = new Ordere()
            {
                orderdato = DateTime.Now,
                kundeid = vogn.userID,
                sendt = false

            };
            try
            {
                Db.Ordere.Add(newOrder);
                

            }
            catch(Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Ordere bli ikke lagt i databasen");
            }
            try
            {
                foreach (var vare in vogn.handlevognVare)
                {
                    var newVare = new Entities.OrderLiner()
                    {
                        produktid = vare.produkt.id,
                        orderid = newOrder.id,
                        antall = vare.antall,
                      

                    };
                    Db.OrderLiner.Add(newVare);
                }
                Db.SaveChanges();

                return newOrder.id;
            }
            catch(Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("OrderLine ble ikke lagti databasen");
            }
            return -1;
        }
        public List<Order> getOrder()
        {
            var result = (from o in Db.Ordere 
                          select new Order()
                          {
                              id = o.id,
                              kundeid = o.kundeid,
                              orderdato = o.orderdato,
                              sendt = o.sendt
                          }).ToList();

            return result;

        }
        public bool setSendt(int id)
        {
            try
            {
               
                Ordere order = Db.Ordere.Find(id);
                order.sendt = true;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Oppdatering av staus mislykket");
                return false;
            }

        }

        public List<Order> getOrders(int id)
        {
            var order = Db.Ordere.Where(o => o.kundeid == id).ToList();
            List<Order> list = new List<Order>();
            foreach (var vare in order)
            {
                list.Add(new Order()
                {
                    id = vare.id,
                    orderdato = vare.orderdato,
                    sendt = vare.sendt
                });
            }


            return list;
        }
        public List<OrderLine> getOrderLine(int id)
        {
        //DØR INNI HER
            var lines = Db.OrderLiner.Where(o => o.orderid == id).ToList();
            List<OrderLine> list = new List<OrderLine>();
            foreach (var vare in lines)
            {
                var vareProdukt = new Produkt()
                {
                    id = vare.produkt.id,
                    navn = vare.produkt.Navn,
                    pris = vare.produkt.Pris,
                    beskrivelse = vare.produkt.Beskrivelse
                };
                list.Add(new OrderLine()
                {
                    id = vare.id,
                    orderid = vare.id,
                    produkt = vareProdukt,
                    antall = vare.antall,
                    produktid = vare.produktid
                });
            }

            return list;
        }

        //Skrive til fil
        private void writeToFile(Exception e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"NipsLogg.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("******   " + DateTime.Now.ToString() + "   ******");
                    writer.WriteLine("");
                    writer.WriteLine("Message: " + e.Message + Environment.NewLine
                       + "Stacktrace: " + e.StackTrace + Environment.NewLine);
                }
            }
            catch (IOException ioe)
            {
                Debug.WriteLine(ioe.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                Debug.WriteLine(uae.Message);
            }
        }

        void IOrderRepository.writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}


