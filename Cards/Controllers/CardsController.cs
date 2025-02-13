﻿namespace Cards.Controllers
{
    using Cards.Data.Models;
    using Cards.Models.Collection;
    using Cards.Services.Cards;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CardsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signManager;
        private readonly ICardsservice cardsService;

        public CardsController(SignInManager<User> signManager, UserManager<User> userManager, ICardsservice cardsservice)
        {
            this.signManager = signManager;
            this.userManager = userManager;
            this.cardsService = cardsservice;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await this.cardsService.All();

            if (!result.Item1)
            {
                return NotFound();
            }
            else
            {
                return this.View(result.Item2);
            }

            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CardCollectionFormModel card)
        {

            var userId = this.userManager.GetUserId(this.User);

            if (this.ModelState.IsValid)
            {

                Tuple<bool, string> result = await this.cardsService.GetKards(card, userId);

                if (!result.Item1)
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }


            }
            else
            {
                return BadRequest();
            }

            //await this.cardsService.Create(card, userId);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Collection()
        {


            var userId =  this.userManager.GetUserId(this.User);


            var result = await this.cardsService.MyCollection(userId);

            if (!result.Item1)
            {
                return NotFound();
            }
            else
            {
                return this.View(result.Item2);
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFromCollection(string cardId)
        {


            var car = this.cardsService.GetCard(cardId);

            if (car == null)
            {
                return NotFound();
            }
            var carDelete = this.cardsService.Delete(car);

            if (!carDelete)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Collection));

        }



    }
}
