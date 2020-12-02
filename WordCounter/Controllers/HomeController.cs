using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using WordCounter.Models;

namespace WordCounter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index([Optional] IDictionary<string,int> disc)
        {

            System.Diagnostics.Debug.WriteLine(disc.Count());
            return View();
        }
        


    public ActionResult SortedCalculate(Array dic,int br)
        {

            

            ViewData["dict"] = dic;

            return View();

        }

        [HttpPost]
        public ActionResult Calculate(HttpPostedFileBase file)
        {
           

            bool result = false;
            StringBuilder strbuild = new StringBuilder();
            try
            {
                if (file.ContentLength == 0)
                    throw new Exception("Zero length file!");
                else
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Document"), fileName);
                    file.SaveAs(filePath);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        result = true;
                        using (StreamReader sr = new StreamReader(Path.Combine(Server.MapPath("~/Document"), fileName)))
                        {
                            while (sr.Peek() >= 0)
                            {
                                strbuild.AppendFormat(sr.ReadLine());
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            String s1 = strbuild.ToString();
            ArrayList list = new ArrayList(s1.Split(' ', ',', ':', ';', '!', '?', '.', '"', '(', ')', ']', '[', '}', '{', '\''));
            IDictionary<string, int> dict = new Dictionary<string, int>();
            
           foreach(String l in list)
            {
                if (l.Length > 1)
                {

                    if (dict.ContainsKey(l.ToLower()))
                    {
                        dict[l.ToLower()] = dict[l.ToLower()] + 1;
                    }
                    else
                    {
                        dict.Add(l.ToLower(), 1);
                    }
                }
            }
            if (dict.ContainsKey("")) dict.Remove("");

            dict=dict.OrderBy(key => key.Value).ToDictionary(key=>key.Key, key=>key.Value);

            ArrayList words = new ArrayList();
            var sol = new List<WordModel>();


            for (int i=0; i<dict.Count(); i++)
            {
                System.Diagnostics.Debug.WriteLine("Rec: {0}, Broj Ponavljanja: {1}", dict.ElementAt(i).Key, dict.ElementAt(i).Value);
                WordModel word = new WordModel(dict.ElementAt(i).Key,dict.ElementAt(i).Value);
                words.Add(word);
                sol.Add(word);

            }
            words.Sort();
            words.Reverse();
            var wordsArray = words.ToArray();
            
         
            
            ViewData["dict"] = wordsArray;
            return View(sol);
        }



    }
}