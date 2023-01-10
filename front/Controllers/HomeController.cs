using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EmlakProject.Models;
using EmlakProject.Services.Bussiness;
using EmlakProject.Services.Data;

namespace EmlakProject.Controllers
{
    public class HomeController : Controller
    {
        List<HouseModel> houses = new List<HouseModel>();
        HouseDAO houseDAO = new HouseDAO();
        SecurityDAO sec = new SecurityDAO();

        List<MessageModel> messages = new List<MessageModel>();
        MessageDAO messageDAO = new MessageDAO();

        public static int venId = 0;
        public static string msgType = "";
        public static int messageId = 0;

        public ActionResult Index()
        {
            houses = houseDAO.fetchMostVisited();

            return View("Index" , houses);
        }
        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult LoginProcess(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            Boolean success = securityService.Authenticate(userModel);

            if (success)
            {
                //return View("LoginSuccess", userModel);
                Response.Write("<script>alert('You are logged in successfully')</script>");
                
                houses = houseDAO.fetchMostVisited();
                return View("Index",houses);
            }
            else
            {
                if (securityService.indexLogin == 1)
                    return View("Login");
                else
                {
                    //    return View("LoginFailure");
                    Response.Write("<script>alert('Logging in failed')</script>");
                    return View("Login");
                }
            }
        }
        public ActionResult Buy()
        {
            ViewBag.Message = "Your buy list";

            return View();
        }
        public ActionResult Account()
        {
            if (SecurityDAO.loginUser.userId == 0)
            {
                Response.Write("<script>alert('You have to login!')</script>");
                houses = houseDAO.fetchMostVisited();
                return View("Index", houses);
            }
            else
            {
                return View("Account" , SecurityDAO.loginUser);
            }
        }
        public ActionResult Inbox()
        {
            ViewBag.Message = "Inbox.";

            messages = messageDAO.fetchInbox(SecurityDAO.loginUser.userId);

            return View("Inbox" , messages);
        }
        public ActionResult Sent()
        {
            ViewBag.Message = "Sent Messages.";

            messages = messageDAO.fetchSent(SecurityDAO.loginUser.userId);

            return View("Sent" , messages);
        }
        public ActionResult Houses()
        {
            ViewBag.Message = "Houses.";

            houses = houseDAO.fetchMyHouses(SecurityDAO.loginUser.userId);

            return View("Houses", houses);
        }
        public ActionResult Sell()
        {
            ViewBag.Message = "Your Sell list";
           
            houses = houseDAO.fetchAllSell();

            return View("Sell", houses);
        }
        public ActionResult Rent()
        {
            ViewBag.Message = "Your Rent list";

            houses = houseDAO.fetchAllRent();

            return View("Rent",houses);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NewAccount()
        {
            return View("CreateAccount");
        }
        //public ActionResult Details(int houseId)
        //{
        //    ViewBag.Message = "House details page.";

        //    List<ImageModel> images = new List<ImageModel>();
        //    ImageDAO imageDAO = new ImageDAO();
        //    images = imageDAO.GetHouseDetails(houseId);

        //    return View("Details", images);

        //}
        public ActionResult Details_(int houseId)
        {
            ViewBag.Message = "House details page.";

            HouseModel house = new HouseModel();
            house = houseDAO.fetchOne(houseId);
            houseDAO.updateVisitCount(house);
            house = houseDAO.fetchOne(houseId);
            return View("Details_", house);
        }
        public ActionResult Create()
        {
            if (SecurityDAO.loginUser.userId == 0)
            {
                Response.Write("<script>alert('You have to login!')</script>");
                
                houses = houseDAO.fetchMostVisited();

                return View("Index",houses);
            }
            else
                return View("HouseForm");
        }
        public ActionResult Edit(int houseId)
        {
            HouseModel house = houseDAO.fetchOne(houseId);
            if (SecurityDAO.loginUser.userId == 0)
            {
                Response.Write("<script>alert('You have to login!')</script>");
                houses = houseDAO.fetchMostVisited();
                return View("Index", houses);
            }
            else
            {
                if (SecurityDAO.loginUser.userId == house.vendorId)
                    return View("HouseForm", house);
                else
                {
                    Response.Write("<script>alert('You do not have permission to edit!')</script>");
                    houses = houseDAO.fetchMostVisited();
                    return View("Index", houses);
                }

            }
        }
        public ActionResult Delete(int houseId)
        {
            HouseModel house = houseDAO.fetchOne(houseId);
            if (SecurityDAO.loginUser.userId == 0)
            {
                Response.Write("<script>alert('You have to login!')</script>");
                houses = houseDAO.fetchMostVisited();
                return View("Index",houses);
            }
            else
            {
                if (SecurityDAO.loginUser.userId == house.vendorId)
                    houseDAO.Delete(houseId);
                else
                    Response.Write("<script>alert('You do not have permission to delete!')</script>");

                if (house.houseType == "R")
                {
                    List<HouseModel> houses = houseDAO.fetchAllRent();
                    return View("Rent", houses);
                }
                else
                {
                    List<HouseModel> houses = houseDAO.fetchAllSell();
                    return View("Sell", houses);
                }
               
            }
        }
        public static bool isChangeMain = false , isChangeFirst = false , isChangeSecond = false , isChangeThird = false , isChangeForth = false;
        [HttpPost]
        public ActionResult ProcessCreate(HouseModel houseModel, List<HttpPostedFileBase> postedFiles , HttpPostedFileBase mainFile,
                                          HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
            HouseDAO houseDAO = new HouseDAO();

            #region
            //List<string> img = new List<string>();
            //foreach (HttpPostedFileBase pFile in postedFiles)
            //{
            //    if (pFile != null)
            //    {
            //        string fileName = Path.GetFileName(pFile.FileName);
            //        //pFile.SaveAs(System.IO.Path.Combine(rootDirectory + @"\pictures") + fileName);
            //        //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            //        //houseModel.images.Add(fileName);
            //        img.Add(fileName);
            //    }
            //}
            //houseModel.images = img;
            #endregion

            #region get images
            if (mainFile != null)
            {
                HttpPostedFileBase main = mainFile;
                houseModel.houseMainImg = main.FileName;
                isChangeMain = true;
            }
            else
                houseModel.houseMainImg = houseModel.houseId + ".jpg";

            if (file1 != null)
            {
                HttpPostedFileBase main = file1;
                houseModel.houseImg1 = main.FileName;
                isChangeFirst = true;
            }
            else
                houseModel.houseImg1 = houseModel.houseId + ".1.jpg";

            if (file2 != null)
            {
                HttpPostedFileBase main = file2;
                houseModel.houseImg2 = main.FileName;
                isChangeSecond = true;
            }
            else
                houseModel.houseImg2 = houseModel.houseId + ".2.jpg";
                    
            if (file3 != null)
            {
                HttpPostedFileBase main = file3;
                houseModel.houseImg3 = main.FileName;
                isChangeThird = true;
            }
            else
                houseModel.houseImg3 = houseModel.houseId + ".3.jpg";

            if (file4 != null)
            {
                HttpPostedFileBase main = file4;
                houseModel.houseImg4 = main.FileName;
                isChangeForth = true;
            }
            else
                houseModel.houseImg4 = houseModel.houseId + ".4.jpg";
            #endregion

            int newID = houseDAO.createHouse(houseModel);

            if (newID == -1)
            {
                Response.Write("<script>alert('The house code is already exist!')</script>");
                return View("HouseForm");
            }
            else
            {
                houseModel.houseMainImg = newID + ".jpg";
                houseModel.houseImg1 = newID + ".1.jpg";
                houseModel.houseImg2 = newID + ".2.jpg";
                houseModel.houseImg3 = newID + ".3.jpg";
                houseModel.houseImg4 = newID + ".4.jpg";
                isChangeMain = false; isChangeFirst = false; isChangeSecond = false; isChangeThird = false; isChangeForth = false;
                return View("Details_", houseModel);
            }
        }
        public ActionResult ProcessCreateAccount(UserModel userModel)
        {
            SecurityDAO securityDAO = new SecurityDAO();

            UserModel user_ = securityDAO.createUser(userModel);

            if (user_ == null)
            {
                Response.Write("<script>alert('The email is already exist!')</script>");
                return View("CreateAccount");
            }
            else
            {
                SecurityDAO.loginUser = user_;
                houses = houseDAO.fetchMostVisited();
                return View("Index", houses);
            }
        }
        public ActionResult HouseDetails()
        {
            return View("HouseDetails");
        }
        public ActionResult ContactAgent(int vendorId)
        {
            if (SecurityDAO.loginUser.userId == 0)
            {
                Response.Write("<script>alert('You have to login!')</script>");

                houses = houseDAO.fetchMostVisited();

                return View("Index", houses);
            }
            else if (SecurityDAO.loginUser.userId == vendorId)
            {
                Response.Write("<script>alert('You are the owner of the house!')</script>");

                houses = houseDAO.fetchMostVisited();

                return View("Index", houses);
            }
            else
            {
                MessageModel message = new MessageModel();
                msgType = "m";

                message.customerName = SecurityDAO.loginUser.userFullName;
                message.customerId = SecurityDAO.loginUser.userId;
                message.date = DateTime.Now.ToShortDateString();
                message.time = DateTime.Now.ToShortTimeString();
                UserModel user_ = sec.fetchOne(vendorId);
                message.vendorName = user_.userFullName;
                message.vendorId = user_.userId;
                message.msgType = "m";
                venId = vendorId;
                message.vendorMobile = user_.userPhone;
                message.vendorEmail = user_.userEmail;
                return View("MessageForm" , message );
            }
        }
        public ActionResult ProcessCreateMessage(MessageModel message)
        {
            MessageDAO messageDAO = new MessageDAO();

            MessageModel message_ = messageDAO.createMessage(message);

            houses = houseDAO.fetchMostVisited();

            Response.Write("<script>alert('Your message has been sent!')</script>");

            return View("Index", houses);
        }
        public ActionResult Reply(int msgId)
        {
            MessageModel message = new MessageModel();
            msgType = "r";
            message = messageDAO.fetchOne(msgId);

            MessageModel repMessage = new MessageModel();
            UserModel vendor = sec.fetchOne(message.customerId);
            UserModel customer = sec.fetchOne(message.vendorId);
            repMessage.customerName = customer.userFullName;
            repMessage.customerId = message.vendorId;
            repMessage.date = DateTime.Now.ToShortDateString();
            repMessage.time = DateTime.Now.ToShortTimeString();
            repMessage.vendorName = vendor.userFullName;
            repMessage.vendorId = message.customerId;
            repMessage.msgType = "r";
            messageId = msgId;
            repMessage.vendorMobile = vendor.userPhone;
            repMessage.vendorEmail = vendor.userEmail;

            return View("MessageForm", repMessage);
        }
        public ActionResult SearchForm()
        {
            return View("SearchForm");
        }
        public ActionResult SearchForName(string searchPhrase)
        {
            List<HouseModel> houses = houseDAO.SearchForName(searchPhrase);
            return View("SearchReasult" , houses);
        }
    }
}