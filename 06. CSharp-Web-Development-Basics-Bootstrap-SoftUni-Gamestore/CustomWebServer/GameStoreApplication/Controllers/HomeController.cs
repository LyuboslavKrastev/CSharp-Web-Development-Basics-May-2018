﻿namespace CustomWebServer.GameStoreApplication.Controllers
{
    using Services.Contracts;
    using CustomWebServer.Server.Http.Contracts;
    using Services;
    using System.Collections.Generic;
    using System;
    using ViewModels.Admin;
    using System.Text;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly IGameService games;

        public HomeController(IHttpRequest request)
            : base(request)
        {
            this.ViewData["colsView"] = "";

            this.games = new GameService();
        }

        public IHttpResponse Index()
        {
            var rowCount = (int)Math.Ceiling(this.games.AllGames().Count() / 3m);

            var gamesToShow = new Queue<AdminListGameViewModel>(this.games.AllGames());

            var result = new StringBuilder();

            var adminButtons = new StringBuilder();

            for (int i = 0; i < rowCount; i++)
            {
                result.AppendLine($@"<div class=""card-group"">");

                for (int j = 0; j < 3; j++)
                {
                    if (gamesToShow.Count == 0)
                    {
                        break;
                    }

                    var game = gamesToShow.Dequeue();

                    if (this.Authentication.IsAdmin)
                    {
                        adminButtons.Clear();

                        adminButtons
                            .AppendLine($@"<a class=""card-button btn btn-warning"" name=""edit"" href=""{{0}}"">Edit</a>
                                   <a class=""card-button btn btn-danger"" name=""delete"" href=""{{1}}"">Delete</a>");
                    }

                    result.AppendLine($@"<div class=""card col-4 thumbnail"">
                                            <img 
                                                     class=""card-image-top img-fluid img-thumbnail""
                                                     onerror =""this.src='{game.Thumbnail}';""
                                                     src = ""{game.Thumbnail}"" >");

                    result.AppendLine($@"  <div class=""card-body"">
                                             <h4 class=""card-title"">{game.Name}</h4>
                                             <p class=""card-text""><strong>Price</strong> - {game.Price}&euro;</p>
                                             <p class=""card-text""><strong>Size</strong> - {game.Size} GB</p>
                                             <p class=""card-text"">{game.Description}</p>
                                           </div>");

                    result.AppendLine($@"  <div class=""card-footer"">
                                             {adminButtons
                                                     .Replace("{0}", $"admin/games/edit/{game.Id}")
                                                     .Replace("{1}", $"admin/games/delete/{game.Id}")}
                                             <a class=""card-button btn btn-outline-primary"" name=""info"" href=""/games/{game.Id}"">Info</a>    
                                             <a class=""card-button btn btn-primary"" name=""buy"" href=""/games/cart"">Buy</a>
                                           </div>
                                         </div>");
                }
                result.AppendLine($@"</div>");
            }

            this.ViewData["games"] = result.ToString();

            return this.FileViewResponse(@"home\index");
        }
    }
}
