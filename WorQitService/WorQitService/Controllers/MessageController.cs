using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;


namespace WorQitService.Controllers
{
    public class MessageController : ApiController
    {
        /// <summary>
        /// int employeeID, int employerID, string text, string sender, string title
        /// </summary>
        public object sendMessage()
        {
            try
            {
                var headers = Request.Headers;

                int employeeID = (headers.Contains("employeeID")) ? Int32.Parse(headers.GetValues("employeeID").First()) : -1;
                int employerID = (headers.Contains("employerID")) ? Int32.Parse(headers.GetValues("employerID").First()) : -1;
                string text = (headers.Contains("text")) ? headers.GetValues("text").First() : null;
                string sender = (headers.Contains("sender")) ? headers.GetValues("sender").First() : null;
                string title = (headers.Contains("title")) ? headers.GetValues("title").First() : null;

                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Message msg = new Message()
                {
                    employeeID = employeeID,
                    employerID = employerID,
                    sender = sender,
                    text = text,
                    title = title,
                    date = DateTime.Now
                };
                wqdb.Messages.Add(msg);
                wqdb.SaveChanges();
                return Json(new { Result = "successful" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }
        /// <summary>
        /// update read to true
        /// </summary>
        /// <param name="ID">message ID</param>
        public object messageRead(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Message msg = (from Message in wqdb.Messages
                               where Message.ID == ID
                               select Message).First();
                msg.read = true;

                wqdb.SaveChanges();
                return Json(new { Result = "successful" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        public object getOverviewEmployee(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                List<Message> msg = (from Message in wqdb.Messages
                               where Message.employeeID == ID
                               select Message).ToList<Message>();
                return Json(new { Result = "successful", Messages = msg });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        public object getOverviewEmployer(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                List<Message> msg = (from Message in wqdb.Messages
                                     where Message.employerID == ID
                                     select Message).ToList<Message>();
                return Json(new { Result = "successful", Messages = msg });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }


        /// <summary>
        /// get last messages exchanged between employer and employee
        /// </summary>
        /// <param name="employerID">employer ID</param>
        /// <param name="employeeID">employee ID</param>
        /// <param name="count">number of messages (count == -1 => returns all messages)</param>
        /// <returns></returns>
        public object getLast(int employerID, int employeeID, int count)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                if (count > 0)
                {
                    List<Message> msg = (from Message in wqdb.Messages
                                         where Message.employerID == employerID && Message.employeeID == employeeID
                                         select Message).OrderByDescending(x => x.date).Take<Message>(count).ToList<Message>();
                    return Json(new { Result = "successful", Messages = msg });
                } else if (count == -1)
                {
                    List<Message> msg = (from Message in wqdb.Messages
                                         where Message.employerID == employerID && Message.employeeID == employeeID
                                         select Message).OrderByDescending(x => x.date).ToList<Message>();
                    return Json(new { Result = "successful", Messages = msg });
                }
                else
                {
                    return Json(new { Result = "failed", Error ="Wrong count number"});
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        /// <summary>
        /// gets message by message ID
        /// </summary>
        /// <param name="ID">message ID</param>
        /// <returns></returns>
        public object getMessage(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                
                    Message msg = (from Message in wqdb.Messages
                                         where Message.ID == ID
                                         select Message).First();
                    return Json(new { Result = "successful", Messages = msg });
               
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }
    }

}
