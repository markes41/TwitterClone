using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class StartController : Controller
    {
        private readonly ILogger<StartController> logger;
        private readonly TwitterContext db;

        public StartController(ILogger<StartController> logger, TwitterContext context)
        {
            this.logger = logger;
            this.db = context;
        }


        public IActionResult Inicio()
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            if(userInSession != null)
            {
                User creatorToAdd = db.Users.Include(u => u.Tweets).Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));
                List<User> usersList = new List<User>();

                for (int i = 0; i < creatorToAdd.Following.Count(); i++)
                {
                    usersList.Add(db.Users.Include(u => u.Tweets).FirstOrDefault(u => u == creatorToAdd.Following[i]));
                }
                
                usersList.Add(creatorToAdd);
                List<Tweet> tweets = new List<Tweet>();
                for (int i = 0; i < usersList.Count(); i++)
                {
                    tweets.AddRange(usersList[i].Tweets);
                }
                var tweetsOrganized = tweets.OrderByDescending(t => t.TweetID);

                for (int i = 0; i < tweets.Count(); i++)
                {
                    tweetsOrganized = tweets.OrderByDescending(t => t.TweetID);
                }
            
                return View(tweetsOrganized.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Profile()
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User profileOwnTweets = db.Users.Include(u => u.Followers).Include(u => u.Following).Include(u => u.Tweets).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));
                ViewBag.Email = userInSession.Mail;
                ViewBag.username = "@"+userInSession.Username;
                ViewBag.day = userInSession.Day;
                ViewBag.month = userInSession.Month;
                ViewBag.year = userInSession.Year;
                ViewBag.creationYear = userInSession.CreationYear;
                ViewBag.creationMonth = userInSession.CreationDay;
                ViewBag.creationDay = userInSession.CreationMonth;
                ViewBag.name = userInSession.Name;
                ViewBag.Followers = profileOwnTweets.Followers.Count();
                ViewBag.Following = profileOwnTweets.Following.Count();
                var tweets = profileOwnTweets.Tweets.OrderByDescending(u => u.TweetID);
                return View(tweets.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Followers()
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userFollowers = db.Users.Include(u => u.Followers).Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));

                return View(userFollowers);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Following()
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userFollowing = db.Users.Include(u => u.Followers).Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));
                return View(userFollowing);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Search(string username)
        {
            User accountSearched = db.Users.Include(u => u.Followers).Include(u => u.Following).Include(u => u.Tweets).FirstOrDefault(u => u.Username.Equals(username));
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(accountSearched != null)
            {
                ViewBag.username = "@"+accountSearched.Username;
                ViewBag.day = accountSearched.Day;
                ViewBag.month = accountSearched.Month;
                ViewBag.year = accountSearched.Year;
                ViewBag.ID = accountSearched.Mail;
                ViewBag.creationYear = accountSearched.CreationYear;
                ViewBag.creationMonth = accountSearched.CreationMonth;
                ViewBag.name = accountSearched.Name;
                ViewBag.Followers = accountSearched.Followers.Count();
                ViewBag.Following = accountSearched.Following.Count();

                ViewBag.showButtonToFollow = true;
                
                var tweets = accountSearched.Tweets.OrderByDescending(u => u.TweetID);

                if(userInSession.Username.Equals(accountSearched.Username) || userInSession == null)
                {
                    ViewBag.showButtonToFollow = false;
                }

                User searchAlreadyFollow = db.Users.Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));

                for (int i = 0; i < searchAlreadyFollow.Following.Count(); i++)
                {
                    if(searchAlreadyFollow.Following[i].Username.Equals(username))
                    {
                        ViewBag.alreadyFollowed = true;
                    }
                }

                return View("Profile", tweets.ToList());
            }
            else
            {
                ViewBag.userNotFound = true;
                return View("Profile");
            }
        }

        public void NewTweet(string content)
        {
            User tweetCreator = HttpContext.Session.Get<User>("UsuarioLogueado");
            if(tweetCreator != null)

            {
                User creatorToAdd = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u.Mail.Equals(tweetCreator.Mail));
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                
                Tweet tweet = new Tweet{
                    Content = content,
                    Owner = creatorToAdd,
                    CreationDay = datevalue.Day.ToString(),
                    CreationMonth = monthWithName(datevalue.Month),
                    CreationYear = datevalue.Year.ToString()
                };
                creatorToAdd.Tweets.Add(tweet);
                db.Users.Update(creatorToAdd);
                db.SaveChanges();
            }

        }

        public void ToRetweet(int ID)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userToRetweet = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));
                Tweet tweetToRetweet = db.Tweets.FirstOrDefault(t => t.TweetID == ID);

                userToRetweet.Tweets.Add(tweetToRetweet);
                db.Users.Update(userToRetweet);
                db.SaveChanges();
            }
        }

        public void ToFollow(string ID)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userProfile = db.Users.Include(u => u.Followers).FirstOrDefault(u => u.Mail.Equals(ID));
                User userAddFollow = db.Users.Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));

                if(userProfile != null && userAddFollow != null)
                {
                    //ADD THE FOLLOWING TO userAddFollow
                    userAddFollow.Following.Add(userProfile);
                    db.Users.Update(userAddFollow);
                
                    //ADD THE FOLLOWER TO userProfile
                    userProfile.Followers.Add(userAddFollow);
                    db.Users.Update(userProfile);

                    //SAVE THE NEW VALUES IN THE DB
                    db.SaveChanges();
                }
                
            }
        }

        public JsonResult BringTweets()
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            User creatorToAdd = db.Users.Include(u => u.Tweets).Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));
            List<User> usersList = new List<User>();

            for (int i = 0; i < creatorToAdd.Following.Count(); i++)
            {
                usersList.Add(db.Users.Include(u => u.Tweets).FirstOrDefault(u => u == creatorToAdd.Following[i]));
            }
            
            usersList.Add(creatorToAdd);
            List<Tweet> tweets = new List<Tweet>();
            for (int i = 0; i < usersList.Count(); i++)
            {
                tweets.AddRange(usersList[i].Tweets);
            }
            var tweetsOrganized = tweets.OrderByDescending(t => t.TweetID);

            for (int i = 0; i < tweets.Count(); i++)
            {
                tweetsOrganized = tweets.OrderByDescending(t => t.TweetID);
            }
        
            return Json(tweetsOrganized.ToList());
            
        }

        public void Unfollow(string ID)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userToDoAction = db.Users.Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));

                for (int i = 0; i < userToDoAction.Following.Count(); i++)
                {
                    if(userToDoAction.Following[i].Mail.Equals(ID))
                    {
                         userToDoAction.Following.RemoveAt(i);
                         break;
                    }
                }

                db.Users.Update(userToDoAction);
                db.SaveChanges();
            }
        }

        public JsonResult EditProfile()
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            return Json(userInSession);
        }

        public void ToEditProfile(string name, string biography, string location)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(userInSession != null)
            {
                User userToEdit = db.Users.FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));

                userToEdit.Biography = biography;
                userToEdit.Name = name;
                userToEdit.Location = location;

                db.Users.Update(userToEdit);
                db.SaveChanges();
            }
        }

        public string monthWithName(int month)
        {
            string monthname = null;
            switch(month)
            {
                case 1:
                    monthname = "ene.";
                    break;
                case 2:
                    monthname = "feb.";
                    break;
                case 3:
                    monthname =  "mar.";
                    break;
                case 4:
                    monthname =  "abr.";
                    break;
                case 5:
                    monthname =  "may.";
                    break;
                case 6:
                    monthname =  "jun.";
                    break;
                case 7:
                    monthname =  "jul.";
                    break;
                case 8:
                    monthname =  "ago.";
                    break;
                case 9:
                    monthname =  "sep.";
                    break;
                case 10:
                    monthname =  "oct.";
                    break;
                case 11:
                    monthname =  "nov.";
                    break;
                case 12:
                    monthname =  "dic.";
                    break;
                default:
                    monthname =  "Mes inválido";
                    break;
            }

            return monthname;
        }

        public JsonResult GetTheRecentTweet(string content)
        {
            Tweet recentTweet = db.Tweets.Include(t => t.Owner).FirstOrDefault(t => t.Content.Equals(content));
            return Json(recentTweet);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
