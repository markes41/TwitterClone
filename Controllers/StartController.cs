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
                ViewBag.Username = userInSession.Username;
                return View(orderTweets(userInSession));
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
                User profileOwnTweets = db.Users.Include(u => u.Followers).Include(u => u.Following).Include(u => u.Tweets).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));
                ViewBag.Email = userInSession.Mail;
                ViewBag.username = "@"+userInSession.Username;
                ViewBag.day = userInSession.Day;
                ViewBag.month = userInSession.Month;
                ViewBag.year = userInSession.Year;
                ViewBag.creationYear = userInSession.CreationYear;
                ViewBag.creationMonth = userInSession.CreationMonth;
                ViewBag.name = userInSession.Name;
                ViewBag.Followers = profileOwnTweets.Followers.Count();
                ViewBag.Following = profileOwnTweets.Following.Count();
                ViewBag.ProfilePicture = profileOwnTweets.ProfilePicture;
                ViewBag.CoverPicture = profileOwnTweets.CoverPicture;
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
            
            if(accountSearched == userInSession)
            {
                return View();
            }
            else
            {
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
                    ViewBag.ProfilePicture = accountSearched.ProfilePicture;
                    ViewBag.CoverPicture = accountSearched.CoverPicture;

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
        }

        public JsonResult NewTweet(string content)
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

                return Json(TweetToReturn(tweet));
            }

                return Json("falló");
        }

        public Tweet TweetToReturn(Tweet tweetToSearch)
        {
            Tweet tweet = db.Tweets.FirstOrDefault(t => t == tweetToSearch);
            return tweet;
        }

        /*public void ToRetweet(int ID, string username)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userToRetweet = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));
                
                User userOwnerTweet = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u.Username.Equals(username));

                for(int i = 0; i < userOwnerTweet.Tweets.Count(); i++)
                {
                    if(userOwnerTweet.Tweets[i].TweetID == ID)
                    {
                        userToRetweet.Tweets.Insert(1, userOwnerTweet.Tweets[i]);
                        break;
                    }
                }

                db.Users.Update(userToRetweet);
                db.SaveChanges();
            }
        }*/

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
            return Json(orderTweets(userInSession));
            
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

        public JsonResult EditProfile(string mail)
        {
            User userToEditProfile = db.Users.FirstOrDefault(u => u.Mail.Equals(mail));

            if(userToEditProfile != null)
            {
                return Json(userToEditProfile);
            }
            else
            {
                return Json("No se encontró el usuario.");
            }
            
        }

        public IActionResult ToEditProfile(string name, string biography, string location)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(userInSession != null)
            {
                User userToEdit = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));

                userToEdit.Biography = biography;
                userToEdit.Name = name;
                userToEdit.Location = location;

                db.Users.Update(userToEdit);
                db.SaveChanges();
                ViewBag.CoverPicture = userToEdit.CoverPicture;
                ViewBag.ProfilePicture = userToEdit.ProfilePicture;
                return View("Profile", userToEdit.Tweets.OrderByDescending(t => t.TweetID).ToList());
            }

            return RedirectToAction("Login", "Home");
        }

        public IActionResult Tweet(int TweetID)
        {
            Tweet tweetToShow = db.Tweets.Include(t => t.Owner).Include(t => t.Comments).FirstOrDefault(t => t.TweetID == TweetID);
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            if(tweetToShow != null)
            {
                ViewBag.UserToCommentProfilePicture = userInSession.ProfilePicture;
                return View(tweetToShow);
            }
            else
            {
                return View("ErrorPage");
            }
        }

        public void AddComment(string comment, string tweetToReply)
        {
            //Usuario que está en sesión y va a agregar un comentario
            User userReply = HttpContext.Session.Get<User>("UsuarioLogueado");
            if(userReply != null)
            {
                //Buscamos el tweet al que vamos a responder con su lista de comentarios
                Tweet tweetReply = db.Tweets.Include(t => t.Comments).FirstOrDefault(t => t.Content.Equals(tweetToReply));

                //Si el tweet existe, agregamos el tweet de respuesta a su lista de comentarios
                if(tweetReply != null)
                {
                    String sDate = DateTime.Now.ToString();
                    DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                
                    Tweet addTweetReply = new Tweet{
                        Content = comment,
                        Owner = userReply,
                        CreationDay = datevalue.Day.ToString(),
                        CreationMonth = monthWithName(datevalue.Month),
                        CreationYear = datevalue.Year.ToString()
                    };

                    tweetReply.Comments.Add(addTweetReply);
                    db.Tweets.Update(tweetReply);
                    db.SaveChanges();
                }
            }
        }

        public void SaveProfilePicture(string URL)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userToSaveUrl = db.Users.FirstOrDefault(u => u == userInSession);

                userToSaveUrl.ProfilePicture = URL;
                db.Users.Update(userToSaveUrl);
                db.SaveChanges();
            }
        }

        public void SaveCoverPicture(string URL)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userInSession != null)
            {
                User userToSaveUrl = db.Users.FirstOrDefault(u => u == userInSession);

                userToSaveUrl.CoverPicture = URL;
                db.Users.Update(userToSaveUrl);
                db.SaveChanges();
            }
        }
        public IActionResult ErrorPage()
        {
            return View();
        }

        public List<Tweet> orderTweets(User inSession)
        {
            User creatorToAdd = db.Users.Include(u => u.Tweets).Include(u => u.Following).FirstOrDefault(u => u.Mail.Equals(inSession.Mail));
            List<User> usersList = new List<User>();
            ViewBag.ProfilePicture = creatorToAdd.ProfilePicture;

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

            return tweetsOrganized.ToList();

        }

        public void DeleteTweet(int ID)
        {
            User userInSession = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            Tweet tweetToDelete = db.Tweets.FirstOrDefault(t => t.TweetID == ID);
            User userToDeleteTweet = db.Users.Include(u => u.Tweets).FirstOrDefault(u => u.Mail.Equals(userInSession.Mail));

            userToDeleteTweet.Tweets.Remove(tweetToDelete);
            db.Users.Update(userToDeleteTweet);
            db.SaveChanges();
            
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
