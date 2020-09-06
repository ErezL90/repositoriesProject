using Newtonsoft.Json;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Repositories.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search by json string for show as list items
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Search(string searchWord)
        {
            using (var webClient = new WebClient())
            {
                var result = new JsonResult { };
                if (searchWord != null)
                {
                    //get array from website https://api.github.com/search/repositories?q=YOUR_SEARCH_KEYWORD

                    //var uri = "https://api.github.com/search/repositories?q=" + searchWord;
                    try
                    {
                        webClient.Headers.Add("user-agent", "Only a test!");
                        var rawJson = webClient.DownloadString("https://api.github.com/search/repositories?q=" + searchWord);
                        result.Data = JsonConvert.DeserializeObject<ItemsCollection>(rawJson);
                    }
                    catch (Exception ex)
                    {
                        result.Data = ex.ToString();
                    }
                }
                else
                {
                    result.Data = "EmptySearching";
                }
                return result;
            }

        }

        /// <summary>
        /// Save the array to session storage
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemName"></param>
        /// <param name="itemOwnerId"></param>
        /// <param name="itemOwnerAvatar"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveItems(int itemId, string itemName, int itemOwnerId, string itemOwnerAvatar)
        {
            var list = Session["itemsArray"] as List<Items>;

            Items itemToAdd = new Items
            {
                Id = itemId,
                Name = itemName,
                Owner =
                new Owner { Id = itemOwnerId, Avatar_url = itemOwnerAvatar }
            };

            try
            {
                var result = new JsonResult { Data = "" };

                if (list != null)
                {
                    bool exist = list.Any(item => item.Id == itemToAdd.Id);
                    if (exist)
                        result.Data = "itemExist";
                    else
                    {
                        list.Add(itemToAdd);
                        Session["itemsArray"] = list;
                        result.Data = "old";
                    }
                }
                else
                {
                    List<Items> listNew = new List<Items>();
                    listNew.Add(itemToAdd);
                    Session["itemsArray"] = listNew;
                    result.Data = "new";
                }
                return result;
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message.ToString() };
            }


        }
        /// <summary>
        /// Get items gallery result for user's cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MyRepositories()
        {
            List<Items> list2Show = (List<Items>)Session["itemsArray"];

            return View();
        }


    }
}