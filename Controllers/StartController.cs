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

        public IActionResult Profile()
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                ViewBag.username = "@"+userInSession.Username;
                ViewBag.day = userInSession.Day;
                ViewBag.month = userInSession.Month;
                ViewBag.year = userInSession.Year;
                User profileOwnTweets = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u == userInSession);
                var tweets = profileOwnTweets.Tweets.OrderByDescending(u => u.TweetID);
                return View(tweets.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Search(string username)
        {
            User accountSearched = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u.Username.Equals(username));
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(accountSearched != null)
            {
                ViewBag.username = "@"+accountSearched.Username;
                ViewBag.day = accountSearched.Day;
                ViewBag.month = accountSearched.Month;
                ViewBag.year = accountSearched.Year;
                ViewBag.ID = accountSearched.Mail;

                ViewBag.showButtonToFollow = true;
                
                var tweets = accountSearched.Tweets.OrderByDescending(u => u.TweetID);

                if(userInSession.Username.Equals(accountSearched.Username) || userInSession == null)
                {
                    ViewBag.showButtonToFollow = false;
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
                Tweet tweet = new Tweet{
                    Content = content,
                    Owner = creatorToAdd
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
